using System.Collections.Generic;
using Newtonsoft.Json;

namespace CommunityPlugin.Objects.Models.WCM.FieldExtraction
{
    public class ClassifiedDocument : OriginalDocument, IClassifiedDocument
    {
        public ClassifiedDocument() { }

        [JsonConstructor]
        public ClassifiedDocument(List<FieldMapping> fieldData)
        {
            FieldData = fieldData;
        }



        public IEnumerable<IFieldData> FieldData { get; set; }
        public int Confidence { get; set; }

        public int FieldDataCount { get; set; }
    }
}
