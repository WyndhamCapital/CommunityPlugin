using CommunityPlugin.Objects.Models.WCM.DocumentMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    public partial class DocumentMapper_SingleDoc_Form : Form
    {
        public Document TheDocument;
        private readonly int? ExternalSourceId;
        public DocumentMapper_SingleDoc_Form(Document document)
        {
            InitializeComponent();

            TheDocument = document;
            MapDocumentToUi(document);
        }

        public DocumentMapper_SingleDoc_Form(int externalSourceId)
        {
            InitializeComponent();
            ExternalSourceId = externalSourceId;
        }


        private void MapDocumentToUi(Document document)
        {
            eFolderName_textBox.Text = TheDocument.EncompassEfolderName;
            externalDocId_textBox.Text = TheDocument.ExternalSystemDocumentId;
            enableChckBox.Checked = TheDocument.Enable;
        }

        private void button_ApplyChanges_Click(object sender, EventArgs e)
        {
            if (!this.ValidateContents())
                return;

            this.MapUiToDocument();

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
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

        }
    }
}
