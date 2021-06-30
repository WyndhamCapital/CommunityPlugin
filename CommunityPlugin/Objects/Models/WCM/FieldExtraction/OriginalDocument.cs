namespace CommunityPlugin.Objects.Models.WCM.FieldExtraction
{
    public class OriginalDocument : IOriginalDocument
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public string FilePath { get; set; }
        public string DocType { get; set; }
    }
}
