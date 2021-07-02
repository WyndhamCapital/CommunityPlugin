using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunityPlugin.Objects.Models.WCM.FieldExtraction.UI;

namespace CommunityPlugin.Non_Native_Modifications.Dialog
{
    public partial class DataExtractionField_Control : UserControl
    {
        public DataExtractionField_Control(UIExtractedField field)
        {
            InitializeComponent();

            LoadDataExtractionField(field);
        }

        private void LoadDataExtractionField(UIExtractedField field)
        {
            encompassFieldDescrLabel.Text = string.IsNullOrEmpty(field.EncompassCompareFieldDescription) ? 
                field.EncompassCompareFieldId : $"{field.EncompassCompareFieldId}: {field.EncompassCompareFieldDescription}";

            if (field.EncompassCompareFieldValue != null)
            {
                loanValueTextBox.Text = field.EncompassCompareFieldValue.ToString();
            }

            insertFieldDescrLabel.Text = string.IsNullOrEmpty(field.EncompassInsertValueFieldDescription) ?
            field.EncompassInsertValueFieldId : $"{field.EncompassInsertValueFieldId}: {field.EncompassInsertValueFieldDescription}";

            if (field.EncompassInsertValueFieldValue != null)
            {
                insertFieldValueTextbox.Text = field.EncompassInsertValueFieldValue.ToString();
            }

            // data extraction properties
            dataExtractionFieldDescrLabel.Text = string.IsNullOrEmpty(field.ExternalSourceFieldDescription) ? 
                field.ExternalSourceFieldId : $"{field.ExternalSourceFieldId}: {field.ExternalSourceFieldDescription}";


            dataExtractionValueTextBox.Text = field.DataExtractionFieldValue;

        }
    }
}
