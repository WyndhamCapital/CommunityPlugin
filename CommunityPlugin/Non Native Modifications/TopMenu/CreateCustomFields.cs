using CommunityPlugin.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{

    public class CreateCustomFields : MenuItemBase
    {
        public override bool CanRun()
        {
            return PluginAccess.CheckAccess(nameof(CreateCustomFields));
        }

        protected override void menuItem_Click(object sender, EventArgs e)
        {
            CreateCustomFields_Form f = new CreateCustomFields_Form();
            f.Show();
        }
    }
}
