using CommunityPlugin.Objects.Models;
using EllieMae.EMLite.ClientServer;
using EllieMae.Encompass.Automation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunityPlugin.Objects.Helpers;

namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    public partial class ConditionsMapperSelectPrelimCondition_Form : Form
    {

        public AutomedPrelimCondition ConditionSelected = null;

        public ConditionsMapperSelectPrelimCondition_Form()
        {
            InitializeComponent();
        }

        public object[] MapConditionToUi { get; private set; }

        private void ConditionsMapperSelectPrelimCondition_Form_Load(object sender, EventArgs e)
        {

            var conditions = EncompassHelper.GetAutomedConditionBusinessRuleConditions();
            this.LoadConditions(conditions);
        }

        private void LoadConditions(GetAutomatedPrelimConditionsResponse conditions)
        {
            foreach (var cond in conditions.Conditions)
            {
               var rowIndex = ConditionsGridView1.Rows.Add(MapConditionToUiGrid(cond));
                ConditionsGridView1.Rows[rowIndex].Tag = cond;
            }
        }


        private object[] MapConditionToUiGrid(AutomedPrelimCondition condition)
        {
            object[] arrayToDisplay = new object[ConditionsGridView1.Columns.Count];

            arrayToDisplay[0] = condition.UwConditionName;
            arrayToDisplay[1] = condition.BusinessRuleName;
            arrayToDisplay[2] = condition.BusinesRuleLastModifiedBy;
            arrayToDisplay[3] = condition.UwTemplateId;

            return arrayToDisplay;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button_ApplyChanges_Click(object sender, EventArgs e)
        {

            if (!this.ValidateContents())
                return;

            // get condition from gridview first
            this.GetConditionSelected();

            this.DialogResult = DialogResult.OK;
        }

        private void GetConditionSelected()
        {
            DataGridViewRow row = ConditionsGridView1.Rows[ConditionsGridView1.CurrentCell.RowIndex];
            var conditionSelected = (AutomedPrelimCondition)row.Tag;
            ConditionSelected = conditionSelected;
        }

        private bool ValidateContents()
        {
            var selectedCell = ConditionsGridView1.CurrentCell;
            if (selectedCell == null)
            {
                MessageBox.Show("No Row is selected, please try again.");
                return false;
            }

            DataGridViewRow row = ConditionsGridView1.Rows[ConditionsGridView1.CurrentCell.RowIndex];
            if (row.IsNewRow)
            {
                MessageBox.Show("No Condition is selected, please try again.");
                return false;
            }

            return true;

        }
    }
}
