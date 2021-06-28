using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommunityPlugin.Objects.Helpers
{
    public partial class PleaseWaitForm : Form
    {
        public Label StatusLabel { get; set; }


        public PleaseWaitForm()
        {
            InitializeComponent();
            StatusLabel = label_Status;
        }
    }
}
