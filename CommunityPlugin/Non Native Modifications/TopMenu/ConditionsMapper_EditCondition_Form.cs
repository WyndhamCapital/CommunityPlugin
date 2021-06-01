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
    public partial class ConditionsMapper_EditCondition_Form : Form
    {

        public ConditionsMapperCondition TheCondition = null;

        public ConditionsMapper_EditCondition_Form()
        {
            InitializeComponent();
        }

        public ConditionsMapper_EditCondition_Form(ConditionsMapperCondition conditionToEdit)
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

            blendFollowUpTypeTextBox.Text = condition.BlendConditionType;
            uwConditionTemplateIdTextBox.Text = condition.EncompassUwConditionTemplateId;
        }

        private ConditionsMapperCondition MapUiChangesToCondition()
        {
            if (TheCondition == null)
            {
                TheCondition = new ConditionsMapperCondition();
            }

            TheCondition.EncompassConditionName = uwConditionNameTextBox.Text;
            TheCondition.EncompassEfolderName = eFolderName_textBox.Text;
            TheCondition.MapDocumentToEncompassEfolder = mapConditionToEfolderCheckBox.Checked;

            TheCondition.BlendConditionType = blendFollowUpTypeTextBox.Text;
            TheCondition.EncompassUwConditionTemplateId = uwConditionTemplateIdTextBox.Text;

            return TheCondition;
        }

        private void ConditionsMapperEditCondition_Form_Load(object sender, EventArgs e)
        {
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
            if (string.IsNullOrEmpty(eFolderName_textBox.Text))
            {
                MessageBox.Show("Efolder Name is reqruired.");
                return false;
            }

            if (string.IsNullOrEmpty(uwConditionNameTextBox.Text))
            {
                MessageBox.Show("UW Condition Name is reqruired.");
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
            using (ConditionsMapper_SelectPrelimCondition_Form selectPrelimForm = new ConditionsMapper_SelectPrelimCondition_Form())
            {

                if (selectPrelimForm.ShowDialog((IWin32Window)this.ParentForm) != DialogResult.OK)
                    return;

                uwConditionNameTextBox.Text = selectPrelimForm.ConditionSelected.UwConditionName;
                uwConditionTemplateIdTextBox.Text = selectPrelimForm.ConditionSelected.UwTemplateId;
            }

        }
    }
}
