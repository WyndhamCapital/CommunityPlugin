
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
            this.DocumentsDropDownBox = new System.Windows.Forms.ComboBox();
            this.ConfidenceLabel = new System.Windows.Forms.Label();
            this.DocumentTypeLabel = new System.Windows.Forms.Label();
            this.ExtractedDocsDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.ExtractedDocsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // FieldsFlowLayoutPanel
            // 
            this.FieldsFlowLayoutPanel.Location = new System.Drawing.Point(380, 123);
            this.FieldsFlowLayoutPanel.Name = "FieldsFlowLayoutPanel";
            this.FieldsFlowLayoutPanel.Size = new System.Drawing.Size(429, 289);
            this.FieldsFlowLayoutPanel.TabIndex = 0;
            // 
            // DocumentsDropDownBox
            // 
            this.DocumentsDropDownBox.FormattingEnabled = true;
            this.DocumentsDropDownBox.Location = new System.Drawing.Point(13, 31);
            this.DocumentsDropDownBox.Name = "DocumentsDropDownBox";
            this.DocumentsDropDownBox.Size = new System.Drawing.Size(265, 21);
            this.DocumentsDropDownBox.TabIndex = 1;
            this.DocumentsDropDownBox.SelectedIndexChanged += new System.EventHandler(this.DocumentsDropDownBox_SelectedIndexChanged);
            // 
            // ConfidenceLabel
            // 
            this.ConfidenceLabel.AutoSize = true;
            this.ConfidenceLabel.Location = new System.Drawing.Point(12, 96);
            this.ConfidenceLabel.Name = "ConfidenceLabel";
            this.ConfidenceLabel.Size = new System.Drawing.Size(100, 15);
            this.ConfidenceLabel.TabIndex = 2;
            this.ConfidenceLabel.Text = "ConfidenceLabel";
            // 
            // DocumentTypeLabel
            // 
            this.DocumentTypeLabel.AutoSize = true;
            this.DocumentTypeLabel.Location = new System.Drawing.Point(12, 69);
            this.DocumentTypeLabel.Name = "DocumentTypeLabel";
            this.DocumentTypeLabel.Size = new System.Drawing.Size(121, 15);
            this.DocumentTypeLabel.TabIndex = 3;
            this.DocumentTypeLabel.Text = "DocumentTypeLabel";
            // 
            // ExtractedDocsDataGridView
            // 
            this.ExtractedDocsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ExtractedDocsDataGridView.Location = new System.Drawing.Point(15, 123);
            this.ExtractedDocsDataGridView.Name = "ExtractedDocsDataGridView";
            this.ExtractedDocsDataGridView.RowHeadersWidth = 51;
            this.ExtractedDocsDataGridView.Size = new System.Drawing.Size(341, 289);
            this.ExtractedDocsDataGridView.TabIndex = 4;
            this.ExtractedDocsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ExtractedDocsDataGridView_CellContentClick);
            // 
            // DataExtractionLoanAllDocsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 450);
            this.Controls.Add(this.ExtractedDocsDataGridView);
            this.Controls.Add(this.DocumentTypeLabel);
            this.Controls.Add(this.ConfidenceLabel);
            this.Controls.Add(this.DocumentsDropDownBox);
            this.Controls.Add(this.FieldsFlowLayoutPanel);
            this.Name = "DataExtractionLoanAllDocsForm";
            this.Text = "DataExtraction_AllDocs_Form";
            ((System.ComponentModel.ISupportInitialize)(this.ExtractedDocsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel FieldsFlowLayoutPanel;
        private System.Windows.Forms.ComboBox DocumentsDropDownBox;
        private System.Windows.Forms.Label ConfidenceLabel;
        private System.Windows.Forms.Label DocumentTypeLabel;
        private System.Windows.Forms.DataGridView ExtractedDocsDataGridView;
    }
}