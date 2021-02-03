namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    partial class ConditionsMapperTable_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConditionsMapperTable_Form));
            this.panel1 = new System.Windows.Forms.Panel();
            this.deleteCondition = new System.Windows.Forms.PictureBox();
            this.editCondition = new System.Windows.Forms.PictureBox();
            this.ViewJson_button = new System.Windows.Forms.Button();
            this.addCondition = new System.Windows.Forms.PictureBox();
            this.ConditionsDataGridView = new System.Windows.Forms.DataGridView();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_ApplyChanges = new System.Windows.Forms.Button();
            this.EncCondition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BlendFollowUp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EfolderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deleteCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConditionsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.deleteCondition);
            this.panel1.Controls.Add(this.editCondition);
            this.panel1.Controls.Add(this.ViewJson_button);
            this.panel1.Controls.Add(this.addCondition);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(752, 33);
            this.panel1.TabIndex = 5;
            // 
            // deleteCondition
            // 
            this.deleteCondition.Image = ((System.Drawing.Image)(resources.GetObject("deleteCondition.Image")));
            this.deleteCondition.Location = new System.Drawing.Point(641, 2);
            this.deleteCondition.Name = "deleteCondition";
            this.deleteCondition.Size = new System.Drawing.Size(28, 28);
            this.deleteCondition.TabIndex = 4;
            this.deleteCondition.TabStop = false;
            this.deleteCondition.Click += new System.EventHandler(this.deleteCondition_Click);
            // 
            // editCondition
            // 
            this.editCondition.Image = ((System.Drawing.Image)(resources.GetObject("editCondition.Image")));
            this.editCondition.Location = new System.Drawing.Point(606, 2);
            this.editCondition.Name = "editCondition";
            this.editCondition.Size = new System.Drawing.Size(28, 28);
            this.editCondition.TabIndex = 3;
            this.editCondition.TabStop = false;
            this.editCondition.Click += new System.EventHandler(this.editCondition_Click);
            // 
            // ViewJson_button
            // 
            this.ViewJson_button.Location = new System.Drawing.Point(675, 3);
            this.ViewJson_button.Name = "ViewJson_button";
            this.ViewJson_button.Size = new System.Drawing.Size(75, 27);
            this.ViewJson_button.TabIndex = 2;
            this.ViewJson_button.Text = "View Json";
            this.ViewJson_button.UseVisualStyleBackColor = true;
            // 
            // addCondition
            // 
            this.addCondition.Image = ((System.Drawing.Image)(resources.GetObject("addCondition.Image")));
            this.addCondition.Location = new System.Drawing.Point(572, 3);
            this.addCondition.Name = "addCondition";
            this.addCondition.Size = new System.Drawing.Size(28, 28);
            this.addCondition.TabIndex = 0;
            this.addCondition.TabStop = false;
            this.addCondition.Click += new System.EventHandler(this.addCondition_Click);
            // 
            // ConditionsDataGridView
            // 
            this.ConditionsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConditionsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConditionsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EncCondition,
            this.BlendFollowUp,
            this.EfolderName});
            this.ConditionsDataGridView.Location = new System.Drawing.Point(3, 43);
            this.ConditionsDataGridView.MultiSelect = false;
            this.ConditionsDataGridView.Name = "ConditionsDataGridView";
            this.ConditionsDataGridView.RowHeadersVisible = false;
            this.ConditionsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ConditionsDataGridView.Size = new System.Drawing.Size(750, 273);
            this.ConditionsDataGridView.TabIndex = 6;
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(665, 322);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(90, 33);
            this.button_Cancel.TabIndex = 22;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_ApplyChanges
            // 
            this.button_ApplyChanges.Location = new System.Drawing.Point(569, 322);
            this.button_ApplyChanges.Name = "button_ApplyChanges";
            this.button_ApplyChanges.Size = new System.Drawing.Size(90, 33);
            this.button_ApplyChanges.TabIndex = 21;
            this.button_ApplyChanges.Text = "Save Changes";
            this.button_ApplyChanges.UseVisualStyleBackColor = true;
            this.button_ApplyChanges.Click += new System.EventHandler(this.button_ApplyChanges_Click);
            // 
            // EncCondition
            // 
            this.EncCondition.HeaderText = "Encompass Condition";
            this.EncCondition.Name = "EncCondition";
            this.EncCondition.Width = 225;
            // 
            // BlendFollowUp
            // 
            this.BlendFollowUp.HeaderText = "Blend Follow-Up";
            this.BlendFollowUp.Name = "BlendFollowUp";
            this.BlendFollowUp.Width = 200;
            // 
            // EfolderName
            // 
            this.EfolderName.HeaderText = "Efolder Name";
            this.EfolderName.Name = "EfolderName";
            this.EfolderName.Width = 200;
            // 
            // ConditionsMapperTable_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 361);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_ApplyChanges);
            this.Controls.Add(this.ConditionsDataGridView);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConditionsMapperTable_Form";
            this.Text = "Conditions Mapper";
            this.Load += new System.EventHandler(this.ConditionsMapperUpdate_Form_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deleteCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConditionsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox deleteCondition;
        private System.Windows.Forms.PictureBox editCondition;
        private System.Windows.Forms.Button ViewJson_button;
        private System.Windows.Forms.DataGridView ConditionsDataGridView;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_ApplyChanges;
        private System.Windows.Forms.PictureBox addCondition;
        private System.Windows.Forms.DataGridViewTextBoxColumn EncCondition;
        private System.Windows.Forms.DataGridViewTextBoxColumn BlendFollowUp;
        private System.Windows.Forms.DataGridViewTextBoxColumn EfolderName;
    }
}