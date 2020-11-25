namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    partial class ConditionsMapperSelectPrelimCondition_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConditionsMapperSelectPrelimCondition_Form));
            this.ConditionsGridView1 = new System.Windows.Forms.DataGridView();
            this.ConditionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RuleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastModifiedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TemplateId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_ApplyChanges = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ConditionsGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ConditionsGridView1
            // 
            this.ConditionsGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConditionsGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ConditionName,
            this.RuleName,
            this.LastModifiedBy,
            this.TemplateId});
            this.ConditionsGridView1.Location = new System.Drawing.Point(12, 12);
            this.ConditionsGridView1.MultiSelect = false;
            this.ConditionsGridView1.Name = "ConditionsGridView1";
            this.ConditionsGridView1.RowHeadersVisible = false;
            this.ConditionsGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ConditionsGridView1.Size = new System.Drawing.Size(614, 293);
            this.ConditionsGridView1.TabIndex = 0;
            // 
            // ConditionName
            // 
            this.ConditionName.HeaderText = "Condition Name";
            this.ConditionName.Name = "ConditionName";
            this.ConditionName.Width = 200;
            // 
            // RuleName
            // 
            this.RuleName.HeaderText = "Business Rule Name";
            this.RuleName.Name = "RuleName";
            this.RuleName.Width = 200;
            // 
            // LastModifiedBy
            // 
            this.LastModifiedBy.HeaderText = "Rule Last Edited By";
            this.LastModifiedBy.Name = "LastModifiedBy";
            this.LastModifiedBy.Width = 85;
            // 
            // TemplateId
            // 
            this.TemplateId.HeaderText = "Condition Id";
            this.TemplateId.Name = "TemplateId";
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(536, 314);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(90, 33);
            this.button_Cancel.TabIndex = 22;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_ApplyChanges
            // 
            this.button_ApplyChanges.Location = new System.Drawing.Point(440, 314);
            this.button_ApplyChanges.Name = "button_ApplyChanges";
            this.button_ApplyChanges.Size = new System.Drawing.Size(90, 33);
            this.button_ApplyChanges.TabIndex = 21;
            this.button_ApplyChanges.Text = "Select";
            this.button_ApplyChanges.UseVisualStyleBackColor = true;
            this.button_ApplyChanges.Click += new System.EventHandler(this.button_ApplyChanges_Click);
            // 
            // ConditionsMapperSelectPrelimCondition_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 359);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_ApplyChanges);
            this.Controls.Add(this.ConditionsGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConditionsMapperSelectPrelimCondition_Form";
            this.RightToLeftLayout = true;
            this.Text = "Select Automed Prelim Condition";
            this.Load += new System.EventHandler(this.ConditionsMapperSelectPrelimCondition_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ConditionsGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ConditionsGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConditionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RuleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastModifiedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn TemplateId;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_ApplyChanges;
    }
}