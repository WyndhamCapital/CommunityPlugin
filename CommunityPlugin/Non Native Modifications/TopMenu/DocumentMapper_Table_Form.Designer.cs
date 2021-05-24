
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    partial class DocumentMapper_Table_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentMapper_Table_Form));
            this.externalSourcesComboBox = new System.Windows.Forms.ComboBox();
            this.documentsDataGridView = new System.Windows.Forms.DataGridView();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.addNewDocument = new System.Windows.Forms.PictureBox();
            this.enabledOnlyDocsCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.documentsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addNewDocument)).BeginInit();
            this.SuspendLayout();
            // 
            // externalSourcesComboBox
            // 
            this.externalSourcesComboBox.FormattingEnabled = true;
            this.externalSourcesComboBox.Location = new System.Drawing.Point(12, 23);
            this.externalSourcesComboBox.Name = "externalSourcesComboBox";
            this.externalSourcesComboBox.Size = new System.Drawing.Size(322, 24);
            this.externalSourcesComboBox.TabIndex = 0;
            this.externalSourcesComboBox.SelectedIndexChanged += new System.EventHandler(this.ExternalSourcesComboBox_SelectedIndexChanged);
            // 
            // documentsDataGridView
            // 
            this.documentsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.documentsDataGridView.Location = new System.Drawing.Point(12, 84);
            this.documentsDataGridView.Name = "documentsDataGridView";
            this.documentsDataGridView.ReadOnly = true;
            this.documentsDataGridView.RowHeadersVisible = false;
            this.documentsDataGridView.RowHeadersWidth = 51;
            this.documentsDataGridView.RowTemplate.Height = 24;
            this.documentsDataGridView.Size = new System.Drawing.Size(888, 345);
            this.documentsDataGridView.TabIndex = 1;
            this.documentsDataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.documentsDataGridView_CellContentDoubleClick);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Enabled = false;
            this.searchTextBox.Location = new System.Drawing.Point(13, 53);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(321, 22);
            this.searchTextBox.TabIndex = 2;
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            // 
            // addNewDocument
            // 
            this.addNewDocument.Image = ((System.Drawing.Image)(resources.GetObject("addNewDocument.Image")));
            this.addNewDocument.Location = new System.Drawing.Point(868, 43);
            this.addNewDocument.Margin = new System.Windows.Forms.Padding(4);
            this.addNewDocument.Name = "addNewDocument";
            this.addNewDocument.Size = new System.Drawing.Size(32, 34);
            this.addNewDocument.TabIndex = 3;
            this.addNewDocument.TabStop = false;
            this.addNewDocument.Click += new System.EventHandler(this.addNewDocument_Click);
            // 
            // enabledOnlyDocsCheckBox
            // 
            this.enabledOnlyDocsCheckBox.AutoSize = true;
            this.enabledOnlyDocsCheckBox.Enabled = false;
            this.enabledOnlyDocsCheckBox.Location = new System.Drawing.Point(341, 53);
            this.enabledOnlyDocsCheckBox.Name = "enabledOnlyDocsCheckBox";
            this.enabledOnlyDocsCheckBox.Size = new System.Drawing.Size(143, 20);
            this.enabledOnlyDocsCheckBox.TabIndex = 4;
            this.enabledOnlyDocsCheckBox.Text = "Enabled Only Docs";
            this.enabledOnlyDocsCheckBox.UseVisualStyleBackColor = true;
            this.enabledOnlyDocsCheckBox.CheckedChanged += new System.EventHandler(this.enabledOnlyDocsCheckBox_CheckedChanged);
            // 
            // DocumentMapper_Table_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 438);
            this.Controls.Add(this.enabledOnlyDocsCheckBox);
            this.Controls.Add(this.addNewDocument);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.documentsDataGridView);
            this.Controls.Add(this.externalSourcesComboBox);
            this.Name = "DocumentMapper_Table_Form";
            this.Text = "Doument Mapper Docs";
            this.Load += new System.EventHandler(this.DocumentMapper_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.documentsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addNewDocument)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion



        private System.Windows.Forms.ComboBox externalSourcesComboBox;
        private System.Windows.Forms.DataGridView documentsDataGridView;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.PictureBox addNewDocument;
        private System.Windows.Forms.CheckBox enabledOnlyDocsCheckBox;
    }
}