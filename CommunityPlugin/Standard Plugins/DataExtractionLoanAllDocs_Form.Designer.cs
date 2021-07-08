
namespace CommunityPlugin.Standard_Plugins
{
    partial class DataExtractionLoanAllDocsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FieldsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ConfidenceLabel = new System.Windows.Forms.Label();
            this.DocumentTypeLabel = new System.Windows.Forms.Label();
            this.ExtractedDocsDataGridView = new System.Windows.Forms.DataGridView();
            this.insertFieldDescrLabel = new System.Windows.Forms.Label();
            this.dataExtraction = new System.Windows.Forms.Label();
            this.standardField = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ExtractedDocsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // FieldsFlowLayoutPanel
            // 
            this.FieldsFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FieldsFlowLayoutPanel.AutoScroll = true;
            this.FieldsFlowLayoutPanel.Location = new System.Drawing.Point(430, 76);
            this.FieldsFlowLayoutPanel.Name = "FieldsFlowLayoutPanel";
            this.FieldsFlowLayoutPanel.Size = new System.Drawing.Size(940, 438);
            this.FieldsFlowLayoutPanel.TabIndex = 0;
            // 
            // ConfidenceLabel
            // 
            this.ConfidenceLabel.AutoSize = true;
            this.ConfidenceLabel.Location = new System.Drawing.Point(433, 36);
            this.ConfidenceLabel.Name = "ConfidenceLabel";
            this.ConfidenceLabel.Size = new System.Drawing.Size(87, 13);
            this.ConfidenceLabel.TabIndex = 2;
            this.ConfidenceLabel.Text = "ConfidenceLabel";
            // 
            // DocumentTypeLabel
            // 
            this.DocumentTypeLabel.AutoSize = true;
            this.DocumentTypeLabel.Location = new System.Drawing.Point(433, 9);
            this.DocumentTypeLabel.Name = "DocumentTypeLabel";
            this.DocumentTypeLabel.Size = new System.Drawing.Size(106, 13);
            this.DocumentTypeLabel.TabIndex = 3;
            this.DocumentTypeLabel.Text = "DocumentTypeLabel";
            // 
            // ExtractedDocsDataGridView
            // 
            this.ExtractedDocsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ExtractedDocsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ExtractedDocsDataGridView.Location = new System.Drawing.Point(10, 9);
            this.ExtractedDocsDataGridView.Name = "ExtractedDocsDataGridView";
            this.ExtractedDocsDataGridView.ReadOnly = true;
            this.ExtractedDocsDataGridView.RowHeadersVisible = false;
            this.ExtractedDocsDataGridView.RowHeadersWidth = 51;
            this.ExtractedDocsDataGridView.Size = new System.Drawing.Size(414, 505);
            this.ExtractedDocsDataGridView.TabIndex = 4;
            this.ExtractedDocsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ExtractedDocsDataGridView_CellContentClick);
            // 
            // insertFieldDescrLabel
            // 
            this.insertFieldDescrLabel.Location = new System.Drawing.Point(1054, 55);
            this.insertFieldDescrLabel.Name = "insertFieldDescrLabel";
            this.insertFieldDescrLabel.Size = new System.Drawing.Size(147, 18);
            this.insertFieldDescrLabel.TabIndex = 11;
            this.insertFieldDescrLabel.Text = "Custom Field Value";
            // 
            // dataExtraction
            // 
            this.dataExtraction.Location = new System.Drawing.Point(739, 55);
            this.dataExtraction.Name = "dataExtraction";
            this.dataExtraction.Size = new System.Drawing.Size(141, 18);
            this.dataExtraction.TabIndex = 9;
            this.dataExtraction.Text = "Data Extraction";
            // 
            // standardField
            // 
            this.standardField.Location = new System.Drawing.Point(595, 55);
            this.standardField.Name = "standardField";
            this.standardField.Size = new System.Drawing.Size(147, 18);
            this.standardField.TabIndex = 8;
            this.standardField.Text = "Encompass Standard Field";
            // 
            // DataExtractionLoanAllDocsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1375, 526);
            this.Controls.Add(this.insertFieldDescrLabel);
            this.Controls.Add(this.dataExtraction);
            this.Controls.Add(this.standardField);
            this.Controls.Add(this.ExtractedDocsDataGridView);
            this.Controls.Add(this.DocumentTypeLabel);
            this.Controls.Add(this.ConfidenceLabel);
            this.Controls.Add(this.FieldsFlowLayoutPanel);
            this.Name = "DataExtractionLoanAllDocsForm";
            this.Text = "DataExtraction_AllDocs_Form";
            ((System.ComponentModel.ISupportInitialize)(this.ExtractedDocsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel FieldsFlowLayoutPanel;
        private System.Windows.Forms.Label ConfidenceLabel;
        private System.Windows.Forms.Label DocumentTypeLabel;
        private System.Windows.Forms.DataGridView ExtractedDocsDataGridView;
        private System.Windows.Forms.Label insertFieldDescrLabel;
        private System.Windows.Forms.Label dataExtraction;
        private System.Windows.Forms.Label standardField;
    }
}