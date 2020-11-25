using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WyndhamLibGlobal.BlendPortal;

namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    public partial class ConditionsMapperEditCondition_Form : Form
    {

        public ConditionsMapperCondition TheCondition = null;

        public ConditionsMapperEditCondition_Form()
        {
            InitializeComponent();
        }

        public ConditionsMapperEditCondition_Form(ConditionsMapperCondition conditionToEdit)
        {
            InitializeComponent();
            this.LoadCondition(conditionToEdit);
        }

        private void LoadCondition(ConditionsMapperCondition condition)
        {
            this.TheCondition = condition;
            uwConditionNameTextBox.Text = condition.EncompassConditionName;
            eFolderName_textBox.Text = condition.EncompassEfolderName;
            mapConditionToEfolderCheckBox.Checked = condition.MapDocumentToEncompassEfolder;

            blendFollowUpTypeTextBox.Text = condition.BlendConditionName;
            uwConditionTemplateIdTextBox.Text = condition.EncompassUwConditionTemplateId;
            postConditionOnBlendAppsCheckBox.Checked = condition.PostConditionOnBlendApplications;
        }

        private ConditionsMapperCondition MapUiChangesToCondition()
        {
            TheCondition.EncompassConditionName = uwConditionNameTextBox.Text;
            TheCondition.EncompassEfolderName = eFolderName_textBox.Text;
            TheCondition.MapDocumentToEncompassEfolder = mapConditionToEfolderCheckBox.Checked;

            TheCondition.BlendConditionName = blendFollowUpTypeTextBox.Text;
            TheCondition.EncompassUwConditionTemplateId = uwConditionTemplateIdTextBox.Text;
            TheCondition.PostConditionOnBlendApplications = postConditionOnBlendAppsCheckBox.Checked;

            return TheCondition;
        }

        private void ConditionsMapperEditCondition_Form_Load(object sender, EventArgs e)
        {
            toolTipBlendFollowUpType.SetToolTip(blendFollowupHelpPictureBox, $"Optional: If there is a Blend Follow-Up Equivalent to this UW Condition 2 things will be accomplished: {Environment.NewLine + Environment.NewLine}" +
                $"1. During Prelim Conditions, if the loan is a Blend App and that Follow-up did not fire, we will post our WCM Prelim Condition.{Environment.NewLine}" +
                $"2. When Synching Blend conditions, we will map this Follow-Up to this Efolder.");

            toolTipBlendFollowUpType.SetToolTip(eFolderNameHelpPictureBox, $"Both prelim conditions and Blend Follow-Ups will be mapped to this eFolder.");
        }


        private void button_ApplyChanges_Click(object sender, EventArgs e)
        {
            if (!this.ValidateContents())
                return;

            this.MapUiChangesToCondition();
            this.DialogResult = DialogResult.OK;
        }

        private bool ValidateContents()
        {
            if (string.IsNullOrEmpty(uwConditionNameTextBox.Text))
            {
                MessageBox.Show("UW Condition Name is reqruired.");
                return false;
            }

            if (string.IsNullOrEmpty(eFolderName_textBox.Text))
            {
                MessageBox.Show("Efolder Name is reqruired.");
                return false;
            }

            if (string.IsNullOrEmpty(uwConditionTemplateIdTextBox.Text))
            {
                MessageBox.Show("Prelim Condition Template ID is reqruired.");
                return false;
            }


            return true;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void addPrelimCondition_Click(object sender, EventArgs e)
        {
            using (ConditionsMapperSelectPrelimCondition_Form selectPrelimForm = new ConditionsMapperSelectPrelimCondition_Form())
            {

                if (selectPrelimForm.ShowDialog((IWin32Window)this.ParentForm) != DialogResult.OK)
                    return;

                uwConditionNameTextBox.Text = selectPrelimForm.ConditionSelected.UwConditionName;
                uwConditionTemplateIdTextBox.Text = selectPrelimForm.ConditionSelected.UwTemplateId;
            }

        }
    }
}
