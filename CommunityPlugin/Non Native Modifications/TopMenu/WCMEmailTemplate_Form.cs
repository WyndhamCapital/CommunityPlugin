using CommunityPlugin.Objects.Helpers;
using CommunityPlugin.Objects.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    public partial class WCMEmailTemplate_Form : Form
    {

        List<AutomatedEmailTemplate> emailTemplates = null;
        public WCMEmailTemplate_Form()
        {
            InitializeComponent();
        }

        private

            void AutomedEmailTemplate_Form_Load(object sender, EventArgs e)
        {
            emailTemplates = Objects.Helpers.CDOHelper.GetCustomDataObjectValue<List<AutomatedEmailTemplate>>("AutomatedEmailTemplates.json");
            var templateTitles = emailTemplates.Select(x => x.TemplateName).ToArray();
            comboBox_Templates.Items.Add("");
            comboBox_Templates.Items.AddRange(templateTitles);
        }

        private void comboBox_Templates_SelectedIndexChanged(object sender, EventArgs e)
        {
            string templateName = comboBox_Templates.Text;

            AutomatedEmailTemplate template = null;
            if (string.IsNullOrEmpty(templateName))
            {
                template = new AutomatedEmailTemplate()
                {
                    TemplateName = templateName,
                    Email = new EmailMessageWcmSignatureRequest()
                };
            }
            else
            {
                template = emailTemplates.Where(x => x.TemplateName.Equals(templateName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            }

            EmailFromName_textBox.Text = string.IsNullOrEmpty(template.Email.FromFullName) ? "" : template.Email.FromFullName;
            EmailFromTitle_textBox.Text = string.IsNullOrEmpty(template.Email.FromTitle) ? "" : template.Email.FromTitle;
            EmailFromPhone_textBox.Text = string.IsNullOrEmpty(template.Email.FromPhoneNumber) ? "" : template.Email.FromPhoneNumber;
            EmailFromEmailAddr_textBox.Text = string.IsNullOrEmpty(template.Email.FromAddress) ? "" : template.Email.FromAddress;
            
            if (template.Email.ToAddresses == null || template.Email.ToAddresses.Any() == false)
            {
                EmailToAddreses_textBox.Text = "";
            }
            else
            {
                EmailToAddreses_textBox.Text = string.Join(",", template.Email.ToAddresses.Select(x => x.Trim()).ToArray());
            }

            if (template.Email.CcAddresses == null || template.Email.CcAddresses.Any() == false)
            {
                EmailCcAddreses_textBox.Text = "";
            }
            else
            {
                EmailCcAddreses_textBox.Text = string.Join(",", template.Email.CcAddresses.Select(x => x.Trim()).ToArray());
            }

            if (template.Email.BccAddresses == null || template.Email.BccAddresses.Any() == false)
            {
                EmailBccAddreses_textBox.Text = "";
            }
            else
            {
                EmailBccAddreses_textBox.Text = string.Join(",", template.Email.BccAddresses.Select(x => x.Trim()).ToArray());
            }

            EmailSubject_textBox.Text = string.IsNullOrEmpty(template.Email.MessageSubject) ? "" : template.Email.MessageSubject;
            EmailBody_richTextBox.Text = string.IsNullOrEmpty(template.Email.MessageBody) ? "" : template.Email.MessageBody;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string templateName = comboBox_Templates.Text;
            var template = emailTemplates.Where(x => x.TemplateName.Equals(templateName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (template == null)
            {
                string msg = "This looks like a new template; do you want to continue creating a new template?";
                DialogResult dialogResult = MessageBox.Show(msg, "New Template", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    template = new AutomatedEmailTemplate()
                    {
                        TemplateName = templateName,
                        Email = new EmailMessageWcmSignatureRequest()
                    };
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }

            }
            else
            {
                emailTemplates.Remove(template);
            }


            //update template with current textboxes
            template.Email.FromFullName = EmailFromName_textBox.Text;
            template.Email.FromTitle = EmailFromTitle_textBox.Text;
            template.Email.FromPhoneNumber = EmailFromPhone_textBox.Text;
            template.Email.FromAddress = EmailFromEmailAddr_textBox.Text;

            template.Email.ToAddresses = string.IsNullOrEmpty(EmailToAddreses_textBox.Text) ? new List<string>() : EmailToAddreses_textBox.Text.Split(',').Select(x => x.Trim()).ToList();
            template.Email.CcAddresses = string.IsNullOrEmpty(EmailCcAddreses_textBox.Text) ? new List<string>() : EmailCcAddreses_textBox.Text.Split(',').Select(x => x.Trim()).ToList();
            template.Email.BccAddresses = string.IsNullOrEmpty(EmailBccAddreses_textBox.Text) ? new List<string>() : EmailBccAddreses_textBox.Text.Split(',').Select(x => x.Trim()).ToList();

            template.Email.MessageSubject = EmailSubject_textBox.Text;
            template.Email.MessageBody = EmailBody_richTextBox.Text;

            emailTemplates.Add(template);

            CDOHelper.SaveObjectToJsonCDO("AutomatedEmailTemplates.json", emailTemplates);

            comboBox_Templates.Items.Clear();
            comboBox_Templates.Items.AddRange(emailTemplates.Select(x => x.TemplateName).ToArray());

            MessageBox.Show("Template updated");
        }

        private void button_deleteTemplate_Click(object sender, EventArgs e)
        {
            string templateName = comboBox_Templates.Text;
            var template = emailTemplates.Where(x => x.TemplateName.Equals(templateName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (template == null)
            {
                MessageBox.Show($"No template found '{templateName}'");
                return;
            }
            else
            {
                string msg = "Are you sure you wish to delete the template?";
                DialogResult dialogResult = MessageBox.Show(msg, "Delete Template", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    emailTemplates.Remove(template);
                    CDOHelper.SaveObjectToJsonCDO("AutomatedEmailTemplates.json", emailTemplates);

                    comboBox_Templates.Items.Clear();
                    comboBox_Templates.Items.AddRange(emailTemplates.Select(x => x.TemplateName).ToArray());

                    MessageBox.Show("Template Deleted");
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }

            }
        }
    }
}
