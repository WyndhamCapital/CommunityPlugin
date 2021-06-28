using CommunityPlugin.Objects.Helpers;
using CommunityPlugin.Objects.Models;
using CommunityPlugin.Objects.Models.WCM.DocumentMapper;
using EllieMae.Encompass.Automation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunityPlugin.Objects.Models.WCM.Helpers;
using EllieMae.EMLite.DataEngine;
using WyndhamLib.Authentication;
using WyndhamLibGlobal;

namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    public partial class DocumentMapperTableForm : Form
    {
        private static readonly WcmSettings WcmSettings = CDOHelper.CDO.CommunitySettings.WcmSettings;
        private static List<ExternalDocumentSource> _externalImportSources;
        private static List<Document> _mapperDocuments = new List<Document>();
        private CustomFieldsInfo _encompassCustomFields;
        private FieldDefinitionCollection _encompassStandardFields;
        public DocumentMapperTableForm()
        {
            InitializeComponent();
        }

        private void DocumentMapper_Form_Load(object sender, EventArgs e)
        {
            LoadExternalSoures();

            LoadEncompassFields();
        }

        private void LoadEncompassFields()
        {
            new TaskFactory().StartNew(() =>
            {
                _encompassCustomFields = EncompassHelper.SessionObjects.ConfigurationManager.GetLoanCustomFields();
            });

            new TaskFactory().StartNew(() =>
            {
                _encompassStandardFields =
                    EncompassHelper.SessionObjects.LoanManager.GetStandardFields().AllFields;
            });
        }

        private void LoadExternalSoures()
        {
            var waitDialog = new PleaseWaitDialog(this);

            new TaskFactory().StartNew(async () =>
            {
                waitDialog.Progress.Report("Loading Mapper Sources...");
                await GetExternalSourcesAsync();

            }).ContinueWith((x) =>
            {
                LoadExternalSourcesInDropDown();
                waitDialog.PleaseWaitForm.Close();
            });
        }



        private void LoadExternalSourcesInDropDown()
        {
            // Remove the handler for the SelectedIndex_Changed event, bind your data, then add the handler back.
            // if handler isn't removed, loading the scenario names will trigger the event to load that scenario
            this.externalSourcesComboBox.SelectedIndexChanged -= new System.EventHandler(this.ExternalSourcesComboBox_SelectedIndexChanged);

            externalSourcesComboBox.DataSource = null;

            externalSourcesComboBox.Refresh();

            externalSourcesComboBox.DisplayMember = "SourceName";
            externalSourcesComboBox.ValueMember = "Id";
            externalSourcesComboBox.DataSource = _externalImportSources;

            externalSourcesComboBox.SelectedIndex = -1;

            this.externalSourcesComboBox.SelectedIndexChanged += new System.EventHandler(this.ExternalSourcesComboBox_SelectedIndexChanged);

            externalSourcesComboBox.Refresh();
        }

        private void ExternalSourcesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            var waitDialog = new PleaseWaitDialog(this);

            ExternalDocumentSource selectedSource = GetExternalSourceSelected();
            waitDialog.Progress.Report($"Retrieving docs for '{selectedSource.SourceName}'...");

            List<Document> docs = WcmHelpers.GetMapperDocuments(selectedSource.Id, WcmSettings);

            _mapperDocuments = docs;
            UpdateDocsGridview(docs);
            EnableControls();

            waitDialog.PleaseWaitForm.Close();

            //new TaskFactory().StartNew(() =>
            //{
            //    ExternalDocumentSource selectedSource = GetExternalSourceSelected();
            //    waitDialog.Progress.Report($"Retrieving docs for '{selectedSource.SourceName}'...");

            //    docs = WcmHelpers.GetMapperDocuments(selectedSource.Id, WcmSettings);

            //}).ContinueWith((x) =>
            //{
            //    waitDialog.PleaseWaitForm.Close();

            //    _mapperDocuments = docs;

            //    UpdateDocsGridview(docs);
            //    EnableControls();

            //});

        }

        private void EnableControls()
        {
            searchTextBox.Enabled = true;
            enabledOnlyDocsCheckBox.Enabled = true;
        }

        private ExternalDocumentSource GetExternalSourceSelected()
        {
            var selectedItem = externalSourcesComboBox.SelectedItem;
            if (selectedItem == null)
            {
                return null;
            }

            return (ExternalDocumentSource)externalSourcesComboBox.SelectedItem;
        }

        private void UpdateDocsGridview(List<Document> docs)
        {
            documentsDataGridView.DataSource = null;
            documentsDataGridView.DataSource = BuildDataTable(docs);

            // Automatically resize the visible rows.
            documentsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Automatically resize the visible rows.
            documentsDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

            documentsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }


        private static DataTable BuildDataTable(List<Document> docs)
        {
            var columns = from t in docs
                          select new
                          {
                              ID = t.Id,
                              ExternalDoc = t.ExternalSystemDocumentId,
                              Efolder = t.EncompassEfolderName,
                              Field_Mappings = t.FieldMappings.Count(),
                              Enable = t.Enable
                          };

            return ExtensionMethods.ToDataTable(columns.ToList());
        }


        private async System.Threading.Tasks.Task GetExternalSourcesAsync()
        {
            try
            {
                HttpResponseMessage httpResponse = WyndhamClientManager.GetAuthHttpClient().GetResponseMessage(WcmSettings.GetDocumentMapperExternalSourcesUrl);
                var rawJson = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    _externalImportSources = JsonConvert.DeserializeObject<List<ExternalDocumentSource>>(rawJson);
                }
                else
                {
                    throw new Exception($"Major error getting Document Importer Settings. " +
                                 "Please restart Encompass and if the error persists submit a help desk ticket. " +
                                 $"{Environment.NewLine}{Environment.NewLine}Status Code: '{httpResponse.StatusCode}'");
                }

            }
            catch (Exception e)
            {
                MessageBox.Show($"Error: {e.ToString()}");
                throw;
            }
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            var docs = SearchDocs();
            UpdateDocsGridview(docs);
        }

        private void documentsDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            string errorMsg = null;
            int? selectedDocId = GetCurrentSelectedDocId(out errorMsg);

            if (selectedDocId.HasValue == false)
            {
                MessageBox.Show(errorMsg);
                return;
            }

            var selectedDoc = _mapperDocuments.FirstOrDefault(x => x.Id.Equals(selectedDocId.Value));
            if (selectedDoc == null)
            {
                MessageBox.Show($"Error. No Doc found with id '{selectedDocId}'. Restart Encompass please!");
                return;
            }

            using (var editDocForm = new DocumentMapperSingleDocForm(selectedDoc, WcmSettings, _encompassCustomFields, _encompassStandardFields))
            {
                if (editDocForm.ShowDialog((IWin32Window)this.ParentForm) != DialogResult.OK)
                    return;

                this.UpdateDocumentInGridview(editDocForm.TheDocument);
            }


        }

        private void UpdateDocumentInGridview(Document docToUpdate)
        {
            // SP - if the doc already exists in list, remove current, and add new/updated doc
            var currentDoc = _mapperDocuments.FirstOrDefault(x => x.Id.Equals(docToUpdate.Id));
            if (currentDoc != null)
                _mapperDocuments.Remove(currentDoc);

            _mapperDocuments.Add(docToUpdate);

            UpdateDocsGridview(_mapperDocuments);
        }

        private int? GetCurrentSelectedDocId(out string errorMsg)
        {
            errorMsg = null;
            var rowSelected = GridViewHelper.GetCurrentGridRow(documentsDataGridView, out errorMsg);

            if (rowSelected == null)
            {
                errorMsg = ($"Error Getting Row: '{errorMsg}'");
                return null; ;
            }

            var documentIdString = rowSelected.Cells["ID"].Value.ToString();

            int selectedDocId;
            var canCovertDocsId = int.TryParse(documentIdString, out selectedDocId);

            if (canCovertDocsId == false)
            {
                errorMsg = ($"Error convering Document ID to int '{documentIdString}'");
                return null;
            }

            return selectedDocId;
        }

        private void addNewDocument_Click(object sender, EventArgs e)
        {
            var sourceSelected = GetExternalSourceSelected();
            if (sourceSelected == null)
            {
                MessageBox.Show("You must select an external source before creating a Document!");
                return;
            }

            using (var editDocForm = new DocumentMapperSingleDocForm(
                sourceSelected.Id,
                WcmSettings,
                _encompassCustomFields,
                _encompassStandardFields))
            {
                if (editDocForm.ShowDialog((IWin32Window)this.ParentForm) != DialogResult.OK)
                    return;

                this.UpdateDocumentInGridview(editDocForm.TheDocument);
            }
        }

        private void enabledOnlyDocsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var docs = SearchDocs();
            UpdateDocsGridview(docs);
        }

        private List<Document> SearchDocs()
        {
            List<Document> response = _mapperDocuments;

            if (enabledOnlyDocsCheckBox.Checked)
            {
                response = response
                        .Where(x => x.Enable.Equals(enabledOnlyDocsCheckBox.Checked)).ToList();
            }

            string searchWord = searchTextBox.Text.ToLower();
            if (string.IsNullOrEmpty(searchWord) == false)
            {
                response = response
                   .Where(x => x.EncompassEfolderName.ToLower().Contains(searchWord) ||
                   x.ExternalSystemDocumentId.ToLower().Contains(searchWord)).ToList();
            }

            return response;
        }
    }


}
