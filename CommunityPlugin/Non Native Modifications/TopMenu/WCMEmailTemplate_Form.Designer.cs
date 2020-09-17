namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    partial class WCMEmailTemplate_Form
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
            System.Windows.Forms.Label label13;
            this.comboBox_Templates = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.EmailBody_richTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.EmailSubject_textBox = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.EmailBccAddreses_textBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.EmailCcAddreses_textBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.EmailToAddreses_textBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.EmailFromEmailAddr_textBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.EmailFromPhone_textBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.EmailFromTitle_textBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.EmailFromName_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_UpdateTemplate = new System.Windows.Forms.Button();
            this.button_deleteTemplate = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(14, 274);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(54, 13);
            label13.TabIndex = 11;
            label13.Text = "*Required";
            label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // comboBox_Templates
            // 
            this.comboBox_Templates.FormattingEnabled = true;
            this.comboBox_Templates.Location = new System.Drawing.Point(66, 12);
            this.comboBox_Templates.Name = "comboBox_Templates";
            this.comboBox_Templates.Size = new System.Drawing.Size(296, 21);
            this.comboBox_Templates.TabIndex = 6;
            this.comboBox_Templates.SelectedIndexChanged += new System.EventHandler(this.comboBox_Templates_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Templates";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(label13);
            this.panel1.Controls.Add(this.EmailBody_richTextBox);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.EmailSubject_textBox);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(8, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(783, 296);
            this.panel1.TabIndex = 8;
            // 
            // EmailBody_richTextBox
            // 
            this.EmailBody_richTextBox.Location = new System.Drawing.Point(418, 10);
            this.EmailBody_richTextBox.Multiline = true;
            this.EmailBody_richTextBox.Name = "EmailBody_richTextBox";
            this.EmailBody_richTextBox.Size = new System.Drawing.Size(355, 257);
            this.EmailBody_richTextBox.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Subject*";
            // 
            // EmailSubject_textBox
            // 
            this.EmailSubject_textBox.Location = new System.Drawing.Point(80, 7);
            this.EmailSubject_textBox.Name = "EmailSubject_textBox";
            this.EmailSubject_textBox.Size = new System.Drawing.Size(274, 20);
            this.EmailSubject_textBox.TabIndex = 17;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.EmailBccAddreses_textBox);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.EmailCcAddreses_textBox);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.EmailToAddreses_textBox);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Location = new System.Drawing.Point(11, 170);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(345, 97);
            this.panel3.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "BCC";
            // 
            // EmailBccAddreses_textBox
            // 
            this.EmailBccAddreses_textBox.Location = new System.Drawing.Point(81, 68);
            this.EmailBccAddreses_textBox.Name = "EmailBccAddreses_textBox";
            this.EmailBccAddreses_textBox.Size = new System.Drawing.Size(262, 20);
            this.EmailBccAddreses_textBox.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(2, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "CC";
            // 
            // EmailCcAddreses_textBox
            // 
            this.EmailCcAddreses_textBox.Location = new System.Drawing.Point(81, 44);
            this.EmailCcAddreses_textBox.Name = "EmailCcAddreses_textBox";
            this.EmailCcAddreses_textBox.Size = new System.Drawing.Size(262, 20);
            this.EmailCcAddreses_textBox.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(2, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "To Addresses*";
            // 
            // EmailToAddreses_textBox
            // 
            this.EmailToAddreses_textBox.Location = new System.Drawing.Point(81, 20);
            this.EmailToAddreses_textBox.Name = "EmailToAddreses_textBox";
            this.EmailToAddreses_textBox.Size = new System.Drawing.Size(262, 20);
            this.EmailToAddreses_textBox.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Recipient";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(377, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Body*";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.EmailFromEmailAddr_textBox);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.EmailFromPhone_textBox);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.EmailFromTitle_textBox);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.EmailFromName_textBox);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(11, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(345, 129);
            this.panel2.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Email Addr*";
            // 
            // EmailFromEmailAddr_textBox
            // 
            this.EmailFromEmailAddr_textBox.Location = new System.Drawing.Point(69, 101);
            this.EmailFromEmailAddr_textBox.Name = "EmailFromEmailAddr_textBox";
            this.EmailFromEmailAddr_textBox.Size = new System.Drawing.Size(274, 20);
            this.EmailFromEmailAddr_textBox.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Phone No.*";
            // 
            // EmailFromPhone_textBox
            // 
            this.EmailFromPhone_textBox.Location = new System.Drawing.Point(69, 77);
            this.EmailFromPhone_textBox.Name = "EmailFromPhone_textBox";
            this.EmailFromPhone_textBox.Size = new System.Drawing.Size(274, 20);
            this.EmailFromPhone_textBox.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Title*";
            // 
            // EmailFromTitle_textBox
            // 
            this.EmailFromTitle_textBox.Location = new System.Drawing.Point(69, 53);
            this.EmailFromTitle_textBox.Name = "EmailFromTitle_textBox";
            this.EmailFromTitle_textBox.Size = new System.Drawing.Size(274, 20);
            this.EmailFromTitle_textBox.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Full Name*";
            // 
            // EmailFromName_textBox
            // 
            this.EmailFromName_textBox.Location = new System.Drawing.Point(69, 30);
            this.EmailFromName_textBox.Name = "EmailFromName_textBox";
            this.EmailFromName_textBox.Size = new System.Drawing.Size(274, 20);
            this.EmailFromName_textBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Sender";
            // 
            // button_UpdateTemplate
            // 
            this.button_UpdateTemplate.Location = new System.Drawing.Point(427, 10);
            this.button_UpdateTemplate.Name = "button_UpdateTemplate";
            this.button_UpdateTemplate.Size = new System.Drawing.Size(126, 23);
            this.button_UpdateTemplate.TabIndex = 9;
            this.button_UpdateTemplate.Text = "Update Template";
            this.button_UpdateTemplate.UseVisualStyleBackColor = true;
            this.button_UpdateTemplate.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_deleteTemplate
            // 
            this.button_deleteTemplate.Location = new System.Drawing.Point(655, 10);
            this.button_deleteTemplate.Name = "button_deleteTemplate";
            this.button_deleteTemplate.Size = new System.Drawing.Size(126, 23);
            this.button_deleteTemplate.TabIndex = 10;
            this.button_deleteTemplate.Text = "Delete Template";
            this.button_deleteTemplate.UseVisualStyleBackColor = true;
            this.button_deleteTemplate.Click += new System.EventHandler(this.button_deleteTemplate_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(81, 4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(150, 13);
            this.label14.TabIndex = 14;
            this.label14.Text = "Separate emails with a comma";
            // 
            // WCMEmailTemplate_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 343);
            this.Controls.Add(this.button_deleteTemplate);
            this.Controls.Add(this.button_UpdateTemplate);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.comboBox_Templates);
            this.Controls.Add(this.label1);
            this.Name = "WCMEmailTemplate_Form";
            this.Text = "AutomedEmailTemplate_Form";
            this.Load += new System.EventHandler(this.AutomedEmailTemplate_Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ComboBox_Templates_TextChanged(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_Templates;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox EmailSubject_textBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox EmailBccAddreses_textBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox EmailCcAddreses_textBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox EmailToAddreses_textBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox EmailFromEmailAddr_textBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox EmailFromPhone_textBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox EmailFromTitle_textBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox EmailFromName_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_UpdateTemplate;
        private System.Windows.Forms.TextBox EmailBody_richTextBox;
        private System.Windows.Forms.Button button_deleteTemplate;
        private System.Windows.Forms.Label label14;
    }
}