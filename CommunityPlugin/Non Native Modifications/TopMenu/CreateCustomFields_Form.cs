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

                foreach (string textForNewRow in rowsFromClipboard)
                {
                    // create a new row in the table for each row from the clipboard. 
                    if (!string.IsNullOrEmpty(textForNewRow))
                    {
                        string[] columnDataForThisRow = textForNewRow.Split('\t');
                        if (columnDataForThisRow.Count() > 1)
                        {
                            MessageBox.Show($"Error: There should be only 1 column for the loan numbers and there are {columnDataForThisRow.Count()} columns.");
                            break;
                        }
                        else
                        {
                            dataGridView1.Rows.Add(textForNewRow);
                        }

                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("The data you pasted is in the wrong format for the cell");
                return;
            }
        }
    }
}
