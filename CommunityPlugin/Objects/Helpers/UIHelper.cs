using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WyndhamLibGlobal;

namespace CommunityPlugin.Objects.Helpers
{
    public static class UIHelper
    {
        public static string FormatListOfExceptionsIntoErrorMessage(List<Exception> exceptions)
        {
           return string.Join(Environment.NewLine + Environment.NewLine, exceptions.Select(x => x.Message).ToList());
        }

        //public static void StartPleaseWaitForm(PleaseWaitForm pleaseWaitForm)
        //{
        //    Thread plsWaitThread = new Thread((ThreadStart)(() => Application.Run((Form)pleaseWaitForm)));
        //    plsWaitThread.SetApartmentState(ApartmentState.STA);
        //    plsWaitThread.Start();
        //    pleaseWaitForm.Focus();
        //}

        //public static PleaseWaitForm GeneratePleaseWaitDialog(Form startingForm)
        //{

        //    var pleaseWaitForm = new PleaseWaitForm();
        //    var progress = new Progress<string>(s => pleaseWaitForm.StatusLabel.Text = s);

        //    int currentX = startingForm.Location.X;
        //    var currentY = startingForm.Location.Y;
        //    var currentWidth = startingForm.Width;
        //    var currentHeight = startingForm.Height;

        //    var startX = (currentWidth / 2) + currentX - (pleaseWaitForm.Width / 2);
        //    var startY = (currentHeight / 2) + currentY - (pleaseWaitForm.Height / 2);
        //    pleaseWaitForm.StartPosition = FormStartPosition.Manual;
        //    pleaseWaitForm.Location = new Point(startX, startY);

        //    return pleaseWaitForm;
        //}
    }

}
