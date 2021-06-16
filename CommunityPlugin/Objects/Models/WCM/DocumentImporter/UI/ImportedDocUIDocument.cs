using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EllieMae.EMLite.DataEngine.Log;
using EllieMae.EMLite.UI;

namespace CommunityPlugin.Objects.Models.WCM.DocumentImporter.UI
{
    public class ImportedDocUIDocument
    {
        public ImportedDocUIDocument()
        {
            HighlightEfolderRow = false;
        }

        public bool HighlightEfolderRow { get; set; }
        public ExternalSource DocumentSource { get; set; }

        public DocumentLog EfolderDocumentDetails { get; set; }

        public GVItem EfolderGridViewRow { get; set; }


    }
}
