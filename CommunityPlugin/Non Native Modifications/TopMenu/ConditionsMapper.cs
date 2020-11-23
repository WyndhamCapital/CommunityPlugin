using CommunityPlugin.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    public class ConditionsMapper : MenuItemBase
    {
        public override bool CanRun()
        {
            return PluginAccess.CheckAccess(nameof(ConditionsMapper));
        }

        protected override void menuItem_Click(object sender, EventArgs e)
        {
            ConditionsMapperTable_Form f = new ConditionsMapperTable_Form();
            f.Show();
        }
    }
}
