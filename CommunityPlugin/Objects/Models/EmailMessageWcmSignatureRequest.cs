using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlugin.Objects.Models
{
    public class EmailMessageWcmSignatureRequest
    {

        [JsonProperty(Required = Required.Always)]
        public string FromFullName { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string FromTitle { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string FromPhoneNumber { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string FromAddress { get; set; }

        [JsonProperty(Required = Required.Always)]
        public List<string> ToAddresses { get; set; }

        [JsonProperty(Required = Required.Default)]
        public List<string> CcAddresses { get; set; }

        [JsonProperty(Required = Required.Default)]
        public List<string> BccAddresses { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string MessageSubject { get; set; }

        [JsonProperty(Required = Required.Default)]
        public string MessageBody { get; set; }

        [JsonProperty(Required = Required.Default)]
        public List<Attachment> Attachments { get; set; }
    }

    public class Attachment
    {
        [JsonProperty(Required = Required.Always)]
        public string AttachmentName { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Base64EncodedAttachment { get; set; }
    }
}
