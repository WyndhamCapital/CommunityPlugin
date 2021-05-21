
namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    partial class DocumentMapper_SingleDoc_Form
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
            this.eFolderName_textBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.externalDocId_textBox = new System.Windows.Forms.TextBox();
            this.enableChckBox = new System.Windows.Forms.CheckBox();
            this.button_ApplyChanges = new System.Windows.Forms.Button();
            this.fieldMappingsDataGridView = new System.Windows.Forms.DataGridView();
            this.button_Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fieldMappingsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // eFolderName_textBox
            // 
            this.eFolderName_textBox.Location = new System.Drawing.Point(144, 23);
            this.eFolderName_textBox.Margin = new System.Windows.Forms.Padding(4);
            this.eFolderName_textBox.Name = "eFolderName_textBox";
            this.eFolderName_textBox.Size = new System.Drawing.Size(210, 22);
            this.eFolderName_textBox.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 26);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 17);
            this.label4.TabIndex = 28;
            this.label4.Text = "Efolder Name*";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(17, 53);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 26);
            this.label7.TabIndex = 30;
            this.label7.Text = "External Doc Id";
            // 
            // externalDocId_textBox
            // 
            this.externalDocId_textBox.Location = new System.Drawing.Point(144, 50);
            this.externalDocId_textBox.Margin = new System.Windows.Forms.Padding(4);
            this.externalDocId_textBox.Name = "externalDocId_textBox";
            this.externalDocId_textBox.Size = new System.Drawing.Size(211, 22);
            this.externalDocId_textBox.TabIndex = 29;
            // 
            // enableChckBox
            // 
            this.enableChckBox.AutoSize = true;
            this.enableChckBox.Location = new System.Drawing.Point(144, 80);
            this.enableChckBox.Margin = new System.Windows.Forms.Padding(4);
            this.enableChckBox.Name = "enableChckBox";
            this.enableChckBox.Size = new System.Drawing.Size(74, 21);
            this.enableChckBox.TabIndex = 31;
            this.enableChckBox.Text = "Enable";
            this.enableChckBox.UseVisualStyleBackColor = true;
            // 
            // button_ApplyChanges
            // 
            this.button_ApplyChanges.Location = new System.Drawing.Point(144, 120);
            this.button_ApplyChanges.Margin = new System.Windows.Forms.Padding(4);
            this.button_ApplyChanges.Name = "button_ApplyChanges";
            this.button_ApplyChanges.Size = new System.Drawing.Size(100, 30);
            this.button_ApplyChanges.TabIndex = 32;
            this.button_ApplyChanges.Text = "Update";
            this.button_ApplyChanges.UseVisualStyleBackColor = true;
            this.button_ApplyChanges.Click += new System.EventHandler(this.button_ApplyChanges_Click);
            // 
            // fieldMappingsDataGridView
            // 
            this.fieldMappingsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fieldMappingsDataGridView.Location = new System.Drawing.Point(373, 23);
            this.fieldMappingsDataGridView.Name = "fieldMappingsDataGridView";
            this.fieldMappingsDataGridView.RowHeadersWidth = 51;
            this.fieldMappingsDataGridView.RowTemplate.Height = 24;
            this.fieldMappingsDataGridView.Size = new System.Drawing.Size(179, 256);
            this.fieldMappingsDataGridView.TabIndex = 33;
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(252, 120);
            this.button_Cancel.Margin = new System.Windows.Forms.Padding(4);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(103, 30);
            this.button_Cancel.TabIndex = 34;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // DocumentMapper_SingleDoc_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 291);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.fieldMappingsDataGridView);
            this.Controls.Add(this.button_ApplyChanges);
            this.Controls.Add(this.enableChckBox);
            this.Controls.Add(this.eFolderName_textBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.externalDocId_textBox);
            this.Name = "DocumentMapper_SingleDoc_Form";
            this.Text = "Edit Document Mapper Doc";
            ((System.ComponentModel.ISupportInitialize)(this.fieldMappingsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox eFolderName_textBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox externalDocId_textBox;
        private System.Windows.Forms.CheckBox enableChckBox;
        private System.Windows.Forms.Button button_ApplyChanges;
        private System.Windows.Forms.DataGridView fieldMappingsDataGridView;
        private System.Windows.Forms.Button button_Cancel;
    }
}