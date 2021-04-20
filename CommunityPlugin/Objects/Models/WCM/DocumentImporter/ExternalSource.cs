using System.Drawing;

namespace CommunityPlugin.Objects.Models.WCM.DocumentImporter
{
    public class ExternalSource
    {
        public string SourceName { get; set; }
        public int Id { get; set; }

        public string NewDocumentHexColor { get; set; }
        public string AlreadyViewedDocHexColor { get; set; }

        public Color AlreadyViewedDocumentColor { get; set; }
        public Color NewDocumentColor { get; set; }

    }
}