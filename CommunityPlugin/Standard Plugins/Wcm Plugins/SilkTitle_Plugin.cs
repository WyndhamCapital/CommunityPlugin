using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityPlugin.Objects;
using CommunityPlugin.Objects.Helpers;
using CommunityPlugin.Objects.Interface;
using CommunityPlugin.Objects.Models;
using EllieMae.Encompass.BusinessObjects.Loans;
using EllieMae.Encompass.BusinessObjects.Loans.Logging;
using EllieMae.Encompass.Collections;
using WyndhamLibGlobal;
using WyndhamLibGlobal.Silk;
using Attachment = EllieMae.Encompass.BusinessObjects.Loans.Attachment;


namespace CommunityPlugin.Standard_Plugins.Wcm_Plugins
{
    class SilkTitle_Plugin : Plugin, IMilestoneCompleted, IFieldChange
    {
        private static readonly WcmSettings WcmSettings = CDOHelper.CDO.CommunitySettings.WcmSettings;
        public override bool Authorized()
        {
            return PluginAccess.CheckAccess(nameof(SilkTitle_Plugin));
        }

        //Milestone Items for Silk
        public override void MilestoneCompleted(object sender, MilestoneEventArgs milestoneEventArgs)
        {
            Loan loan = milestoneEventArgs.Loan;
            string currentMilestone = loan.Fields["Log.MS.Stage"].ToString();
            string titleCompany = loan.Fields["411"].ToString();
            if (titleCompany.Contains("Silk"))
            {
                switch (currentMilestone.ToLower())
                {
                    case "loan setup phase 1":
                        bool orderTitleResponse = OrderSilkTitle(loan);
                        if (orderTitleResponse)
                        {
                            bool sendOrderDocsToSilk = PostPreliminaryTitleOrderDocsToSilk(loan);
                        }

                        break;
                }
                
            }
            
        }

        private bool PostPreliminaryTitleOrderDocsToSilk(Loan loan)
        {
            string postDocToSilkUri = WcmSettings.PostDocToSilk;
            //grab 1003 and title quote from eFolder
            List<B64SilkDocument> docsToPost = new List<B64SilkDocument>();

            Attachment signedAppFromEncompass =  GetLatestAttachment(loan, "Disclosures - Signed 1003");

            B64SilkDocument signedApp = new B64SilkDocument
            {
                Name = signedAppFromEncompass.Title,
                Base64EncodedSilkDocument = Convert.ToBase64String(signedAppFromEncompass.DataOriginal),
                FieldIdToSet = loan.Fields["CX.SILK.1003.UPLOAD.DATE"]
            };

            docsToPost.Add(signedApp);

            Attachment titleQuoteFromEncompass = GetLatestAttachment(loan, "Title Fee Estimate");

            B64SilkDocument titleQuote = new B64SilkDocument
            {
                Name = titleQuoteFromEncompass.Title,
                Base64EncodedSilkDocument = Convert.ToBase64String(titleQuoteFromEncompass.DataOriginal),
                FieldIdToSet = loan.Fields["CX.SILK.FEE.UPLOAD.DATE"]
            };
            
            docsToPost.Add(titleQuote);

            foreach (B64SilkDocument doc in docsToPost)
            {
                var docRequest = SilkUtility.MapLoanToPostSilkDocumentRequest(loan, doc);

                var docRepsonse = SilkUtility.PostDocumentToSilk(postDocToSilkUri, docRequest);
                if (!docRepsonse.WasSuccessful)
                {
                    //need to do something if not successful
                    loan.Fields["CX.SILK.DOC.UPLOAD.ERRORMSG"].Value = docRepsonse.ErrorMessage;
                    return false;
                }
                else
                {
                    doc.FieldIdToSet.Value = DateTime.Now;
                   
                }

            }

            return true;

        }

        private Attachment GetLatestAttachment(Loan theLoan, string trackedDocumentName)
        {
            LogEntryList trackedDocuments = theLoan.Log.TrackedDocuments.GetDocumentsByTitle(trackedDocumentName);

            List<Attachment> attachments = new List<Attachment>();

            foreach (TrackedDocument doc in trackedDocuments)
            {
                AttachmentList attList = doc.GetAttachments();
                foreach (Attachment att in attList)
                {
                    attachments.Add(att);
                }

            }
            
            return attachments.OrderByDescending(a => a.Date).FirstOrDefault();
        }

        private bool OrderSilkTitle(Loan loan)
        {
            //build title order
            var titleRequest = SilkUtility.MapLoanToSilkTitleOrderRequest(loan);

            //send to silk

            var silkTitleContainerUri = WcmSettings.PostTitleOrderToSilk;

            var titleResponse = SilkUtility.PostTitleOrderToSilk(loan, silkTitleContainerUri, titleRequest);

            if (!titleResponse.WasSuccessful)
            {
                throw new Exception("Error posting title Order to Silk" + $"Error:{titleResponse.ErrorMessage}." +
                                    Environment.NewLine +
                                    "Please submit a Help Desk Ticket");
                
            }

            return true;
        }
    }
}
