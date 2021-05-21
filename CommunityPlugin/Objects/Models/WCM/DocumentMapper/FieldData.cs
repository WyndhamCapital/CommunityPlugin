using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityPlugin.Objects.Models.WCM.DocumentMapper
{
    public class FieldData
    {
        public int Id { get; set; }

        public int DocumentId { get; set; }

        public bool Enable { get; set; }

        public string ExternalFieldId { get; set; }
        public string EncompassFieldId { get; set; }

    }
}
