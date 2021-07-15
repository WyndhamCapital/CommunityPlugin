using CommunityPlugin.Objects;
using CommunityPlugin.Objects.Args;
using CommunityPlugin.Objects.Interface;
using EllieMae.EMLite.DocEngine;
using EllieMae.EMLite.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommunityPlugin.Non_Native_Modifications.Dialog
{
    public class DisclosurePlanCodeValidation : Plugin, ILogin
    {
        Form orderEdisclosuresForm;
        Button orderDisclosuresButton;
        public override bool Authorized()
        {
            return PluginAccess.CheckAccess(nameof(DisclosurePlanCodeValidation));
        }

        public override void Login(object sender, EventArgs e)
        {
            FormWrapper.FormOpened += FormWrapper_FormOpened;
        }

        private void FormWrapper_FormOpened(object sender, FormOpenedArgs e)
        {
            if (e.OpenForm.Name.Equals("orderdisclosuredialog", StringComparison.OrdinalIgnoreCase))
            {
                orderEdisclosuresForm = e.OpenForm;

                this.DisableOrderEdisclosuresButton();
                this.MonitorPlanCodeSelected();
            }
        }

        private void MonitorPlanCodeSelected()
        {
            var planCodeGrid = orderEdisclosuresForm.Controls.Find("gvPlanCodes", true).FirstOrDefault() as GridView;
            if (planCodeGrid != null)
            {
                planCodeGrid.SelectedIndexChanged += PlanCodeGrid_SelectedIndexChanged;
            }
        }

        private void DisableOrderEdisclosuresButton()
        {
            var pnlWithButtons = orderEdisclosuresForm.Controls.Find("pnlButtons", true);
            if (pnlWithButtons != null && pnlWithButtons.Count() > 0)
            {
                Panel ButtonsPanel = (Panel)pnlWithButtons[0];
                var panelControls = ButtonsPanel.Controls;

                var orderEdisclosuresButton = ButtonsPanel.Controls.Find("processBtn", true);
                if (orderEdisclosuresButton != null)
                {
                    var orderButton = (Button)orderEdisclosuresButton[0];
                    orderButton.Enabled = false;
                    orderDisclosuresButton = orderButton;
                }
            }
        }

        private void PlanCodeGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            var fileGrid = orderEdisclosuresForm.Controls.Find("gvPlanCodes", true).FirstOrDefault() as GridView;
            if (fileGrid != null)
            {
                if (fileGrid.SelectedItems != null && fileGrid.SelectedItems.Count > 0)
                {
                    var selectedPlan = (StandardPlan)fileGrid.SelectedItems[0].Tag;

                    var fields = selectedPlan.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                    var dataEntity = fields.FirstOrDefault(x => x.FieldType == typeof(EllieMae.EMLite.DocEngine.DocEngineMetadata));

                    if (dataEntity != null)
                    {

                    }

                    // SP -- need to convert FieldInfo object below to EllieMae.EMLite.DocEngine.DocEngineEntity
                    // to somehow get to the properties; if you try cast below; it will exception
                    // you cannot cast from fieldInfo to a POCO

                    dynamic test = fields[0];
                    //var convertTest = (EllieMae.EMLite.DocEngine.DocEngineEntity)test;
                    //var loanTypeValue = convertTest.GetAttribute("LoanTypeValue");



                    // SP - run validation -- if validation passes, enable the button again
                    orderDisclosuresButton.Enabled = true;

                }
            };
        }




    }
}
