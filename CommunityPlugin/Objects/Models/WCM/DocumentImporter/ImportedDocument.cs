namespace CommunityPlugin.Objects.Models.WCM.DocumentImporter
{
    // Class copied from BBW Core solution - EncompassGateway project
    public class ImportedDocument
    {
        public string EncompassAttachmentId { get; set; }
        public string EncompassEfolderId { get; set; }

        public bool EnableHighlighting { get; set; }

        public string ExternalSourceDocName { get; set; }

        // foreign key mappings below this line
        public int ExternalSourceId { get; set; }
        public int? ExternalSourceDocId { get; set; }

        public int ImportRequestId { get; set; }
    }
}