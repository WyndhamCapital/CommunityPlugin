
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
            this.components = new System.ComponentModel.Container();
            this.eFolderName_textBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.externalDocId_textBox = new System.Windows.Forms.TextBox();
            this.enableChckBox = new System.Windows.Forms.CheckBox();
            this.button_ApplyChanges = new System.Windows.Forms.Button();
            this.fieldMappingsDataGridView = new System.Windows.Forms.DataGridView();
            this.fieldMappingContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fieldMappingsDataGridView)).BeginInit();
            this.fieldMappingContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // eFolderName_textBox
            // 
            this.eFolderName_textBox.Location = new System.Drawing.Point(108, 19);
            this.eFolderName_textBox.Name = "eFolderName_textBox";
            this.eFolderName_textBox.Size = new System.Drawing.Size(158, 20);
            this.eFolderName_textBox.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Efolder Name*";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(13, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 21);
            this.label7.TabIndex = 30;
            this.label7.Text = "External Doc Id*";
            // 
            // externalDocId_textBox
            // 
            this.externalDocId_textBox.Location = new System.Drawing.Point(108, 41);
            this.externalDocId_textBox.Name = "externalDocId_textBox";
            this.externalDocId_textBox.Size = new System.Drawing.Size(159, 20);
            this.externalDocId_textBox.TabIndex = 29;
            // 
            // enableChckBox
            // 
            this.enableChckBox.AutoSize = true;
            this.enableChckBox.Location = new System.Drawing.Point(108, 65);
            this.enableChckBox.Name = "enableChckBox";
            this.enableChckBox.Size = new System.Drawing.Size(59, 17);
            this.enableChckBox.TabIndex = 31;
            this.enableChckBox.Text = "Enable";
            this.enableChckBox.UseVisualStyleBackColor = true;
            // 
            // button_ApplyChanges
            // 
            this.button_ApplyChanges.Location = new System.Drawing.Point(108, 150);
            this.button_ApplyChanges.Name = "button_ApplyChanges";
            this.button_ApplyChanges.Size = new System.Drawing.Size(75, 24);
            this.button_ApplyChanges.TabIndex = 32;
            this.button_ApplyChanges.Text = "Update";
            this.button_ApplyChanges.UseVisualStyleBackColor = true;
            this.button_ApplyChanges.Click += new System.EventHandler(this.button_ApplyChanges_Click);
            // 
            // fieldMappingsDataGridView
            // 
            this.fieldMappingsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldMappingsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fieldMappingsDataGridView.ContextMenuStrip = this.fieldMappingContextMenuStrip;
            this.fieldMappingsDataGridView.Location = new System.Drawing.Point(280, 19);
            this.fieldMappingsDataGridView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fieldMappingsDataGridView.Name = "fieldMappingsDataGridView";
            this.fieldMappingsDataGridView.RowHeadersVisible = false;
            this.fieldMappingsDataGridView.RowHeadersWidth = 51;
            this.fieldMappingsDataGridView.RowTemplate.Height = 24;
            this.fieldMappingsDataGridView.Size = new System.Drawing.Size(289, 155);
            this.fieldMappingsDataGridView.TabIndex = 33;
            // 
            // fieldMappingContextMenuStrip
            // 
            this.fieldMappingContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyMenuItem,
            this.pasteMenuItem});
            this.fieldMappingContextMenuStrip.Name = "fieldMappingContextMenuStrip";
            this.fieldMappingContextMenuStrip.Size = new System.Drawing.Size(103, 48);
            // 
            // copyMenuItem
            // 
            this.copyMenuItem.Name = "copyMenuItem";
            this.copyMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyMenuItem.Text = "Copy";
            this.copyMenuItem.Click += new System.EventHandler(this.copyMenuItem_Click);
            // 
            // pasteMenuItem
            // 
            this.pasteMenuItem.Name = "pasteMenuItem";
            this.pasteMenuItem.Size = new System.Drawing.Size(102, 22);
            this.pasteMenuItem.Text = "Paste";
            this.pasteMenuItem.Click += new System.EventHandler(this.pasteMenuItem_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(189, 150);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(77, 24);
            this.button_Cancel.TabIndex = 34;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // DocumentMapper_SingleDoc_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 183);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.fieldMappingsDataGridView);
            this.Controls.Add(this.button_ApplyChanges);
            this.Controls.Add(this.enableChckBox);
            this.Controls.Add(this.eFolderName_textBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.externalDocId_textBox);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "DocumentMapper_SingleDoc_Form";
            this.Text = "Edit Document Mapper Doc";
            ((System.ComponentModel.ISupportInitialize)(this.fieldMappingsDataGridView)).EndInit();
            this.fieldMappingContextMenuStrip.ResumeLayout(false);
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
        private System.Windows.Forms.ContextMenuStrip fieldMappingContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteMenuItem;
    }
}