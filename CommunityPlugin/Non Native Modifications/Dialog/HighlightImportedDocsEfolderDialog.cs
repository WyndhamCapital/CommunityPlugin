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
using CommunityPlugin.Objects.Models.WCM.DocumentImporter;
using CommunityPlugin.Objects.Models.WCM.DocumentImporter.UI;
using Elli.Common;
using EllieMae.EMLite.ClientServer.eFolder;
using EllieMae.EMLite.DataEngine.Log;
using EllieMae.Encompass.BusinessObjects.Loans.Logging;
using Newtonsoft.Json;
using WyndhamLib.Authentication;
using Form = System.Windows.Forms.Form;
using EllieMae.Encompass.BusinessObjects.Loans;
using EllieMae.Encompass.Collections;

namespace CommunityPlugin.Non_Native_Modifications.Dialog
{
    public class HighlightImportedDocsEfolderDialog : Plugin, ILogin, ILoanOpened, ILogEntryChanged
    {
        private Dictionary<int, ExternalSource> _externalSourcesDictionary = null;
        private static readonly WcmSettings WcmSettings = CDOHelper.CDO.CommunitySettings.WcmSettings;
        private static readonly string ImportedDocsLoanCdoName = "WCM.DocumentImporterDocs.json";

        TrackedDoc _trackedDocLastUpdated = new TrackedDoc();
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

            // SP - when loan opens, get loan cdo that contains list of docs imported
            // on a new thread so doesn't slow loan open down
            Task.Factory.StartNew(() =>
            {
                ImportedDocsLoanCdo =
                    LoanCdoHelper.GetLoanCustomDataObjectValue<ImportedDocumentsLoanCdo>(EncompassApplication.CurrentLoan,
                        ImportedDocsLoanCdoName);
            });

        }

        public override void LogEntryChanged(object sender, LogEntryEventArgs e)
        {


            var entry = e.LogEntry;

            // SP - whenever a new attachment is added to a tracked doc, 
            // we need to check if this attachment was from AI and if the tracked doc no longer needs to be highlighted
            if (entry != null && (entry.EntryType.Equals(LogEntryType.TrackedDocument)))
            {
                // SP - any updates to loan CDO won't affect UI or user until next time they load eFolder view
                // run on a new thread so UI isn't delayed
                Task.Factory.StartNew(() =>
                {
                    UpdateImportedDocsLoanCdoHighlighting(entry);
                });
            }
        }

        private void UpdateImportedDocsLoanCdoHighlighting(LogEntry entry)
        {
            if (ImportedDocsLoanCdo == null)
                return;

            var trackedDocumemnt = (TrackedDocument)entry;

            var matchingTrackedDocContainingDocImports =
                ImportedDocsLoanCdo.Documents
                    .Where(x => string.IsNullOrEmpty(x.EncompassEfolderId) == false
                    && x.EncompassEfolderId.Equals(trackedDocumemnt.ID)
                    && x.EnableHighlighting).ToList();

            // SP - if this tracked doc doesn't contain any imported docs, it is of no concern
            // go to next
            if (matchingTrackedDocContainingDocImports.Any() == false)
                return;

            List<EllieMae.Encompass.BusinessObjects.Loans.Attachment> attachmentList = new List<EllieMae.Encompass.BusinessObjects.Loans.Attachment>();
            AttachmentList attachments = trackedDocumemnt.GetAttachments();
            for (int i = 0; i < attachments.Count; i++)
            {
                attachmentList.Add(attachments[i]);
            }


            var importedAttachmentGuids =
                matchingTrackedDocContainingDocImports.Select(x => x.EncompassAttachmentId).ToList();

            var currentAttachmentGuids = attachmentList.Select(x => x.Name).ToList();

            List<string> attachmentGuidsThatStillNeedHighlighting =
                importedAttachmentGuids
                .Intersect(currentAttachmentGuids)
                .ToList();

            if (attachmentGuidsThatStillNeedHighlighting.Any() == false)
            {
                // SP - when 1 file attachment moves to a new tracked doc
                // there will be multiple events thrown for every property that is updated
                // i.e. received date, receieved user, etc will all get an event thrown

                // checks if this is the same tracked doc we just updated last time
                var currentTrackedDoc = new TrackedDoc(trackedDocumemnt.ID, currentAttachmentGuids);
                if (HasTrackedDocumentChanged(currentTrackedDoc))
                {
                    // if there are no duplicates, go update the cdo so that any docs where tracked doc
                    // is this guid, set enable highlight to FALSE. 
                    UnhighlightImportedDocumentsLoanCdo(trackedDocumemnt);

                    _trackedDocLastUpdated = currentTrackedDoc;
                }
            }
        }

