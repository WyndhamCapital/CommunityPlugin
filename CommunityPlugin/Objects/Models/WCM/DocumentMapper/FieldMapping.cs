using CommunityPlugin.Objects.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityPlugin.Objects.Models.WCM.DocumentMapper
{
    public class FieldMapping
    {
        [UIMap(DisplayName = "External Field ID")]
        [JsonProperty(Required = Required.Always)]
        public string ExternalFieldId { get; set; }

        [UIMap(DisplayName = "Encompass Field ID")]
        [JsonProperty(Required = Required.Always)]

        public string EncompassFieldId { get; set; }

        [UIMap(Editable = false)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public bool Enable { get; set; }

        [UIMap(Editable = false)]
        public int DocumentId { get; set; }

    }
}
