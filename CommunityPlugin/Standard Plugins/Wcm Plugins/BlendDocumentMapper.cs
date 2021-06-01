using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunityPlugin.Objects;
using CommunityPlugin.Objects.Helpers;
using CommunityPlugin.Objects.Interface;
using CommunityPlugin.Objects.Models;
using Elli.Common.Extensions;
using EllieMae.Encompass.Automation;
using EllieMae.Encompass.BusinessObjects.Loans;
using EllieMae.Encompass.BusinessObjects.Loans.Logging;
using EllieMae.Encompass.Collections;
using WyndhamLib.Authentication;
using WyndhamLibGlobal;
using WyndhamLibGlobal.BlendPortal.MicroServiceModels;

namespace CommunityPlugin.Standard_Plugins.Wcm_Plugins
{
    class BlendDocumentMapper : Plugin, ILoanOpened
    {
        private static readonly WcmSettings WcmSettings = CDOHelper.CDO.CommunitySettings.WcmSettings;
        public override bool Authorized()
        {
            return PluginAccess.CheckAccess(nameof(BlendDocumentMapper));
        }

        public override void LoanOpened(object sender, EventArgs e)
        {
            //string url = wcmSettings.GetDocumentImporterSourcesUrl;
            // HttpResponseMessage httpResponse = WyndhamClientManager.GetAuthHttpClient().GetResponseMessage(url);
            // var rawJson = await httpResponse.Content.ReadAsStringAsync();

            var currentLoan = EncompassApplication.CurrentLoan;
            var currentUser = EncompassApplication.CurrentUser;
            LoanLockList locks = currentLoan.GetCurrentLocks();

            //documents only need to be mapped if file starter is blend_api date is empty
            var fs = currentLoan.Fields["LoanTeamMember.UserID.File Starter"].ToString();
            var blendDocMapperCompleteDate = currentLoan.Fields["CX.BLEND.APP.DOCS.IMPORTED"];

            if (locks != null)
            {
                if (fs.Equals("blend_apitest") && blendDocMapperCompleteDate.IsEmpty())
                {
                    Task.Factory.StartNew(() =>
                    {
                        bool blendDocsMapped = DownloadAndMapBlendApplicationDocuments(currentLoan);
                    });
                }
            }


        }

        private bool DownloadAndMapBlendApplicationDocuments(Loan loan)
        {
            TrackedDocument eFolder = null;
            string blendLoanId = loan.Fields["CX.BLEND.LOANID"].ToString();
            List<BlendDocReturned> eFolderDocuments = new List<BlendDocReturned>();

            string url = WcmSettings.GetAllPortalDocumentsUri;
            GetAllPortalDocumentsResponse getAllDocsResponse = BlendUtility.GetAllBlendPortalDocuments(blendLoanId, url);

            

            try
            {
                foreach (var doc in getAllDocsResponse.documents)
                {
                    var docType = doc.type;
                    switch (docType)
                    {
                        case "CREDIT_REPORT":

                            eFolder = loan.Log.TrackedDocuments.Add("Credit Report", "Started");
                            var creditReportMapped = MapDocumentToEfolder(loan, doc, eFolder);
                            if (creditReportMapped)
                            {
                                DeleteAttachmentFromFileManager(loan, doc);
                            }

                            break;
                        case "VERIFICATION_OF_INCOME":
                            eFolder = loan.Log.TrackedDocuments.Add("Income - Written VOE", "Started");
                            var voiMapped = MapDocumentToEfolder(loan, doc, eFolder);
                            if (voiMapped)
                            {
                                DeleteAttachmentFromFileManager(loan, doc);
                            }

                            break;
                        case "GENERATED_ASSET_STATEMENT":

                            var assetPlaceholder = loan.Log.TrackedDocuments.GetDocumentsByTitle("Asset - Bank Statements - Chk/Svgs");
                            if (assetPlaceholder.Count > 0)
                            {
                                foreach (TrackedDocument assetDoc in assetPlaceholder)
                                {
                                    eFolder = assetDoc;
                                    break;
                                }
                            }
                            else
                            {
                                eFolder = loan.Log.TrackedDocuments.Add("Asset - Bank Statements - Chk/Svgs", "Started");
                            }
                            
                            var assetsMapped = MapDocumentToEfolder(loan, doc, eFolder);
                            if (assetsMapped)
                            {
                                DeleteAttachmentFromFileManager(loan, doc);
                            }

                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Macro.Alert($"Error mapping Blend Documents to the eFolder. Please submit a helpdesk Ticket." + Environment.NewLine + $"Error Message: {e.Message}");
                return false;
            }

            //once mapped set the date field
            loan.Fields["CX.BLEND.APP.DOCS.IMPORTED"].Value = DateTime.Now;
            return true;
        }

        private bool MapDocumentToEfolder(Loan loan, BlendDocReturned document, TrackedDocument eFolder)
        {
            string docId = document.id;

            try
            {
                var url = WcmSettings.GetDocumentFromBlendUri;

               
                GetDocumentResponse docResponse = BlendUtility.GetDocumentFromBlendPortal(docId, url);

                var data = new EllieMae.Encompass.BusinessObjects.DataObject(docResponse.DocumentData);
                EllieMae.Encompass.BusinessObjects.Loans.Attachment attachment = loan.Attachments.AddObject(data, ".pdf");
                attachment.Title = $"{document.name}";

                eFolder.Attach(attachment);
               
                // once attached go update the export status

                var uri = WcmSettings.UpdateDocExportStatusBlendUri;
                var updateRequest = new UpdateDocumentExportStatusRequest()
                {
                    BlendDocumentId = docId,
                    UtcDocumentExportTime = DateTime.UtcNow.ToShortDateString()
                };
                var updateResponse = BlendUtility.PostDocumentExportStatusUpdate(updateRequest, uri);
            }
            catch (Exception ex)
            {
                Macro.Alert($"Error mapping {document.name} to eFolder. Please submit a Help Desk Ticket." + Environment.NewLine + $"Error Message: {ex.Message}");
                return false;
            }

            return true;
        }

        private void DeleteAttachmentFromFileManager(Loan loan, BlendDocReturned document)
        {
            foreach (EllieMae.Encompass.BusinessObjects.Loans.Attachment att in loan.Attachments)
            {
                var attachmentTitle = att.Title;
                if (attachmentTitle.Equals(document.name))
                {
                    //check to make sure this document is not the one we just added to the eFolder
                    if (att.GetDocument() == null)
                    {

                        loan.Attachments.Remove(att);
                        return;
                    }
                }
            }
        }

        
    }
}
