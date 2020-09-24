using CommunityPlugin.Objects.Helpers;
using EllieMae.EMLite.Common;
using EllieMae.EMLite.DataEngine;
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
    public partial class CreateCustomFields_Form : Form
    {
        public CreateCustomFields_Form()
        {
            InitializeComponent();

            var formatValues = Enum.GetNames(typeof(FieldFormat));
            var valuesAlphabetized = formatValues.OrderBy(x => x).ToList();
            textBox_fieldFormatValues.Text = string.Join(Environment.NewLine, valuesAlphabetized.ToArray());
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyClipboard();
        }

        private void CopyClipboard()
        {
            DataObject d = dataGridView1.GetClipboardContent();
            Clipboard.SetDataObject(d);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteClipboard();
        }


        private void PasteClipboard()
        {
            try
            {

                string clipboardText = Clipboard.GetText();
                string[] rowsFromClipboard = clipboardText.Split('\n');

                // if row count is greater than 1, delete current rows to paste rows from clipboard
                if (rowsFromClipboard.Count() > 0)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Refresh();
                }

                int columnCount = dataGridView1.Columns.Count;
                int rowIndex = 0;
                foreach (string textForNewRow in rowsFromClipboard)
                {
                    rowIndex++;
                    // create a new row in the table for each row from the clipboard. 
                    if (!string.IsNullOrEmpty(textForNewRow))
                    {
                        string[] columnDataForThisRow = textForNewRow.Split('\t');
                        dataGridView1.Rows.Add(columnDataForThisRow);
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("The data you pasted is in the wrong format for the cell");
                return;
            }
        }

        private void button_CreateFields_Click(object sender, EventArgs e)
        {
            Dictionary<int, CustomFieldInfo> fieldsToCreate = new Dictionary<int, CustomFieldInfo>();

            List<string> errors = new List<string>();
            Dictionary<int, CustomFieldInfo> fieldsThatAlreadyExist = new Dictionary<int, CustomFieldInfo>();



            int errorIndex = 0;
            EllieMae.EMLite.ClientServer.IConfigurationManager configMan = EncompassHelper.SessionObjects.ConfigurationManager;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                List<string> rowErrors = new List<string>();

                DataGridViewRow row = dataGridView1.Rows[i];

                if (row.IsNewRow)
                    continue;

                string name = row.Cells[0].Value == null ? string.Empty : row.Cells[0].Value.ToString();
                if (string.IsNullOrEmpty(name))
                {
                    rowErrors.Add($"field id is empty");
                }
                else if (name.Length > 28)
                {
                    rowErrors.Add($"field id has '{name.Length}' characters and has a max of 28");
                }

                string desc = row.Cells[1].Value == null ? string.Empty : row.Cells[1].Value.ToString();
                if (string.IsNullOrEmpty(desc))
                {
                    rowErrors.Add($"description is empty");
                }

                FieldFormat format = Enum.TryParse(row.Cells[2].Value.ToString().ToUpper(), out format) ? format : FieldFormat.NONE;
                if (format == FieldFormat.NONE)
                {
                    var cellValue = row.Cells[2].Value ?? string.Empty;
                    rowErrors.Add($"field format '{cellValue.ToString()}'");
                }

                // we only need a valid 'length' for STRING fields
                int length = int.TryParse(row.Cells[3].Value.ToString(), out length) ? length : 0;
                if (format == FieldFormat.STRING && length == 0)
                {
                    rowErrors.Add($"length '{row.Cells[3].Value.ToString()}'");
                }

                if (rowErrors.Any())
                {
                    errorIndex++;
                    errors.Add($"{errorIndex}. Row {i + 1}. {string.Join(", ", rowErrors.ToArray())}");
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
                else
                {
                    var fieldToCreate = new CustomFieldInfo(name, desc, format, length, "");

                    var currentField = configMan.GetLoanCustomField(fieldToCreate.FieldID);
                    if (currentField != null)
                    {
                        fieldsThatAlreadyExist.Add(i, currentField);
                    }

                    fieldsToCreate.Add(i, fieldToCreate);
                }

            }

            if (errors.Any())
            {
                textBox_Results.Text = string.Join(Environment.NewLine, errors.ToArray());
                MessageBox.Show($"Error converting '{errors.Count}' rows to fields.");
                return;
            }

            if (fieldsThatAlreadyExist.Any())
            {

                foreach (var item in fieldsThatAlreadyExist)
                {
                    dataGridView1.Rows[item.Key].DefaultCellStyle.BackColor = Color.Yellow;
                }

                string msg = $"The fields below already exist, do you wish to overwrite them? {Environment.NewLine + string.Join(Environment.NewLine, fieldsThatAlreadyExist.Select(x => x.Value.FieldID).ToArray())}";
                DialogResult dialogResult = MessageBox.Show(msg, "Overwrite Already existing Fields?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Creating fields cancelled.");
                    return;
                }
            }


            foreach (var field in fieldsToCreate)
            {
                try
                {
                    configMan.UpdateLoanCustomField(field.Value);
                    dataGridView1.Rows[field.Key].DefaultCellStyle.BackColor = Color.LightGreen;

                }
                catch (Exception ex)
                {
                    errors.Add($"{field.Value.FieldID}. {ex.ToString()}");
                    dataGridView1.Rows[field.Key].DefaultCellStyle.BackColor = Color.Red;

                }
            }

            if (errors.Any())
            {
                MessageBox.Show($"Created '{fieldsToCreate.Count - errors.Count}' fields but had errors with '{errors.Count}'");
                textBox_Results.Text = string.Join(Environment.NewLine, errors.ToArray());
            }
            else
            {
                string msg = $"Created all fields ('{fieldsToCreate.Count}') successfully!";
                MessageBox.Show(msg);
                textBox_Results.Text = msg;
            }
        }
    }
}
