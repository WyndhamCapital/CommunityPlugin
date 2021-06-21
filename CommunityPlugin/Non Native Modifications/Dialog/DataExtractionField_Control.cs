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
            if (string.IsNullOrEmpty(field.EncompassFieldDescription))
            {
                encompassFieldDescrLabel.Text = field.EncompassFieldId;
            }
            else
            {
                encompassFieldDescrLabel.Text = $"{field.EncompassFieldId}: {field.EncompassFieldDescription}";
            }

            if (string.IsNullOrEmpty(field.ExternalSourceFieldDescription))
            {
                dataExtractionFieldDescrLabel.Text = field.ExternalSourceFieldId;
            }
            else
            {
                dataExtractionFieldDescrLabel.Text = $"{field.ExternalSourceFieldId}: {field.ExternalSourceFieldDescription}";
            }

            if (field.EncompassFieldValue != null)
            {
                loanValueTextBox.Text = field.EncompassFieldValue.ToString();
            }

            dataExtractionValueTextBox.Text = field.DataExtractionFieldValue;

        }
    }
}
