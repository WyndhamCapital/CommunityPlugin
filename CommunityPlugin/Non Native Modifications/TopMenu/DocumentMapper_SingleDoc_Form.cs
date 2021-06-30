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
using EllieMae.EMLite.DataEngine;
using WyndhamLib.Authentication;
using System.IO;

namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    public partial class DocumentMapperSingleDocForm : Form
    {
        public Document TheDocument;
        private readonly int? _externalSourceId;
        private readonly WcmSettings _settings;
        private CustomFieldsInfo _encompassCustomFields;
        private List<FieldDefinition> _encompassStandardCommonFields;

        public DocumentMapperSingleDocForm(Document document,
            WcmSettings wcmSettings,
            CustomFieldsInfo encompassCustomFields,
            List<FieldDefinition> encompassStandardFields)
        {
            InitializeComponent();

            _settings = wcmSettings;
            TheDocument = document;
            _encompassStandardCommonFields = encompassStandardFields;
            _encompassCustomFields = encompassCustomFields;

            GridViewHelper.LoadFieldMappingColumns<FieldMapping>(fieldMappingsDataGridView);
            MapDocumentToUi();
        }


        public DocumentMapperSingleDocForm(int externalSourceId,
            WcmSettings wcmSettings,
            CustomFieldsInfo encompassCustomFields,
            List<FieldDefinition> encompassStandardFields)
        {
            InitializeComponent();
            GridViewHelper.LoadFieldMappingColumns<FieldMapping>(fieldMappingsDataGridView);

            _settings = wcmSettings;
            _encompassStandardCommonFields = encompassStandardFields;
            _encompassCustomFields = encompassCustomFields;
            _externalSourceId = externalSourceId;
        }

        private void MapDocumentToUi()
        {
            eFolderName_textBox.Text = TheDocument.EncompassEfolderName;
            externalDocId_textBox.Text = TheDocument.ExternalSystemDocumentId;
            enableChckBox.Checked = TheDocument.Enable;

            GridViewHelper.LoadListObjectsToGridview(fieldMappingsDataGridView, TheDocument.FieldMappings);
        }


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

            result = GridViewHelper.SetRowObjectPropertiesFromGridViewColumns(result, row, fieldMappingsDataGridView);

            return result;
        }



        private void button_ApplyChanges_Click(object sender, EventArgs e)
        {
            if (!this.ValidateContents())
                return;

            this.MapUiToDocument();
            this.UpdateDocumentMapperDoc();

            if (!ValidateDocumentMapper())
                return;

            this.DialogResult = DialogResult.OK;
        }

        private bool ValidateDocumentMapper()
        {

            if (TheDocument.FieldMappings.Any())
            {

                var currentValueFieldIds = TheDocument
                    .FieldMappings
                    .Select(x => x.EncompassFieldIdCurrentValue).ToList();

                if (ValidateEncompassFields(currentValueFieldIds, false) == false)
                    return false;


                var insertValueFieldIds = TheDocument
                    .FieldMappings
                    .Select(x => x.EncompassFieldIdInsertValue).ToList();

                // SP - for insert value Fields, we are only allowing custom fields right now
                // the fields will be set in the API, where standard fields are set by object properties, not field ID's
                // it's possible to add some standard fields (use contract/schema generator), and pass that to update loan
                // but for V1, only custom fields are allowed
                if (ValidateEncompassFields(insertValueFieldIds, true) == false)
                    return false;

            }

            return true;
        }

        private bool ValidateEncompassFields(List<string> fieldIds, bool allowCustomFieldsOnly)
        {
            List<Exception> errors = new List<Exception>();
            fieldIds.RemoveAll(string.IsNullOrEmpty);

            // if there are no fields to validate, return true, there are no errors
            if (fieldIds.Any() == false)
                return true;


            var result = true;

            var customFields = fieldIds.
                Where(x => x.StartsWith("cx.", StringComparison.OrdinalIgnoreCase)).ToList();

            List<string> customFieldErrors = ValidateEncompassCustomFields(customFields);
            if (customFieldErrors.Any())
            {
                errors.Add(new Exception($"The custom fields below do NOT exist. " +
                                         $"{Environment.NewLine + string.Join(Environment.NewLine, customFieldErrors)}"));
            }

            var standardFields = fieldIds.Except(customFields).ToList();
            if (allowCustomFieldsOnly && standardFields.Any())
            {
                errors.Add(new Exception($"Only custom field are allowed; the standard fields below are present. " +
                                         $"{Environment.NewLine + string.Join(Environment.NewLine, standardFields)}"));

            }
            else
            {
                // if standard fields are allowed, check if they exist
                List<string> standardFieldErrors = ValidateEncompassStandardFields(standardFields);
                if (standardFieldErrors.Any())
                {
                    errors.Add(new Exception($"The standard fields below do NOT exist. " +
                                             $"{Environment.NewLine + string.Join(Environment.NewLine, standardFieldErrors)}"));
                }
            }


            if (errors.Any())
            {
                MessageBox.Show(UIHelper.FormatListOfExceptionsIntoErrorMessage(errors));
                return false;
            }
            else
            {
                return true;
            }
        }

        private List<string> ValidateEncompassStandardFields(List<string> fieldIds)
        {
            List<string> errorFields = new List<string>();
            foreach (var fieldId in fieldIds)
            {
                var standardField = _encompassStandardCommonFields.FirstOrDefault(x =>
                    x.FieldID.Equals(fieldId,
                        StringComparison.OrdinalIgnoreCase));

                // if field doesn't exist in encompass; this is a problem
                if (standardField == null)
                {
                    errorFields.Add(fieldId);
                }

            }

            return errorFields;
        }

        private List<string> ValidateEncompassCustomFields(List<string> encompassCustomFieldIds)
        {
            List<string> errorFields = new List<string>();
            foreach (var customFieldId in encompassCustomFieldIds)
            {
                var encompassCustomField = _encompassCustomFields.Cast<CustomFieldInfo>().FirstOrDefault(x =>
                    x.FieldID.Equals(customFieldId,
                        StringComparison.OrdinalIgnoreCase));

                // if field doesn't exist in encompass; this is a problem
                if (encompassCustomField == null)
                {
                    errorFields.Add(customFieldId);
                }

            }

            return errorFields;
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
                MessageBox.Show("External Doc Id is reqruired.");
                return false;
            }


            return true;
        }

        private void MapUiToDocument()
        {
            if (TheDocument == null)
            {
                TheDocument = new Document();
                TheDocument.ExternalDocumentSourceId = _externalSourceId.Value;
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
            string url = $"{_settings.UpdateDocumentMapperDocumentUrl}?userId={EncompassApplication.CurrentUser.ID}";
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
                .FirstOrDefault(x => x.EncompassFieldIdCurrentValue.Equals(newFieldMapping.EncompassFieldIdCurrentValue));

            if (alreadyExistingFieldMapping != null)
                throw new DuplicateException($"Field Mapping with Encompass Field Id '{newFieldMapping.EncompassFieldIdCurrentValue}' already exists");

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
