
namespace CommunityPlugin.Non_Native_Modifications.Dialog
{
    partial class DataExtractionField_Control
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.loanValueTextBox = new System.Windows.Forms.TextBox();
            this.dataExtractionValueTextBox = new System.Windows.Forms.TextBox();
            this.encompassFieldDescrLabel = new System.Windows.Forms.Label();
            this.dataExtractionFieldDescrLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // loanValueTextBox
            // 
            this.loanValueTextBox.Location = new System.Drawing.Point(203, 18);
            this.loanValueTextBox.Name = "loanValueTextBox";
            this.loanValueTextBox.Size = new System.Drawing.Size(138, 20);
            this.loanValueTextBox.TabIndex = 0;
            // 
            // dataExtractionValueTextBox
            // 
            this.dataExtractionValueTextBox.Location = new System.Drawing.Point(347, 18);
            this.dataExtractionValueTextBox.Name = "dataExtractionValueTextBox";
            this.dataExtractionValueTextBox.Size = new System.Drawing.Size(138, 20);
            this.dataExtractionValueTextBox.TabIndex = 1;
            // 
            // encompassFieldDescrLabel
            // 
            this.encompassFieldDescrLabel.Location = new System.Drawing.Point(13, 21);
            this.encompassFieldDescrLabel.Name = "encompassFieldDescrLabel";
            this.encompassFieldDescrLabel.Size = new System.Drawing.Size(177, 40);
            this.encompassFieldDescrLabel.TabIndex = 2;
            this.encompassFieldDescrLabel.Text = "encompassFieldDescrLabel";
            // 
            // dataExtractionFieldDescrLabel
            // 
            this.dataExtractionFieldDescrLabel.Location = new System.Drawing.Point(13, 61);
            this.dataExtractionFieldDescrLabel.Name = "dataExtractionFieldDescrLabel";
            this.dataExtractionFieldDescrLabel.Size = new System.Drawing.Size(177, 40);
            this.dataExtractionFieldDescrLabel.TabIndex = 3;
            this.dataExtractionFieldDescrLabel.Text = "dataExtractionFieldDescrLabel";
            // 
            // DataExtractionField_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataExtractionFieldDescrLabel);
            this.Controls.Add(this.encompassFieldDescrLabel);
            this.Controls.Add(this.dataExtractionValueTextBox);
            this.Controls.Add(this.loanValueTextBox);
            this.Name = "DataExtractionField_Control";
            this.Size = new System.Drawing.Size(499, 107);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox loanValueTextBox;
        private System.Windows.Forms.TextBox dataExtractionValueTextBox;
        private System.Windows.Forms.Label encompassFieldDescrLabel;
        private System.Windows.Forms.Label dataExtractionFieldDescrLabel;
    }
}
