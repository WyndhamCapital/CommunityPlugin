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
            TheCondition.EncompassUwConditionTemplateId = uwConditionNameTextBox.Text;
            TheCondition.PostConditionOnBlendApplications = postConditionOnBlendAppsCheckBox.Checked;

            return TheCondition;
        }

        private void ConditionsMapperEditCondition_Form_Load(object sender, EventArgs e)
        {
            toolTipBlendFollowUpType.SetToolTip(blendFollowUpTypeTextBox, "Optional: if there is a Blend equivalent for this condition, if the loan needs this condition and the follow-up did NOT fire in Blend, we will fire it.");
            toolTipUwConditionName.SetToolTip(uwConditionNameTextBox, "This field doesn't actually control anything and is strictly for our record keeping.");
        }


        private void button_ApplyChanges_Click(object sender, EventArgs e)
        {
            this.MapUiChangesToCondition();
            this.DialogResult = DialogResult.OK;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void addCondition_Click(object sender, EventArgs e)
        {

        }
    }
}
