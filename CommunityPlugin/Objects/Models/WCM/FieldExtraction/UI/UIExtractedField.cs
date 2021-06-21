using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlugin.Objects.Models.WCM.FieldExtraction.UI
{
    public class UIExtractedField
    {
        public CommunityPlugin.Objects.Models.WCM.DocumentMapper.FieldMapping FieldMap { get; set; }
        public string ExternalSourceFieldId { get; set; }
        public string ExternalSourceFieldDescription { get; set; }
        public string EncompassFieldId { get; set; }
        public string EncompassFieldDescription { get; set; }
        public object EncompassFieldValue { get; set; }

        public string DataExtractionFieldValue { get; set; }

    }
}