        private bool HasTrackedDocumentChanged(TrackedDoc trackedDocumemnt)
        {
            if (_trackedDocLastUpdated == null) { return true; }

            if (trackedDocumemnt.Equals(_trackedDocLastUpdated))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void UnhighlightImportedDocumentsLoanCdo(TrackedDocument trackedDocumemnt)
        {

            // set 'enable highlighting' to false for docs in this tracked doc
            ImportedDocsLoanCdo.Documents
                .Where(x => string.IsNullOrEmpty(x.EncompassEfolderId) == false
                && x.EncompassEfolderId == trackedDocumemnt.ID)
                .ToList().ForEach(x => x.EnableHighlighting = false);


            LoanCdoHelper.SaveObjectToJsonCDO(
                EncompassApplication.CurrentLoan,
                ImportedDocsLoanCdoName,
               ImportedDocsLoanCdo);
        }

        private void GetExternalSources()
        {
            try
            {
                Task task = new TaskFactory().StartNew(async () =>
                {
                    List<ExternalSource> response = await WcmHelpers.GetDocumentImporterExternalSourcesAsync(WcmSettings);

                    _externalSourcesDictionary = response.ToDictionary(x => x.Id, x => x);

                }).ContinueWith((x) =>
                {
                    if (_externalSourcesDictionary != null)
                    {
                        foreach (var externalSource in _externalSourcesDictionary)
                        {
                            externalSource.Value.AlreadyViewedDocumentColor = ColorTranslator.FromHtml($"#{externalSource.Value.AlreadyViewedDocHexColor}");
                            externalSource.Value.NewDocumentColor = ColorTranslator.FromHtml($"#{externalSource.Value.NewDocumentHexColor}");

                        }
                    }
                });
            }
            catch (Exception e)
            {

                Macro.Alert($"Error: {e.ToString()}");
            }
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
            else if (e.OpenForm.Name.Equals("AllFilesDialog", StringComparison.OrdinalIgnoreCase))
            {
                this.HighlightFilesUnassigned(e.OpenForm);
            }

            // SP - when we tried to add column to doc gridview, it would throw an exception when a user
            // edited the columns via EM native function -- form below represents that 'edit column' panel
            // tried to find the controls for that panel, but didn't end up moving forward with it
            //else if (e.OpenForm.Name.Equals("TableLayoutColumnSelector", StringComparison.OrdinalIgnoreCase))
            //{
            //    ExportTableLaoutColumnSelectorControls(e.OpenForm);
            //}
        }

        private void HighlightEfolderDocs(Form eFolderDialogForm)
        {
            GridView documentGrid = eFolderDialogForm.Controls.Find("gvDocuments", true).FirstOrDefault() as GridView;

            if (documentGrid != null)
            {
                // SP - add column to efolder code below -- currently will exception out if a new column is added to dialog...
                #region Add Column to GV
                //GVColumn aiColumn = documentGrid.Columns.FirstOrDefault(x => x.Name.Equals("AI"));
                //if (aiColumn != null)
                //    documentGrid.Columns.Remove(aiColumn);
                //aiColumn = new GVColumn("AI");
                //documentGrid.Columns.Add(aiColumn);



                #endregion

                IDictionary<GVItem, DocumentLog> efolderDocDictionary =
                    GridViewHelper.GetAllEncompassGridViewItems<DocumentLog>(documentGrid);

                foreach (var importedDocument in ImportedDocsLoanCdo.Documents)
                {
                    // SP - we only need to highlight docs where enabled is set to T
                    if (importedDocument.EnableHighlighting == false)
                        continue;

                    var efolderDocDetails = GetMatchingEfolderDocFromUi(importedDocument, efolderDocDictionary);
                    if (efolderDocDetails.HighlightEfolderRow)
                    {
                        HighlightEfolderDoc(efolderDocDetails);

                        #region SP - this is where you would set column
                        //// gridRow.SubItems[aiColumn.Index].Text = "Y";
                        // gridRow.SubItems[aiColumn.Name].Text = "Y";
                        #endregion

                    }
                }
            }



        }

        private void HighlightEfolderDoc(ImportedDocUIDocument efolderDoc)
        {
            if (efolderDoc.EfolderDocumentDetails.DateAccessed < efolderDoc.EfolderDocumentDetails.DateReceived)
            {
                efolderDoc.EfolderGridViewRow.BackColor = efolderDoc.DocumentSource.NewDocumentColor;
            }
            else
            {
                efolderDoc.EfolderGridViewRow.BackColor = efolderDoc.DocumentSource.AlreadyViewedDocumentColor;
            }
        }

        private ImportedDocUIDocument GetMatchingEfolderDocFromUi(ImportedDocument importedDocument,
            IDictionary<GVItem, DocumentLog> efolderDocDictionary)
        {

            ImportedDocUIDocument result = new ImportedDocUIDocument();

            var matchingEfolderDoc =
                efolderDocDictionary
                    .FirstOrDefault(x => x.Value.Guid.Equals(importedDocument.EncompassEfolderId));

            // if matching efolder is found (if not found, efolder has been deleted by end user)
            if (matchingEfolderDoc.Key == null)
            {
                result.HighlightEfolderRow = false;
                return result;
            }


            var externalSource =
                    _externalSourcesDictionary.FirstOrDefault(x =>
                        x.Key.Equals(importedDocument.ExternalSourceId));

            if (externalSource.Value == null)
            {
                result.HighlightEfolderRow = false;
                return result;
            }

            result.HighlightEfolderRow = true;

            result.DocumentSource = externalSource.Value;
            result.EfolderDocumentDetails = matchingEfolderDoc.Value;
            result.EfolderGridViewRow = matchingEfolderDoc.Key;

            return result;
        }



        private ImportedDocUIDocument GetMatchingFileUnassignedAttachmentFromUi(ImportedDocument importedDocument,
            IDictionary<GVItem, ImageAttachment> fileManagerAttachmentDictionary)
        {

            ImportedDocUIDocument result = new ImportedDocUIDocument();

            var matchingAttachment =
                fileManagerAttachmentDictionary
                    .FirstOrDefault(x => x.Value.ID.Equals(importedDocument.EncompassAttachmentId, StringComparison.OrdinalIgnoreCase));

            // if matching file is found from loan cdo, highlight row!
            if (matchingAttachment.Key == null)
            {
                result.HighlightEfolderRow = false;
                return result;
            }

            var externalSource =
                    _externalSourcesDictionary.FirstOrDefault(x =>
                        x.Key.Equals(importedDocument.ExternalSourceId));

            if (externalSource.Value == null)
            {
                result.HighlightEfolderRow = false;
                return result;
            }

            result.HighlightEfolderRow = true;
            result.DocumentSource = externalSource.Value;
            result.EfolderGridViewRow = matchingAttachment.Key;

            return result;
        }

        private void HighlightFilesUnassigned(Form documentDialogForm)
        {
            var fileGrid = documentDialogForm.Controls.Find("gvUnassigned", true).FirstOrDefault() as GridView;
            if (fileGrid != null)
            {
                IDictionary<GVItem, ImageAttachment> attachmentDictionary =
                GridViewHelper.GetAllEncompassGridViewItems<ImageAttachment>(fileGrid);

                var importedDocsUnassigned = ImportedDocsLoanCdo.Documents
                     .Where(x => string.IsNullOrEmpty(x.EncompassEfolderId)).ToList();

                foreach (var importedDocument in importedDocsUnassigned)
                {
                    var attachmentDetails = GetMatchingFileUnassignedAttachmentFromUi(importedDocument, attachmentDictionary);
                    if (attachmentDetails.HighlightEfolderRow)
                    {
                        attachmentDetails.EfolderGridViewRow.BackColor =
                            attachmentDetails.DocumentSource.AlreadyViewedDocumentColor;
                    }
                }
            }

        }


        ////**** Below is 100% discovery to find other uses in UI
        //__________________________________________________

        //// SP - this is 100% exploratory -- seeing all UI names to 'hijack' UI
        //private void ExportUiControlNames(Form eFolderDialogForm)
        //{
        //    List<ContextMenu> ctxtMenu = new List<ContextMenu>();
        //    var allControls = GetAllControls(eFolderDialogForm, out ctxtMenu);

        //    List<Tuple<string, string, string>> list = new List<Tuple<string, string, string>>();


        //    foreach (var control in allControls)
        //    {
        //        try
        //        {
        //            string text = string.IsNullOrEmpty(control.Text) ? "" : control.Text;
        //            list.Add(new Tuple<string, string, string>(control.Name, control.GetType().ToString(), text));


        //            if (control.GetType() == typeof(ContextMenuStrip))
        //            {
        //                var menu = (ContextMenuStrip)control;

        //                for (int i = 0; i < menu.Items.Count; i++)
        //                {
        //                    try
        //                    {
        //                        var item = menu.Items[i];
        //                        list.Add(new Tuple<string, string, string>(control.Name, item.Name, item.Text));

        //                    }
        //                    catch (Exception e)
        //                    {

        //                    }
        //                }

        //            }


        //        }
        //        catch (Exception e)
        //        {

        //        }
        //    }


        //    foreach (var control in ctxtMenu)
        //    {
        //        try
        //        {
        //            list.Add(new Tuple<string, string, string>(control.Name, control.GetType().ToString(), ""));
        //        }
        //        catch (Exception e)
        //        {

        //        }

        //        foreach (MenuItem item in control.MenuItems)
        //        {
        //            try
        //            {
        //                string text = string.IsNullOrEmpty(item.Text) ? "" : item.Text;
        //                list.Add(new Tuple<string, string, string>($"{control.Name}_{item.Name}", item.GetType().ToString(), text));
        //            }
        //            catch (Exception e)
        //            {
        //                ;
        //            }

        //        }

        //    }
        //    if (Directory.Exists(@"c:\temp\Trapeze"))
        //    {
        //        string output = JsonConvert.SerializeObject(list, Formatting.Indented);
        //        string fileName =
        //            $"C:\\temp\\Trapeze\\AllFormEfolderDialogControls_{DateTime.Now:MM-dd-yyyy_hh-mm-tt}.json";
        //        File.WriteAllText(fileName, output);
        //    }
        //}
        //private void ExportTableLaoutColumnSelectorControls(Form openForm)
        //{
        //    var controls = openForm.Controls;
        //    Dictionary<string, string> export = new Dictionary<string, string>();
        //    for (int i = 0; i < controls.Count; i++)
        //    {
        //        var ctrl = controls[i];
        //        export.Add(ctrl.Name, ctrl.GetType().ToString());
        //    }

        //    if (Directory.Exists(@"c:\temp\Trapeze"))
        //    {
        //        string output = JsonConvert.SerializeObject(export, Formatting.Indented);
        //        string fileName =
        //            $"C:\\temp\\Trapeze\\ColumnSelectorControls_{EncompassApplication.CurrentLoan.LoanNumber}-{DateTime.Now:MM-dd-yyyy_hh-mm-tt}.json";
        //        File.WriteAllText(fileName, output);
        //    }
        //}

        //public IEnumerable<System.Windows.Forms.Control> GetAll(System.Windows.Forms.Control control, Type type)
        //{
        //    var controls = control.Controls.Cast<System.Windows.Forms.Control>();

        //    return controls.SelectMany(ctrl => GetAll(ctrl, type))
        //        .Concat(controls)
        //        .Where(c => c.GetType() == type);
        //}

        //private List<System.Windows.Forms.Control> GetAllControls(System.Windows.Forms.Control container, out List<ContextMenu> contextMenu)
        //{
        //    List<System.Windows.Forms.Control> response = new List<System.Windows.Forms.Control>();
        //    contextMenu = new List<ContextMenu>();


        //    foreach (System.Windows.Forms.Control c in container.Controls)
        //    {

        //        response.Add(c);

        //        if (c.ContextMenuStrip != null)
        //        {
        //            response.Add(c.ContextMenuStrip);
        //        }

        //        if (c.ContextMenu != null)
        //        {
        //            contextMenu.Add(c.ContextMenu);
        //        }

        //        List<ContextMenu> ctxtMenu = new EmList<ContextMenu>();
        //        var controls = GetAllControls(c, out ctxtMenu);
        //        response.AddRange(controls);
        //        contextMenu.AddRange(ctxtMenu);

        //    }

        //    return response;
        //}

    }


}
