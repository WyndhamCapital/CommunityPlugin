namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    partial class UnassignRole_Form
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
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rolesDD = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonStartUnassign = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Location = new System.Drawing.Point(13, 61);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(195, 405);
            this.flowLayoutPanel.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rolesDD);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(13, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(198, 43);
            this.panel1.TabIndex = 2;
            // 
            // rolesDD
            // 
            this.rolesDD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rolesDD.FormattingEnabled = true;
            this.rolesDD.Location = new System.Drawing.Point(53, 3);
            this.rolesDD.Name = "rolesDD";
            this.rolesDD.Size = new System.Drawing.Size(132, 21);
            this.rolesDD.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Role:";
            // 
            // buttonStartUnassign
            // 
            this.buttonStartUnassign.Location = new System.Drawing.Point(217, 13);
            this.buttonStartUnassign.Name = "buttonStartUnassign";
            this.buttonStartUnassign.Size = new System.Drawing.Size(137, 42);
            this.buttonStartUnassign.TabIndex = 3;
            this.buttonStartUnassign.Text = "Start Unassign";
            this.buttonStartUnassign.UseVisualStyleBackColor = true;
            this.buttonStartUnassign.Click += new System.EventHandler(this.buttonStartUnassign_ClickAsync);
            // 
            // statusLabel
            // 
            this.statusLabel.Location = new System.Drawing.Point(214, 61);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(140, 60);
            this.statusLabel.TabIndex = 4;
            this.statusLabel.Text = "Status Label";
            this.statusLabel.Visible = false;
            // 
            // UnassignRole_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 475);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.buttonStartUnassign);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel);
            this.Name = "UnassignRole_Form";
            this.Text = "UnassignRole_Form";
            this.Load += new System.EventHandler(this.UnassignRole_Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox rolesDD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonStartUnassign;
        private System.Windows.Forms.Label statusLabel;
    }
}