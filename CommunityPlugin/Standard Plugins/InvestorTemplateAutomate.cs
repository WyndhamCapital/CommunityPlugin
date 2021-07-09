using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunityPlugin.Objects;
using CommunityPlugin.Objects.Helpers;
using CommunityPlugin.Objects.Interface;
using EllieMae.Encompass.Automation;
using EllieMae.Encompass.BusinessObjects.Loans;

namespace CommunityPlugin.Standard_Plugins
{
    public class InvestorTemplateAutomate : Plugin, IFieldChange, ILoanOpened
    {
        public override bool Authorized()
        {
            return PluginAccess.CheckAccess(nameof(InvestorTemplateAutomate));
        }

        public override void LoanOpened(object sender, EventArgs e)
        {

        }

        public override void FieldChanged(object sender, FieldChangeEventArgs e)
        {
            if (e.FieldID.Equals("VEND.X263") && !string.IsNullOrEmpty(e.NewValue))
            {
                if (EncompassApplication.CurrentLoan.Fields["1999"].IsEmpty() == false)
                {

                    string investorName = e.NewValue;

                    var investorTemplate = EncompassApplication.Session.SystemSettings.Secondary.InvestorTemplates[investorName];
                    if (investorTemplate == null)
                    {
                        MessageBox.Show(
                            $"Error: Unable to find Investor Template name for '{investorName}' to apply template. " +
                            $"This is a major issue that needs to get resolved or loan will not have proper investor data.");

                        return;
                    }

                    // Applies the MilestoneTemplate to the loan.
                    EncompassApplication.CurrentLoan.ApplyInvestorToLoan(investorTemplate);

                }
            }
        }
    }
}
