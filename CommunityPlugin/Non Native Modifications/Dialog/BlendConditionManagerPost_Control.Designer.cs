namespace CommunityPlugin.Non_Native_Modifications.Dialog
{
    partial class BlendConditionManagerPost_Control
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlendConditionManagerPost_Control));
            this.button_postConditionsToPortal = new System.Windows.Forms.Button();
            this.richTextBox_selectedCondToPostDescr = new System.Windows.Forms.RichTextBox();
            this.checkBox_Borrower = new System.Windows.Forms.CheckBox();
            this.checkBox_CoBorrower = new System.Windows.Forms.CheckBox();
            this.panelBorrowers = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelBorrowers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_postConditionsToPortal
            // 
            this.button_postConditionsToPortal.Location = new System.Drawing.Point(6, 182);
            this.button_postConditionsToPortal.Name = "button_postConditionsToPortal";
            this.button_postConditionsToPortal.Size = new System.Drawing.Size(189, 28);
            this.button_postConditionsToPortal.TabIndex = 39;
            this.button_postConditionsToPortal.Text = "Send Condition To Portal";
            this.button_postConditionsToPortal.UseVisualStyleBackColor = true;
            this.button_postConditionsToPortal.Click += new System.EventHandler(this.button_postConditionsToPortal_Click);
            // 
            // richTextBox_selectedCondToPostDescr
            // 
            this.richTextBox_selectedCondToPostDescr.Location = new System.Drawing.Point(6, 49);
            this.richTextBox_selectedCondToPostDescr.Name = "richTextBox_selectedCondToPostDescr";
            this.richTextBox_selectedCondToPostDescr.Size = new System.Drawing.Size(189, 127);
            this.richTextBox_selectedCondToPostDescr.TabIndex = 40;
            this.richTextBox_selectedCondToPostDescr.Text = "";
            // 
            // checkBox_Borrower
            // 
            this.checkBox_Borrower.AutoSize = true;
            this.checkBox_Borrower.Location = new System.Drawing.Point(0, 9);
            this.checkBox_Borrower.Name = "checkBox_Borrower";
            this.checkBox_Borrower.Size = new System.Drawing.Size(122, 17);
            this.checkBox_Borrower.TabIndex = 37;
            this.checkBox_Borrower.Text = "checkBox_Borrower";
            this.checkBox_Borrower.UseVisualStyleBackColor = true;
            // 
            // checkBox_CoBorrower
            // 
            this.checkBox_CoBorrower.AutoSize = true;
            this.checkBox_CoBorrower.Location = new System.Drawing.Point(0, 25);
            this.checkBox_CoBorrower.Name = "checkBox_CoBorrower";
            this.checkBox_CoBorrower.Size = new System.Drawing.Size(135, 17);
            this.checkBox_CoBorrower.TabIndex = 38;
            this.checkBox_CoBorrower.Text = "checkBox_CoBorrower";
            this.checkBox_CoBorrower.UseVisualStyleBackColor = true;
            this.checkBox_CoBorrower.Visible = false;
            // 
            // panelBorrowers
            // 
            this.panelBorrowers.Controls.Add(this.checkBox_CoBorrower);
            this.panelBorrowers.Controls.Add(this.checkBox_Borrower);
            this.panelBorrowers.Location = new System.Drawing.Point(44, 3);
            this.panelBorrowers.Name = "panelBorrowers";
            this.panelBorrowers.Size = new System.Drawing.Size(151, 41);
            this.panelBorrowers.TabIndex = 41;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 29);
            this.pictureBox1.TabIndex = 42;
            this.pictureBox1.TabStop = false;
            // 
            // BlendConditionManager_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panelBorrowers);
            this.Controls.Add(this.richTextBox_selectedCondToPostDescr);
            this.Controls.Add(this.button_postConditionsToPortal);
            this.Name = "BlendConditionManager_Control";
            this.Size = new System.Drawing.Size(200, 215);
            this.panelBorrowers.ResumeLayout(false);
            this.panelBorrowers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_postConditionsToPortal;
        private System.Windows.Forms.RichTextBox richTextBox_selectedCondToPostDescr;
        private System.Windows.Forms.CheckBox checkBox_Borrower;
        private System.Windows.Forms.CheckBox checkBox_CoBorrower;
        private System.Windows.Forms.Panel panelBorrowers;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
