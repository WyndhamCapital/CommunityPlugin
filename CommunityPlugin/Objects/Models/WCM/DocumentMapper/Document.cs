using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CommunityPlugin.Objects.Models.WCM.DocumentMapper
{
    public class Document
    {
        public Document()
        {
            FieldMappings = new List<FieldMapping>();
        }

        public int Id { get; set; }
        public int ExternalDocumentSourceId { get; set; }

        public bool Enable { get; set; }

        public string ExternalSystemDocumentId { get; set; }

        public string EncompassEfolderName { get; set; }

        public IList<FieldMapping> FieldMappings { get; set; }

    }
}
