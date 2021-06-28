using CommunityPlugin.Objects.Attributes;

namespace CommunityPlugin.Objects.Models.WCM.FieldExtraction
{
    public interface IDocument
    {
        int Id { get; set; }
        [UIMap(Ignore = true)]
        string FilePath { get; set; }
        [UIMap(Ignore = true)]
        string Hash { get; set; }
        string DocType { get; set; }
    }
}
