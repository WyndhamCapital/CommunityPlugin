namespace CommunityPlugin.Objects.Models.WCM.FieldExtraction
{
    public interface IFieldData
    {
        int Id { get; set; }
        int Confidence { get; set; }
        string Key { get; set; }
        string Value { get; set; }
        int ClassifiedPageNumber { get; set; }
        int OriginalPageNumber { get; set; }
        int PositionX { get; set; }
        int PositionY { get; set; }
        int Width { get; set; }
        int Height { get; set; }
    }
}