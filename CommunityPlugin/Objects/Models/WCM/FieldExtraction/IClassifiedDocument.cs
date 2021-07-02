using System.Collections.Generic;
using CommunityPlugin.Objects.Attributes;

namespace CommunityPlugin.Objects.Models.WCM.FieldExtraction
{
    public interface IClassifiedDocument : IDocument
    {
        // SP - dynamically load gridview columns will exception on custom property
        [UIMap(Ignore = true)]
        IEnumerable<IFieldData> FieldData { get; set; }
        int Confidence { get; set; }


        // SP - used in UI to show number of fields extracted
        [UIMap(DisplayName = "# Fields")]
        int FieldDataCount { get; set; }
    }
}