using CommunityPlugin.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    public class DocumentMapper : MenuItemBase
    {
        public override bool CanRun()
        {
            return PluginAccess.CheckAccess(nameof(DocumentMapper));
        }

        protected override void menuItem_Click(object sender, EventArgs e)
        {
            DocumentMapperTableForm f = new DocumentMapperTableForm();
            f.Show();
        }
    }
}
