using CommunityPlugin.Objects.Helpers;
using CommunityPlugin.Objects.Models;
using CommunityPlugin.Objects.Models.Exceptions;
using CommunityPlugin.Objects.Models.WCM.DocumentMapper;
using EllieMae.Encompass.Automation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WyndhamLib.Authentication;

namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    public partial class DocumentMapper_SingleDoc_Form : Form
    {
        public Document TheDocument;
        private readonly int? ExternalSourceId;
        private readonly WcmSettings Settings;

        public DocumentMapper_SingleDoc_Form(Document document, WcmSettings wcmSettings)
        {
            InitializeComponent();

            Settings = wcmSettings;
            TheDocument = document;

            GridViewHelper.LoadFieldMappingColumns<FieldMapping>(fieldMappingsDataGridView);
            MapDocumentToUi();
        }






        public DocumentMapper_SingleDoc_Form(int externalSourceId, WcmSettings wcmSettings)
        {
            InitializeComponent();
            GridViewHelper.LoadFieldMappingColumns<FieldMapping>(fieldMappingsDataGridView);

            Settings = wcmSettings;
            ExternalSourceId = externalSourceId;
        }

        private void MapDocumentToUi()
        {
            eFolderName_textBox.Text = TheDocument.EncompassEfolderName;
            externalDocId_textBox.Text = TheDocument.ExternalSystemDocumentId;
            enableChckBox.Checked = TheDocument.Enable;

            GridViewHelper.LoadListObjectsToGridview(fieldMappingsDataGridView, TheDocument.FieldMappings);
        }




        //private void LoadFieldsGridview(IList<FieldMapping> fieldMappings)
        //{
        //    fieldMappingsDataGridView.Rows.Clear();

        //    foreach (var field in fieldMappings)
        //    {
        //        int fieldIndex = fieldMappingsDataGridView.Rows.Add(GridViewHelper.MapObjectToDataGridRowForUi(fieldMappingsDataGridView, field));
        //        fieldMappingsDataGridView.Rows[fieldIndex].Tag = field;
        //    }
        //}



        private FieldMapping MapDgRowToFieldDataObject(DataGridViewRow row)
        {
            FieldMapping result = null;

            if (row.Tag != null)
            {
                result = (FieldMapping)row.Tag;
            }
            else
            {
                // SP - means this is a newlyl created field data
                result = new FieldMapping();

                if (TheDocument != null)
                    result.DocumentId = TheDocument.Id;
            }

            foreach (PropertyInfo propertyInfo in result.GetType().GetProperties())
            {
                // SP - check if a column with this name exists first
                var column = GridViewHelper.GetColumnByName(fieldMappingsDataGridView, propertyInfo.Name);
                if (column != null)
                {
                    var value = row.Cells[propertyInfo.Name].Value;
                    if (propertyInfo != null && propertyInfo.CanWrite)
                    {
                        if (value != null && value != System.DBNull.Value)
                            propertyInfo.SetValue(result, value, null);
                    }
                }
            }


            return result;
        }


        private void button_ApplyChanges_Click(object sender, EventArgs e)
        {
            if (!this.ValidateContents())
                return;

            this.MapUiToDocument();
            this.UpdateDocumentMapperDoc();

            this.DialogResult = DialogResult.OK;
        }

        private bool ValidateContents()
        {
            if (string.IsNullOrEmpty(eFolderName_textBox.Text))
            {
                MessageBox.Show("Efolder Name is reqruired.");
                return false;
            }

            if (string.IsNullOrEmpty(externalDocId_textBox.Text))
            {
                MessageBox.Show("UW Condition Name is reqruired.");
                return false;
            }


            return true;
        }

        private void MapUiToDocument()
        {
            if (TheDocument == null)
            {
                TheDocument = new Document();
                TheDocument.ExternalDocumentSourceId = ExternalSourceId.Value;
            }

            TheDocument.Enable = enableChckBox.Checked;
            TheDocument.EncompassEfolderName = eFolderName_textBox.Text;
            TheDocument.ExternalSystemDocumentId = externalDocId_textBox.Text;

            TheDocument.FieldMappings = GetFieldMappingsFromUiDataGrid();

        }

        private IList<FieldMapping> GetFieldMappingsFromUiDataGrid()
        {
            List<FieldMapping> result = new List<FieldMapping>();

            for (int i = 0; i < fieldMappingsDataGridView.Rows.Count; i++)
            {
                var row = fieldMappingsDataGridView.Rows[i];
                if (row.IsNewRow)
                    continue;

                var field = MapDgRowToFieldDataObject(row);
                result.Add(field);
            }

            return result;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

        }


        private void UpdateDocumentMapperDoc()
        {
            string url = $"{Settings.UpdateDocumentMapperDocumentUrl}?userId={EncompassApplication.CurrentUser.ID}";
            string json = JsonConvert.SerializeObject(TheDocument);

            var httpResponse = WyndhamClientManager.GetAuthHttpClient().Post(url, TheDocument);
            if (httpResponse.IsSuccessStatusCode)
            {
                Task<string> responseString = httpResponse.Content.ReadAsStringAsync();
                TheDocument = JsonConvert.DeserializeObject<Document>(responseString.Result);
            }
            else
            {
                throw new Exception($"Error retrieving external source documents!'{httpResponse.StatusCode}'");
            }
        }

        private void pasteMenuItem_Click(object sender, EventArgs e)
        {
            List<Exception> errors = new List<Exception>();

            var newFieldDataMaps = ConvertClipboardToFieldDataMappings(out errors);
            if (errors.Any())
            {
                string errorMsg = UIHelper.FormatListOfExceptionsIntoErrorMessage(errors);
                throw new Exception(errorMsg);
            }

            // set enable proprety to True -- we wouldn't be adding them to not enable them..
            // adding and then disabling would be the exception
            newFieldDataMaps.ForEach(x => x.Enable = true);


            foreach (var field in newFieldDataMaps)
                TheDocument.FieldMappings.Add(field);

            GridViewHelper.LoadListObjectsToGridview(fieldMappingsDataGridView, TheDocument.FieldMappings);
        }

        private List<FieldMapping> ConvertClipboardToFieldDataMappings(out List<Exception> errors)
        {
            List<FieldMapping> result = new List<FieldMapping>();
            errors = new List<Exception>();
            string[] pastedRows = GridViewHelper.GetPastedRowsFromClipboard();
            if (pastedRows == null)
                return result;

            foreach (string pastedRow in pastedRows)
            {
                try
                {
                    var newFieldMapping = ConvertClipboardRowToFieldDataMapping(pastedRow);
                    ValidateFieldMapping(newFieldMapping);
                    result.Add(newFieldMapping);
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }


            return result;
        }

        private void ValidateFieldMapping(FieldMapping newFieldMapping)
        {
            //SP - if required property does not have value; will throw seralizer exception 
            JsonConvert.SerializeObject(newFieldMapping);

            // check for duplicates
            var alreadyExistingFieldMapping = TheDocument.FieldMappings
                .FirstOrDefault(x => x.EncompassFieldId.Equals(newFieldMapping.EncompassFieldId));

            if (alreadyExistingFieldMapping != null)
                throw new DuplicateException($"Field Mapping with Encompass Field Id '{newFieldMapping.EncompassFieldId}' already exists");

            alreadyExistingFieldMapping = TheDocument.FieldMappings
                .FirstOrDefault(x => x.ExternalFieldId.Equals(newFieldMapping.ExternalFieldId));

            if (alreadyExistingFieldMapping != null)
                throw new DuplicateException($"Field Mapping with External Field Id '{newFieldMapping.ExternalFieldId}' already exists");

        }

        private FieldMapping ConvertClipboardRowToFieldDataMapping(string pastedRow)
        {
            var result = new FieldMapping();
            var pastedRowValues = pastedRow.Split(new char[] { '\t' });

            for (int i = 0; i < pastedRowValues.Count(); i++)
            {
                var value = (object)pastedRowValues[i];

                // SP - the column names match the property names 
                // columns are loaded dynamically with the same object property names
                var column = fieldMappingsDataGridView.Columns[i];
                string propertyName = column.Name;

                PropertyInfo fieldDataMappingProperty = result.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                if (fieldDataMappingProperty != null && fieldDataMappingProperty.CanWrite && value != null)
                {

                    if (value.GetType().Equals(fieldDataMappingProperty.PropertyType) == false)
                    {
                        throw new InvalidValueException($"Unable to set property '{fieldDataMappingProperty.Name}' type '{fieldDataMappingProperty.GetType()}' the value provided '{value}' type '{value.GetType()}'");
                    }

                    fieldDataMappingProperty.SetValue(result, Convert.ChangeType(value, fieldDataMappingProperty.PropertyType), null);
                }
            }

            return result;
        }

        private void copyMenuItem_Click(object sender, EventArgs e)
        {
            DataObject d = fieldMappingsDataGridView.GetClipboardContent();
            Clipboard.SetDataObject(d);
        }
    }
}
