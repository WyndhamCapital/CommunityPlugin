using CommunityPlugin.Objects;
using CommunityPlugin.Objects.Args;
using CommunityPlugin.Objects.Interface;
using EllieMae.Encompass.Automation;
using EllieMae.Encompass.BusinessObjects.Loans;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WyndhamLibGlobal;
using WyndhamLibGlobal.BlendPortal;

namespace CommunityPlugin.Non_Native_Modifications.Dialog
{
    public class ConditionsManagerInUwCondition : Plugin, ILogin, ILoanOpened
    {
        List<ConditionsManagerCondition> ConditionsAlreadyPostedToBlend;
        List<ConditionsManagerCondition> ConditionsToQueue;


        public override bool Authorized()
        {
            return PluginAccess.CheckAccess(nameof(ConditionsManagerInUwCondition));
        }

        public override void Login(object sender, EventArgs e)
        {
            FormWrapper.FormOpened += FormWrapper_FormOpened;
        }


        public override void LoanOpened(object sender, EventArgs e)
        {

        }


        private void FormWrapper_FormOpened(object sender, FormOpenedArgs e)
        {
            var form = e.OpenForm;
            if (!form.Name.Equals("UnderwritingDetailsDialog", StringComparison.OrdinalIgnoreCase))
                return;

            var pnlStatus = form.Controls.Find("pageStatus", true);
            if (pnlStatus != null && pnlStatus.Count() > 0)
            {
                Panel uwStatusPanel = (Panel)pnlStatus[0];
                if (uwStatusPanel.Controls != null && uwStatusPanel.Controls.Count > 0)
                {
                    string borrowerName = "";
                    // populate borrower checkboxes that let you select who to send tasks to
                    BorrowerPair currentPair = EncompassApplication.CurrentLoan.BorrowerPairs.Current;

                    string borrowerBlendId = BlendUtility.GetCurrentBorrowerPairBorrowerBlendId(EncompassApplication.CurrentLoan);
                    if (string.IsNullOrEmpty(borrowerBlendId))
                        return;

                    borrowerName = $"{currentPair.Borrower.FirstName} {currentPair.Borrower.LastName}";


                    //if (currentPair.CoBorrower != null)
                    //{
                    //    string coborrowerBlendId = BlendUtility.GetCurrentBorrowerPairCoBorrowerBlendId(theLoan);
                    //    if (string.IsNullOrEmpty(coborrowerBlendId) == false)
                    //    {
                    //        CoBorrowePortalId = coborrowerBlendId;
                    //        checkBox_CoBorrower.Text = $"{currentPair.CoBorrower.FirstName} {currentPair.CoBorrower.LastName}";
                    //        checkBox_CoBorrower.Visible = true;
                    //    }
                    //}

                    //has this condition already been posted
                    // if y, show updates control
                    // if n, show post control

                    var conditionManager = new BlendConditionManagerPost_Control(borrowerName);
                    conditionManager.Location = new Point(100, 70);

                    conditionManager.Visible = true;
                    conditionManager.BringToFront();
                    uwStatusPanel.Controls.Add((Control)conditionManager);
                    uwStatusPanel.Refresh();

                    //var SendDisclosures = new Button();
                    //SendDisclosures.Name = "WcmDisclosures";
                    //SendDisclosures.Text = "WCM TESTING";
                    //SendDisclosures.Size = new Size(70, 22);
                    //// Back.Image = (Image)Resources.Back;
                    //SendDisclosures.Click += new EventHandler(SendDisclosuresButton_Click);
                    //SendDisclosures.Location = new Point(75, 22);
                    //SendDisclosures.Visible = true;
                    //SendDisclosures.Enabled = true;
                    //SendDisclosures.BringToFront();
                    //ButtonsPanel.Controls.Add((Control)SendDisclosures);
                    //ButtonsPanel.Refresh();
                }
            }

        }

        private void SendDisclosuresButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Gotchya!");
        }
    }
}
