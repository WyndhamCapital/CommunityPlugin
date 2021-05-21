using CommunityPlugin.Objects.Helpers;
using CommunityPlugin.Objects.Models;
using CommunityPlugin.Objects.Models.WCM.DocumentMapper;
using EllieMae.Encompass.Automation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using WyndhamLib.Authentication;

namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    public partial class DocumentMapper_Table_Form : Form
    {
        private static readonly WcmSettings WcmSettings = CDOHelper.CDO.CommunitySettings.WcmSettings;
        private static List<ExternalDocumentSource> ExternalImportSources;
        private static List<Document> MapperDocuments = new List<Document>();

        public DocumentMapper_Table_Form()
        {
            InitializeComponent();
        }

        private void DocumentMapper_Form_Load(object sender, EventArgs e)
        {
            LoadExternalSoures();
        }

        private void LoadExternalSoures()
        {
            Task loadSourcesTask = new TaskFactory().StartNew(async () =>
            {
                await GetExternalSourcesAsync();

            }).ContinueWith((x) =>
            {
                LoadExternalSourcesInDropDown();
            });
        }


        public static List<Document> GetMapperDocuments(ExternalDocumentSource sourceToRetrieve)
        {
            string finalUri = $"{WcmSettings.GetDocumentMapperDocumentsUrl}?externalSourceId={sourceToRetrieve.Id}&includeFieldMappings=true&enabledOnlyDocs=false";
            HttpResponseMessage httpResponse = WyndhamClientManager.GetAuthHttpClient().GetResponseMessage(finalUri);

            if (httpResponse.IsSuccessStatusCode)
            {
                Task<string> responseString = httpResponse.Content.ReadAsStringAsync();
                var docs = JsonConvert.DeserializeObject<List<Document>>(responseString.Result);
                MapperDocuments = docs;
                return docs;
            }
            else
            {
                throw new Exception($"Error retrieving external source documents!'{httpResponse.StatusCode}'");
            }
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
            externalSourcesComboBox.DataSource = ExternalImportSources;

            externalSourcesComboBox.SelectedIndex = -1;

            this.externalSourcesComboBox.SelectedIndexChanged += new System.EventHandler(this.ExternalSourcesComboBox_SelectedIndexChanged);

            externalSourcesComboBox.Refresh();
        }

        private void ExternalSourcesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExternalDocumentSource selectedSource = GetExternalSourceSelected();
                
            List<Document> docs = GetMapperDocuments(selectedSource);

            UpdateDocsGridview(docs);

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

          return  ExtensionMethods.ToDataTable(columns.ToList());
        }


        private async System.Threading.Tasks.Task GetExternalSourcesAsync()
        {
            try
            {

                HttpResponseMessage httpResponse = WyndhamClientManager.GetAuthHttpClient().GetResponseMessage(WcmSettings.GetDocumentMapperExternalSourcesUrl);
                var rawJson = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    ExternalImportSources = JsonConvert.DeserializeObject<List<ExternalDocumentSource>>(rawJson);
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
            string searchWord = searchTextBox.Text.ToLower();
            if (string.IsNullOrEmpty(searchWord))
            {
                UpdateDocsGridview(MapperDocuments);
            }
            else
            {
                var docs = MapperDocuments
                    .Where(x => x.EncompassEfolderName.ToLower().Contains(searchWord) ||
                    x.ExternalSystemDocumentId.ToLower().Contains(searchWord)).ToList();

                UpdateDocsGridview(docs);
            }
        }

        private void documentsDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            string errorMsg = null;
            int? selectedDocId = GetCurrentSelectedDocId(out errorMsg);

            if(selectedDocId.HasValue == false)
            {
                MessageBox.Show(errorMsg);
                return;
            }

            var selectedDoc = MapperDocuments.FirstOrDefault(x => x.Id.Equals(selectedDocId.Value));
            if(selectedDoc == null)
            {
                MessageBox.Show($"Error. No Doc found with id '{selectedDocId}'. Restart Encompass please!");
                return;
            }

            using (var editDocForm = new DocumentMapper_SingleDoc_Form(selectedDoc))
            {
                if (editDocForm.ShowDialog((IWin32Window)this.ParentForm) != DialogResult.OK)
                    return;

                Document updatedDoc = UpdateDocumentMapperDoc(editDocForm.TheDocument);

                this.UpdateDocumentInGridview(updatedDoc);
            }


        }

        private void UpdateDocumentInGridview(Document docToUpdate)
        {
            // SP - if the doc already exists in list, remove current, and add new/updated doc
            var currentDoc = MapperDocuments.FirstOrDefault(x => x.Id.Equals(docToUpdate.Id));
            if (currentDoc != null)
                MapperDocuments.Remove(currentDoc);

            MapperDocuments.Add(docToUpdate);

            UpdateDocsGridview(MapperDocuments);
        }

        private int? GetCurrentSelectedDocId(out string errorMsg)
        {
            errorMsg = null;
            var rowSelected = WcmHelpers.GetCurrentGridRow(documentsDataGridView, out errorMsg);

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

            using (var editDocForm = new DocumentMapper_SingleDoc_Form(sourceSelected.Id))
            {
                if (editDocForm.ShowDialog((IWin32Window)this.ParentForm) != DialogResult.OK)
                    return;

                //
                Document updatedDoc = UpdateDocumentMapperDoc(editDocForm.TheDocument);
                this.UpdateDocumentInGridview(updatedDoc);
            }
        }

        private Document UpdateDocumentMapperDoc(Document theDocument)
        {
            string url = $"{WcmSettings.UpdateDocumentMapperDocumentUrl}?userId={EncompassApplication.CurrentUser.ID}";
            string json = JsonConvert.SerializeObject(theDocument);

            var httpResponse = WyndhamClientManager.GetAuthHttpClient().Post(url, theDocument);
            if (httpResponse.IsSuccessStatusCode)
            {
                Task<string> responseString = httpResponse.Content.ReadAsStringAsync();
                var doc = JsonConvert.DeserializeObject<Document>(responseString.Result);
                return doc;
            }
            else
            {
                throw new Exception($"Error retrieving external source documents!'{httpResponse.StatusCode}'");
            }
        }
    }


}
