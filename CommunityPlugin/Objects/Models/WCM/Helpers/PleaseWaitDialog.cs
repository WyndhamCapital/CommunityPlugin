using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunityPlugin.Objects.Helpers;

namespace CommunityPlugin.Objects.Models.WCM.Helpers
{
    public class PleaseWaitDialog
    {
        public PleaseWaitForm PleaseWaitForm { get; set; }
        public IProgress<string> Progress { get; set; }


        public PleaseWaitDialog()
        {
            var pleaseWaitForm = new PleaseWaitForm();
            var progress = new Progress<string>(s => pleaseWaitForm.StatusLabel.Text = s);

            PleaseWaitForm = pleaseWaitForm;
            Progress = progress;

            StartForm();
        }

        // SP - Please Wait Form will start in upper-right quadrant of screen
        // Constructor with no params will start form wherever Windows starts it
        public PleaseWaitDialog(Form currentForm)
        {
            var pleaseWaitForm = new PleaseWaitForm();
            int currentX = currentForm.Location.X;
            var currentY = currentForm.Location.Y;
            var currentWidth = currentForm.Width;
            var currentHeight = currentForm.Height;

            var startX = (currentWidth / 2) + currentX - (pleaseWaitForm.Width / 2);
            var startY = (currentHeight / 2) + currentY - (pleaseWaitForm.Height / 2);
            pleaseWaitForm.StartPosition = FormStartPosition.Manual;
            pleaseWaitForm.Location = new Point(startX, startY);

            var progress = new Progress<string>(s => pleaseWaitForm.StatusLabel.Text = s);

            PleaseWaitForm = pleaseWaitForm;
            Progress = progress;

            StartForm();
        }

        public void StartForm()
        {
            Thread plsWaitThread = new Thread((ThreadStart)(() => Application.Run((Form)PleaseWaitForm)));
            plsWaitThread.SetApartmentState(ApartmentState.STA);
            plsWaitThread.Start();
            PleaseWaitForm.Focus();
        }


    }
}
