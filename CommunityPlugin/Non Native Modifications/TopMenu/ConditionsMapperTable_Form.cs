using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunityPlugin.Objects.Models;
using WyndhamLibGlobal.BlendPortal;

namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{

    public partial class ConditionsMapperTable_Form : Form
    {
        ConditionsMapperSettings ConditionMappingSettings = null;

        public ConditionsMapperTable_Form()
        {
            InitializeComponent();
        }

        private void ConditionsMapperUpdate_Form_Load(object sender, EventArgs e)
        {
            ConditionMappingSettings = WyndhamLibGlobal.GlobalEncompassUtility.GetConditionsMapperSettings();

            this.LoadConditions(ConditionMappingSettings);

        }

        private void LoadConditions(ConditionsMapperSettings conditionMappingSettings)
        {
            foreach (var condition in conditionMappingSettings.PrelimConditions)
            {
                var rowIndex = ConditionsDataGridView.Rows.Add(MapConditionToUiGrid(condition));
                ConditionsDataGridView.Rows[rowIndex].Tag = condition;
            }
        }

        private object[] MapConditionToUiGrid(ConditionsMapperCondition condition)
        {
            object[] arrayToDisplay = new object[ConditionsDataGridView.Columns.Count];

            arrayToDisplay[0] = condition.EncompassConditionName;
            arrayToDisplay[1] = condition.BlendConditionType;
            arrayToDisplay[2] = condition.EncompassEfolderName;

            return arrayToDisplay;
        }

        private void editCondition_Click(object sender, EventArgs e)
        {
            var selectedCell = ConditionsDataGridView.CurrentCell;
            if (selectedCell == null)
            {
                MessageBox.Show("No Row is selected, please try again.");
                return;
            }

            DataGridViewRow row = ConditionsDataGridView.Rows[ConditionsDataGridView.CurrentCell.RowIndex];
            if (row.IsNewRow)
            {
                MessageBox.Show("No Condition is selected, please try again.");
                return;
            }

            var conditionSelected = (ConditionsMapperCondition)row.Tag;

            using (ConditionsMapperEditCondition_Form editConditionDialog = new ConditionsMapperEditCondition_Form(conditionSelected))
            {

                if (editConditionDialog.ShowDialog((IWin32Window)this.ParentForm) != DialogResult.OK)
                    return;

                this.UpdateConditionInGridview(editConditionDialog.TheCondition);
            }
        }

        private void UpdateConditionInGridview(ConditionsMapperCondition theCondition)
        {
            int currentRowIndex = ConditionsDataGridView.CurrentCell.RowIndex;
            ConditionsDataGridView.Rows[currentRowIndex].SetValues(MapConditionToUiGrid(theCondition));
            ConditionsDataGridView.Rows[currentRowIndex].Tag = theCondition;
        }

        private void addCondition_Click(object sender, EventArgs e)
        {
            using (ConditionsMapperEditCondition_Form newConditionDialog = new ConditionsMapperEditCondition_Form())
            {

                if (newConditionDialog.ShowDialog((IWin32Window)this.ParentForm) != DialogResult.OK)
                    return;

                this.AddConditionToGridview(newConditionDialog.TheCondition);
            }
        }

        private void AddConditionToGridview(ConditionsMapperCondition theCondition)
        {
            var rowIndex = ConditionsDataGridView.Rows.Add(MapConditionToUiGrid(theCondition));
            ConditionsDataGridView.Rows[rowIndex].Tag = theCondition;
        }

        private void deleteCondition_Click(object sender, EventArgs e)
        {
            var selectedCell = ConditionsDataGridView.CurrentCell;
            if (selectedCell == null)
            {
                MessageBox.Show("No Row is selected, please try again.");
                return;
            }

            DataGridViewRow row = ConditionsDataGridView.Rows[ConditionsDataGridView.CurrentCell.RowIndex];
            if (row.IsNewRow)
            {
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Deleting a condition can NOT be undone. Do you wish to continue?", "Delete Condition", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ConditionsDataGridView.Rows.RemoveAt(ConditionsDataGridView.CurrentCell.RowIndex);
            }

        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_ApplyChanges_Click(object sender, EventArgs e)
        {
            List<ConditionsMapperCondition> prelimConditions = new List<ConditionsMapperCondition>();
            for (int i = 0; i < ConditionsDataGridView.Rows.Count; i++)
            {
                var row = ConditionsDataGridView.Rows[i];

                if (row.IsNewRow)
                    continue;

                var condition = (ConditionsMapperCondition)row.Tag;
                prelimConditions.Add(condition);
            }

            ConditionMappingSettings.PrelimConditions = prelimConditions;

            Objects.Helpers.CDOHelper.SaveObjectToJsonCDO(WyndhamLibGlobal.GlobalEncompassUtility.GetConditionsMapperSettingsFileName(), ConditionMappingSettings);

            MessageBox.Show("Changes Saved!");

        }
    }
}
