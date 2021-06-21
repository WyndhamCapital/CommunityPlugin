namespace CommunityPlugin.Objects.Models.WCM.FieldExtraction
{
    public interface IDocument
    {
        int Id { get; set; }
        string FilePath { get; set; }
        string Hash { get; set; }
        string DocType { get; set; }
    }
}
