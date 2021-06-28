
namespace CommunityPlugin.Objects.Helpers
{
    partial class PleaseWaitForm
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
            this.label_Status = new System.Windows.Forms.Label();
            this.picture_Spinner = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picture_Spinner)).BeginInit();
            this.SuspendLayout();
            // 
            // label_Status
            // 
            this.label_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Status.Location = new System.Drawing.Point(146, 35);
            this.label_Status.Name = "label_Status";
            this.label_Status.Size = new System.Drawing.Size(330, 86);
            this.label_Status.TabIndex = 3;
            this.label_Status.Text = "Loading...";
            // 
            // picture_Spinner
            // 
            this.picture_Spinner.Image = global::CommunityPlugin.Properties.Resources.WyndhamWaiting;
            this.picture_Spinner.Location = new System.Drawing.Point(12, 22);
            this.picture_Spinner.Name = "picture_Spinner";
            this.picture_Spinner.Size = new System.Drawing.Size(112, 99);
            this.picture_Spinner.TabIndex = 2;
            this.picture_Spinner.TabStop = false;
            // 
            // PleaseWaitDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(496, 146);
            this.Controls.Add(this.label_Status);
            this.Controls.Add(this.picture_Spinner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PleaseWaitDialog";
            this.Text = "PleaseWaitDialog";
            ((System.ComponentModel.ISupportInitialize)(this.picture_Spinner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_Status;
        private System.Windows.Forms.PictureBox picture_Spinner;
    }
}