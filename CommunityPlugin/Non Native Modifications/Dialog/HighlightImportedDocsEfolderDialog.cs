using CommunityPlugin.Objects;
using CommunityPlugin.Objects.Args;
using CommunityPlugin.Objects.Helpers;
using CommunityPlugin.Objects.Interface;
using CommunityPlugin.Objects.Models;
using EllieMae.EMLite.UI;
using EllieMae.Encompass.Automation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Words;
using CommunityPlugin.Objects.Models.WCM.DocumentImporter;
using Elli.Common;
using EllieMae.EMLite.ClientServer.eFolder;
using EllieMae.EMLite.DataEngine.Log;
using EllieMae.Encompass.BusinessObjects.Loans.Logging;
using Newtonsoft.Json;
using WyndhamLib.Authentication;
using WyndhamLibGlobal.BlendPortal.MicroServiceModels;

namespace CommunityPlugin.Non_Native_Modifications.Dialog
{
    public class HighlightImportedDocsEfolderDialog : Plugin, ILogin, ILoanOpened
    {
        private Dictionary<int, ExternalSource> _externalSourcesDictionary = null;
        private static readonly WcmSettings WcmSettings = CDOHelper.CDO.CommunitySettings.WcmSettings;
        public ImportedDocumentsLoanCdo ImportedDocsLoanCdo = null;

        public override bool Authorized()
        {
            return PluginAccess.CheckAccess(nameof(HighlightImportedDocsEfolderDialog));
        }

        public override void Login(object sender, EventArgs e)
        {
            GetExternalSources();

            FormWrapper.FormOpened += FormWrapper_FormOpened;
        }

