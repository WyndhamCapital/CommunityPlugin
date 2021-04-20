using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlugin.Objects.Models.WCM.DocumentImporter
{
    // SP - use this to export id's of different non-native encompass objects (like documents and attachments)
    // the non-native objects (attachmetns from UI Gridview) have ID's while the native objects do NOT
    // the non-native objects match the ID's from the API....
    public class MetaDataExporting
    {
        public IDictionary<string, string> NonNativeDocs { get; set; }
        public IDictionary<string, string> NativeDocs { get; set; }


        public MetaDataExporting()
        {
            NonNativeDocs = new Dictionary<string, string>();
            NativeDocs = new Dictionary<string, string>();
        }
    }
}
