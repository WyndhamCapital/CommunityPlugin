using System.ComponentModel.DataAnnotations;

namespace CommunityPlugin.Objects.Models.WCM.DocumentMapper
{
    public class ExternalDocumentSource
    {
        public int Id { get; set; }
        public string SourceName { get; set; }
        public bool Enable { get; set; }
    }
}