        public override void LoanOpened(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                ImportedDocsLoanCdo =
                    LoanCdoHelper.GetLoanCustomDataObjectValue<ImportedDocumentsLoanCdo>(EncompassApplication.CurrentLoan,
                        "WCM.DocumentImporterDocs.json");


            });
        }

        private void GetExternalSources()
        {
            Task taxTask = new TaskFactory().StartNew(async () =>
            {
                string url = WcmSettings.GetDocumentImporterSourcesUrl;
                HttpResponseMessage httpResponse = WyndhamClientManager.GetAuthHttpClient().GetResponseMessage(url);
                var rawJson = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    var externalImportSources = JsonConvert.DeserializeObject<List<ExternalSource>>(rawJson);

                    _externalSourcesDictionary = externalImportSources.ToDictionary(x => x.Id, x => x);
                }
                else
                {
                    Macro.Alert($"Major error getting Document Importer Settings. " +
                                "Please restart Encompass and if the error persists submit a help desk ticket. " +
                                $"{Environment.NewLine}{Environment.NewLine}Status Code: '{httpResponse.StatusCode}'");
                }


            }).ContinueWith((x) =>
            {

                foreach (var externalSource in _externalSourcesDictionary)
                {
                    externalSource.Value.AlreadyViewedDocumentColor = ColorTranslator.FromHtml($"#{externalSource.Value.AlreadyViewedDocHexColor}");
                    externalSource.Value.NewDocumentColor = ColorTranslator.FromHtml($"#{externalSource.Value.NewDocumentHexColor}");

                }
            });
        }

        private void FormWrapper_FormOpened(object sender, FormOpenedArgs e)
        {
            if (e.OpenForm.Name.Equals("efolderdialog", StringComparison.OrdinalIgnoreCase))
            {
                if (ImportedDocsLoanCdo != null && _externalSourcesDictionary != null)
                {
                    this.HighlightEfolderDocs(e.OpenForm);
                }
            }
            else if (e.OpenForm.Name.Equals("DocumentDetailsDialog", StringComparison.OrdinalIgnoreCase))
            {
                this.HighlightDocumentAttachments(e.OpenForm);
            }
            else if (e.OpenForm.Name.Equals("AllFilesDialog", StringComparison.OrdinalIgnoreCase))
            {
                this.HighlightFilesUnassigned(e.OpenForm);
            }
        }

        private void HighlightEfolderDocs(Form eFolderDialogForm)
        {

            var documentGrid = eFolderDialogForm.Controls.Find("gvDocuments", true).FirstOrDefault() as GridView;

            if (documentGrid != null)
            {

                IDictionary<GVItem, DocumentLog> documentDictionary = new Dictionary<GVItem, DocumentLog>();

                foreach (GVItem row in documentGrid.Items)
                {
                    var document = (EllieMae.EMLite.DataEngine.Log.DocumentLog)row.Tag;
                    documentDictionary.Add(row, document);
                }

                foreach (var importedDocument in ImportedDocsLoanCdo.Documents)
                {
                    var matchingEfolderDoc =
                        documentDictionary
                            .FirstOrDefault(x => x.Value.Guid.Equals(importedDocument.EncompassEfolderId));

                    // if matching efolder is found
                    if (matchingEfolderDoc.Key != null)
                    {
                        var externalSource =
                            _externalSourcesDictionary.FirstOrDefault(x =>
                                x.Key.Equals(importedDocument.ExternalSourceId));

                        if (externalSource.Value != null)
                        {
                            var doc = matchingEfolderDoc.Value;
                            var gridRow = matchingEfolderDoc.Key;
                            if (doc.DateAccessed < doc.DateReceived)
                            {
                                gridRow.BackColor = externalSource.Value.NewDocumentColor;
                            }
                            else
                            {
                                gridRow.BackColor = externalSource.Value.AlreadyViewedDocumentColor;
                            }
                        }
                    }
                }
            }


            //if (Directory.Exists(@"c:\temp\Trapeze"))
            //{
            //    string output = JsonConvert.SerializeObject(export, Formatting.Indented);
            //    string fileName =
            //        $"C:\\temp\\Trapeze\\EfolderDocumentIdsExport_{EncompassApplication.CurrentLoan.LoanNumber}-{DateTime.Now:MM-dd-yyyy_hh-mm-tt}.json";
            //    File.WriteAllText(fileName, output);
            //}

        }

        private void HighlightFilesUnassigned(Form documentDialogForm)
        {

            List<object> export = new List<object>();

            var fileGrid = documentDialogForm.Controls.Find("gvUnassigned", true).FirstOrDefault() as GridView;
            if (fileGrid != null)
            {
                IDictionary<GVItem, NativeAttachment> attachmentDictionary = new Dictionary<GVItem, NativeAttachment>();

                foreach (GVItem row in fileGrid.Items)
                {
                    var attachment = (NativeAttachment)row.Tag;
                    attachmentDictionary.Add(row, attachment);
                }

                foreach (var importedDocument in ImportedDocsLoanCdo.Documents)
                {
                    var matchingAttachment =
                        attachmentDictionary
                            .FirstOrDefault(x => x.Value.ID.Equals(importedDocument.EncompassAttachmentId));

                    // if matching file is found from loan cdo, highlight row!
                    if (matchingAttachment.Key != null)
                    {

                        var externalSource =
                            _externalSourcesDictionary.FirstOrDefault(x =>
                                x.Key.Equals(importedDocument.ExternalSourceId));

                        if (externalSource.Value != null)
                        {
                            var gridRow = matchingAttachment.Key;
                            gridRow.BackColor = externalSource.Value.AlreadyViewedDocumentColor;
                        }

                    }
                }
            }


            if (Directory.Exists(@"c:\temp\Trapeze"))
            {
                string output = JsonConvert.SerializeObject(export,
                    Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                string fileName =
                    $"C:\\temp\\Trapeze\\FilesUnassignedGridViewExport_{EncompassApplication.CurrentLoan.LoanNumber}-{DateTime.Now:MM-dd-yyyy_hh-mm-tt}.json";
                File.WriteAllText(fileName, output);
            }

        }

        private void HighlightDocumentAttachments(Form documentDialogForm)
        {

            //MetaDataExporting export = new MetaDataExporting();

            var attachmentGrid = documentDialogForm.Controls.Find("gvFiles", true).FirstOrDefault() as GridView;
            if (attachmentGrid != null)
            {
                IDictionary<GVItem, NativeAttachment> attachmentDictionary = new Dictionary<GVItem, NativeAttachment>();

                foreach (GVItem row in attachmentGrid.Items)
                {
                    var attachment = (NativeAttachment)row.Tag;
                    attachmentDictionary.Add(row, attachment);
                }

                foreach (var importedDocument in ImportedDocsLoanCdo.Documents)
                {
                    var matchingAttachment =
                        attachmentDictionary
                            .FirstOrDefault(x => x.Value.ID.Equals(importedDocument.EncompassAttachmentId));

                    // if matching efolder is found
                    if (matchingAttachment.Key != null)
                    {

                        var externalSource =
                            _externalSourcesDictionary.FirstOrDefault(x =>
                                x.Key.Equals(importedDocument.ExternalSourceId));

                        if (externalSource.Value != null)
                        {
                            var gridRow = matchingAttachment.Key;
                            gridRow.BackColor = externalSource.Value.AlreadyViewedDocumentColor;
                        }

                    }
                }
            }


            //if (Directory.Exists(@"c:\temp\Trapeze"))
            //{
            //    string output = JsonConvert.SerializeObject(
            //        export,
            //        Formatting.Indented,
            //        new JsonSerializerSettings
            //        {
            //            NullValueHandling = NullValueHandling.Ignore
            //        });

            //    string fileName =
            //        $"C:\\temp\\Trapeze\\AttachmentsExport_{EncompassApplication.CurrentLoan.LoanNumber}-{DateTime.Now:MM-dd-yyyy_hh-mm-tt}.json";
            //    File.WriteAllText(fileName, output);
            //}

        }

    }


}
