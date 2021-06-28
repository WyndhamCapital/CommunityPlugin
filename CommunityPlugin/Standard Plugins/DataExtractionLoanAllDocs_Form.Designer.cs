
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
            ((System.ComponentModel.ISupportInitialize)(this.ExtractedDocsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // FieldsFlowLayoutPanel
            // 
            this.FieldsFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldsFlowLayoutPanel.Location = new System.Drawing.Point(410, 52);
            this.FieldsFlowLayoutPanel.Name = "FieldsFlowLayoutPanel";
            this.FieldsFlowLayoutPanel.Size = new System.Drawing.Size(644, 332);
            this.FieldsFlowLayoutPanel.TabIndex = 0;
            // 
            // ConfidenceLabel
            // 
            this.ConfidenceLabel.AutoSize = true;
            this.ConfidenceLabel.Location = new System.Drawing.Point(407, 36);
            this.ConfidenceLabel.Name = "ConfidenceLabel";
            this.ConfidenceLabel.Size = new System.Drawing.Size(87, 13);
            this.ConfidenceLabel.TabIndex = 2;
            this.ConfidenceLabel.Text = "ConfidenceLabel";
            // 
            // DocumentTypeLabel
            // 
            this.DocumentTypeLabel.AutoSize = true;
            this.DocumentTypeLabel.Location = new System.Drawing.Point(407, 9);
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
            this.ExtractedDocsDataGridView.Location = new System.Drawing.Point(15, 9);
            this.ExtractedDocsDataGridView.Name = "ExtractedDocsDataGridView";
            this.ExtractedDocsDataGridView.ReadOnly = true;
            this.ExtractedDocsDataGridView.RowHeadersVisible = false;
            this.ExtractedDocsDataGridView.RowHeadersWidth = 51;
            this.ExtractedDocsDataGridView.Size = new System.Drawing.Size(387, 375);
            this.ExtractedDocsDataGridView.TabIndex = 4;
            this.ExtractedDocsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ExtractedDocsDataGridView_CellContentClick);
            // 
            // DataExtractionLoanAllDocsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 396);
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
    }
}