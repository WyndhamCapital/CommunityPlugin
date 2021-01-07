using CommunityPlugin.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    public class UnassignRole : MenuItemBase
    {
        public override bool CanRun()
        {
            return PluginAccess.CheckAccess(nameof(UnassignRole));
        }

        protected override void menuItem_Click(object sender, EventArgs e)
        {
            UnassignRole_Form form = new UnassignRole_Form();
            form.Show();
        }
    }

}
