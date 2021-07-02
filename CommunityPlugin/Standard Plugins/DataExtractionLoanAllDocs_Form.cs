using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CommunityPlugin.Non_Native_Modifications.Dialog;
using CommunityPlugin.Objects.Helpers;
using CommunityPlugin.Objects.Models.WCM.DocumentMapper;
using CommunityPlugin.Objects.Models.WCM.FieldExtraction;
using CommunityPlugin.Objects.Models.WCM.FieldExtraction.UI;
using EllieMae.Encompass.Automation;
using FieldMapping = CommunityPlugin.Objects.Models.WCM.DocumentMapper.FieldMapping;

namespace CommunityPlugin.Standard_Plugins
{
    public partial class DataExtractionLoanAllDocsForm : Form
    {
        public List<IClassifiedDocument> FieldExtractedDocs;
        private readonly List<Document> _dataExtractionDocumentMaps;
        public DataExtractionLoanAllDocsForm(List<IClassifiedDocument> classifiedDocuments, List<Document> dataExtractionDocumentMaps)
        {
            FieldExtractedDocs = classifiedDocuments;
            _dataExtractionDocumentMaps = dataExtractionDocumentMaps;
            InitializeComponent();

            LoadDocuments();
        }

        private void LoadDocuments()
        {

            GridViewHelper.LoadFieldMappingColumns<IClassifiedDocument>(ExtractedDocsDataGridView);
            SetCountOfFieldsExtracted();
            GridViewHelper.LoadListObjectsToGridview(ExtractedDocsDataGridView, FieldExtractedDocs);

        }

        private void SetCountOfFieldsExtracted()
        {
            foreach (var classifiedDocument in FieldExtractedDocs)
            {
                if (classifiedDocument.FieldData != null && classifiedDocument.FieldData.Any())
                {
                    classifiedDocument.FieldDataCount = classifiedDocument.FieldData.Count();
                }
            }
        }



        private void LoadDocument(IClassifiedDocument documentToLoad)
        {
            DocumentTypeLabel.Text = documentToLoad.DocType;
            ConfidenceLabel.Text = $@"Confidence: {documentToLoad.Confidence}. ID: {documentToLoad.Id.ToString()}";

            FieldsFlowLayoutPanel.Controls.Clear();

            foreach (var extractedfield in documentToLoad.FieldData)
            {

                var fieldMap = GetFieldMap(extractedfield, documentToLoad, _dataExtractionDocumentMaps);
                UIExtractedField fieldToLoad = MapDataExtractionFieldForUi(extractedfield, fieldMap);

                var fieldControl = new DataExtractionField_Control(fieldToLoad);
                FieldsFlowLayoutPanel.Controls.Add(fieldControl);
            }


        }

        private UIExtractedField MapDataExtractionFieldForUi(IFieldData extractedfield, FieldMapping fieldMap)
        {
            UIExtractedField result = new UIExtractedField();

            result.ExternalSourceFieldId = extractedfield.Key;
            result.DataExtractionFieldValue = extractedfield.Value;

            if (fieldMap != null)
            {
                result.FieldMap = fieldMap;

                if (string.IsNullOrEmpty(fieldMap.EncompassFieldIdCurrentValue) == false)
                {
                    result.EncompassCompareFieldId = fieldMap.EncompassFieldIdCurrentValue;

                    var encompassField = EncompassApplication.CurrentLoan.Fields[fieldMap.EncompassFieldIdCurrentValue];
                    if (encompassField != null)
                    {
                        result.EncompassCompareFieldDescription = encompassField.Descriptor.Description;
                        result.EncompassCompareFieldValue = encompassField.Value;
                    }
                }


                if (string.IsNullOrEmpty(fieldMap.EncompassFieldIdInsertValue) == false)
                {
                    result.EncompassInsertValueFieldId = fieldMap.EncompassFieldIdInsertValue;

                    var encompassField = EncompassApplication.CurrentLoan.Fields[fieldMap.EncompassFieldIdInsertValue];
                    if (encompassField != null)
                    {
                        result.EncompassInsertValueFieldDescription = encompassField.Descriptor.Description;
                        result.EncompassInsertValueFieldValue = encompassField.Value;
                    }
                }

            }

            return result;
        }

        private FieldMapping GetFieldMap(IFieldData extractedfield, IClassifiedDocument documentToLoad, List<Document> dataExtractionDocumentMaps)
        {
            var documentMap =
                dataExtractionDocumentMaps.FirstOrDefault(
                    x => x.ExternalSystemDocumentId.Equals(documentToLoad.DocType));

            if (documentMap == null)
                return null;


            if (documentMap.FieldMappings == null)
                return null;
            
            var fieldMap = 
                documentMap.FieldMappings.FirstOrDefault(
                    x => x.ExternalFieldId.Equals(extractedfield.Key));

            return fieldMap;
        }

        private void ExtractedDocsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string errorMsg;
            var selectedDocument = GridViewHelper.GetCurrentGridRowTag<IClassifiedDocument>(ExtractedDocsDataGridView, out errorMsg);

            if (selectedDocument == null)
            {
                MessageBox.Show(errorMsg);
            }
            else
            {
                LoadDocument(selectedDocument);
            }

        }
    }
}
