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
            this.ConditionsGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.ConditionsGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ConditionsGridView1
            // 
            this.ConditionsGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConditionsGridView1.Location = new System.Drawing.Point(14, 74);
            this.ConditionsGridView1.Name = "ConditionsGridView1";
            this.ConditionsGridView1.Size = new System.Drawing.Size(774, 293);
            this.ConditionsGridView1.TabIndex = 0;
            // 
            // ConditionsMapperSelectPrelimCondition_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ConditionsGridView1);
            this.Name = "ConditionsMapperSelectPrelimCondition_Form";
            this.Text = "ConditionsMapper_SelectAutomedPrelimCondition_Form";
            this.Load += new System.EventHandler(this.ConditionsMapperSelectPrelimCondition_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ConditionsGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ConditionsGridView1;
    }
}