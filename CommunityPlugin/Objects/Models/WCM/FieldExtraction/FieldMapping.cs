namespace CommunityPlugin.Objects.Models.WCM.FieldExtraction
{
    public class FieldMapping : IFieldData
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int Confidence { get; set; }
        public int ClassifiedPageNumber { get; set; }
        public int OriginalPageNumber { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
