using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunityPlugin.Objects;
using CommunityPlugin.Objects.Helpers;
using CommunityPlugin.Objects.Interface;
using CommunityPlugin.Objects.Models.WCM.DocumentImporter;
using CommunityPlugin.Objects.Models.WCM.PricingAlertAudit;
using EllieMae.Encompass.Automation;
using EllieMae.Encompass.BusinessObjects;
using Newtonsoft.Json;

namespace CommunityPlugin.Standard_Plugins
{
    public class PricingAlertAudit : Plugin, ILogin, ILoanOpened, ICommitted
    {
        private List<PricingAlertField> _pricingAlertFields;
        private bool _hasEmailSentForThisLoan = false;

        public override bool Authorized()
        {
            return PluginAccess.CheckAccess(nameof(PricingAlertAudit));
        }

        public override void Login(object sender, EventArgs e)
        {
            new TaskFactory().StartNew(() =>
            {
                _pricingAlertFields = CDOHelper.CDO.CommunitySettings.WcmSettings.PricingAlertAuditFields;
            });
        }

        public override void LoanOpened(object sender, EventArgs e)
        {
            // when loan opens, reset variable so we can re-send alert for newly opened loan
            _hasEmailSentForThisLoan = false;
        }


        public override void Committed(object sender, PersistentObjectEventArgs e)
        {
            new TaskFactory().StartNew(() =>
            {

                if (_hasEmailSentForThisLoan == false)
                {
                    var didPricingChange = false;

                    foreach (var pricingAlertField in _pricingAlertFields)
                    {
                        didPricingChange = CheckIfPricingFieldChanged(pricingAlertField);

                        // SP - when first field is found that changed, no need to check others
                        if (didPricingChange)
                            break;
                    }

                    if (didPricingChange)
                    {
                        _hasEmailSentForThisLoan = true;

                        // SP - triggers KM advanced email utility to send email
                        EncompassApplication.CurrentLoan.Fields["CX.KM.HTMLEMAIL"].Value = "Pricing_Audit";
                    }
                }
            });

        }

        private bool CheckIfPricingFieldChanged(PricingAlertField pricingAlertField)
        {
            var currentLoan = EncompassApplication.CurrentLoan;

            var systemField = currentLoan.Fields[pricingAlertField.EncompassFieldToMonitor];
            var customField = currentLoan.Fields[pricingAlertField.CustomFieldToCompare];

            if (systemField.IsEmpty() &&
                customField.IsEmpty() == false)
            {
                return true;
            }
            else
            {

                // SP - comparing field.value before compares objects; if that works below is not necssary
                // if comparing objects does not work, may need reflection below
                //var systemValue = Cast(systemField.Value, systemField.Value.GetType());
                //var customValue = Cast(customField.Value, customField.Value.GetType());
                //if (systemValue.Equals(customValue))
                //{
                //    return true;
                //}

                if (systemField.Value.Equals(customField.Value))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }

        public static dynamic Cast(dynamic obj, Type castTo)
        {
            return Convert.ChangeType(obj, castTo);
        }
    }
}
