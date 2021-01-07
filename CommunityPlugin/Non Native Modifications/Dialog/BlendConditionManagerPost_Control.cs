using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommunityPlugin.Non_Native_Modifications.Dialog
{
    public partial class BlendConditionManagerPost_Control : UserControl
    {
        public BlendConditionManagerPost_Control(string borrowerName, bool selectBorrower = true)
        {
            InitializeComponent();
            checkBox_Borrower.Text = borrowerName;
            checkBox_Borrower.Checked = selectBorrower;
        }

        private void button_postConditionsToPortal_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Gotchya!");
        }
    }
}
