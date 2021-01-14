using CommunityPlugin.Objects;
using CommunityPlugin.Objects.Interface;
using EllieMae.EMLite.UI;
using EllieMae.EMLite.WebServices;
using EllieMae.Encompass.Automation;
using EllieMae.Encompass.BusinessObjects.Loans.Logging;
using EllieMae.Encompass.BusinessObjects.Users;
using Encompass.DocService;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommunityPlugin.Non_Native_Modifications.Dialog
{
    public class BlendSendDisclosures : Plugin, ILogin
    {
        Button SendDisclosures = null;
        Form emDialogForm;
        GridView DocumentGrid;
        string DocumentGridControlId = "gvDocuments";

        Form eFolderDialogForm;

        public override bool Authorized()
        {
            return PluginAccess.CheckAccess(nameof(BlendSendDisclosures));
        }

        public override void Login(object sender, EventArgs e)
        {

            FormWrapper.FormOpened += FormWrapper_FormOpened;
        }



        private void FormWrapper_FormOpened(object sender, Objects.Args.FormOpenedArgs e)
        {
            foreach (Form openForm in (ReadOnlyCollectionBase)Application.OpenForms)
            {
                switch (openForm.Name.ToLower())
                {
                    case "formselectiondialog":
                        this.emDialogForm = openForm;
                        HideKmSendDisclosuresButton();

                        // SP 09/20 -- Uncomment here to inject WCM button
                        //InjectWcmSendDisclosuresButton();
                        break;

                    case "efolderdialog":
                        this.HideKmRetreiveBlendDocsButton(openForm);
                        break;

                    default:
                        break;
                }

            }
        }


        private void HideKmRetreiveBlendDocsButton(Form eFolderDialogForm)
        {
            Control[] pnlToolbarcontrol = eFolderDialogForm.Controls.Find("pnlToolbar", true);


            if (pnlToolbarcontrol != null && pnlToolbarcontrol.Count() > 0)
            {
                FlowLayoutPanel flowLayoutPanel = pnlToolbarcontrol[0] as FlowLayoutPanel;
                if (flowLayoutPanel == null)
                    return;


                //Find KM button and hide it -- we don't want users pulling in documents from their control
                if (flowLayoutPanel.Controls != null && flowLayoutPanel.Controls.Count > 0)
                {

                    var kmButton = flowLayoutPanel.Controls.Find("btnBlendRetrieveDocs", true);


 
                    if (kmButton == null || kmButton.Count() == 0)
                    {
                        // km button doesn't appear until AFTER form loads
                        // if button doesn't exist when form loads, start new thread that will search for it and wait until it finds it
                        Task hideBtnTask = new TaskFactory().StartNew(() =>
                        {
                            for (int i = 0; i < 30; i++)
                            {
                                Thread.Sleep(250);
                                kmButton = flowLayoutPanel.Controls.Find("btnBlendRetrieveDocs", true);
                                bool hideButtonSuccessful = HideKmRetreieveButton(kmButton);
                                if (hideButtonSuccessful)
                                {
                                    break;
                                }
                            }

                        });

                    }
                    else
                    {
                        this.HideKmRetreieveButton(kmButton);
                    }

                }
            }
        }

        private bool HideKmRetreieveButton(Control[] kmButton)
        {

            if (kmButton != null && kmButton.Count() > 0)
            {
                Button kmSendDisclosuresButton = (Button)kmButton[0];
                kmSendDisclosuresButton.Visible = false;
                kmSendDisclosuresButton.Enabled = false;
                return true;
            }

            return false;
        }


        private void InjectWcmSendDisclosuresButton()
        {
            string formName = emDialogForm.Name;
            if (!formName.Equals("FormSelectionDialog", StringComparison.OrdinalIgnoreCase))
                return;

            DocumentGrid = emDialogForm.Controls.Find(DocumentGridControlId, true).FirstOrDefault() as GridView;
            //if (DocumentGrid == null)
            //    return;

            // need to add button here
            var pnlcontrol = emDialogForm.Controls.Find("pnlButtons", true);
            if (pnlcontrol != null && pnlcontrol.Count() > 0)
            {
                Panel ButtonsPanel = (Panel)pnlcontrol[0];
                var panelControls = ButtonsPanel.Controls;

                // if button hasn't been added, add it now
                var wcmButton = ButtonsPanel.Controls.Find("WcmDisclosures", true);
                if (wcmButton != null && wcmButton.Count() == 0)
                {
                    SendDisclosures = new Button();
                    SendDisclosures.Name = "WcmDisclosures";
                    SendDisclosures.Text = "WCM TESTING";
                    SendDisclosures.Size = new Size(70, 22);
                    // Back.Image = (Image)Resources.Back;
                    SendDisclosures.Click += new EventHandler(SendDisclosuresButton_Click);
                    SendDisclosures.Location = new Point(75, 22);
                    SendDisclosures.Visible = true;
                    SendDisclosures.Enabled = true;
                    SendDisclosures.BringToFront();
                    ButtonsPanel.Controls.Add((Control)SendDisclosures);
                    ButtonsPanel.Refresh();
                }
            }
        }

        public void HideKmSendDisclosuresButton()
        {
            var pnlcontrol = emDialogForm.Controls.Find("pnlButtons", true);
            if (pnlcontrol != null && pnlcontrol.Count() > 0)
            {
                Panel ButtonsPanel = (Panel)pnlcontrol[0];

                //Find KM button and hide it -- we don't want users sending disclosures through KM button
                if (ButtonsPanel.Controls != null && ButtonsPanel.Controls.Count > 0)
                {
                    var kmButton = ButtonsPanel.Controls.Find("btnDiscloseToBlend", true);
                    if (kmButton != null && kmButton.Count() > 0)
                    {
                        Button kmSendDisclosuresButton = (Button)kmButton[0];
                        kmSendDisclosuresButton.Visible = false;
                        kmSendDisclosuresButton.Enabled = false;
                    }
                }
            }
        }

        private void SendDisclosuresButton_Click(object sender, EventArgs e)
        {

            if (DocumentGrid == null)
            {
                DocumentGrid = emDialogForm.Controls.Find(DocumentGridControlId, true).FirstOrDefault() as GridView;
                if (DocumentGrid == null)
                {
                    MessageBox.Show("Unable to locate Document Grid with disclosures. Please submit a help desk ticket!");
                    return;
                }
            }

            MethodInfo getDocsMethod = this.emDialogForm.GetType().GetMethod("getSelectedDocuments", BindingFlags.Instance | BindingFlags.NonPublic);
            PdfDocumentList _eDiscPdfDocList = getDocsMethod.Invoke((object)this.emDialogForm, new object[0]) as PdfDocumentList;

            if (Directory.Exists(@"c:\temp"))
            {
                string pdfJson = JsonConvert.SerializeObject(_eDiscPdfDocList, Formatting.Indented);
                string name = string.Format("C:\\temp\\BlendDisclosures-{0:MM-dd-yyyy_hh-mm-tt}.json", DateTime.Now);
                File.WriteAllText(name, pdfJson);
                Macro.Alert($"File saved to: {name}");
            }

            // we've already selected the package type, so grab it here

            // need to move blend disclosure methods over to wyndham lib global file

            // read each local file on hard drive into byte array to send them!

        }



    }

}
