using CommunityPlugin.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    class WcmEmailTemplate : MenuItemBase
    {

        public override bool CanRun()
        {
            return PluginAccess.CheckAccess(nameof(WcmEmailTemplate));
        }

        protected override void menuItem_Click(object sender, EventArgs e)
        {
            WCMEmailTemplate_Form f = new WCMEmailTemplate_Form();
            f.Show();
        }
    }

}
