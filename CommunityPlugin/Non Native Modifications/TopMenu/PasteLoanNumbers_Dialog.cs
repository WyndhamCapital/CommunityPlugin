using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    public partial class PasteLoanNumbers_Dialog : UserControl
    {
        public PasteLoanNumbers_Dialog()
        {
            InitializeComponent();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyClipboard();
        }


        private void CopyClipboard()
        {
            DataObject d = dataGridView_LoanNumbers.GetClipboardContent();
            Clipboard.SetDataObject(d);
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
                    dataGridView_LoanNumbers.Rows.Clear();
                    dataGridView_LoanNumbers.Refresh();
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
                            dataGridView_LoanNumbers.Rows.Add(textForNewRow);
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

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteClipboard();
        }

        public List<string> GetLoanNumbersFromDataGrid()
        {
            List<string> loanNumbersToReturn = new List<string>();
            int loanCount = 0;
            foreach (DataGridViewRow row in dataGridView_LoanNumbers.Rows)
            {
                if (row.Cells[0].Value != null && !string.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                {
                    string loanNumber = row.Cells[0].Value.ToString().Trim();
                    loanNumbersToReturn.Add(loanNumber);

                    loanCount++;
                }
            }

            return loanNumbersToReturn;
        }

    }
}
