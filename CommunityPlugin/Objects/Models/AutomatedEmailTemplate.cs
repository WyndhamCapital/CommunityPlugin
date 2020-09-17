using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlugin.Objects.Models
{
    public class AutomatedEmailTemplate
    {
        public string TemplateName { get; set; }
        public EmailMessageWcmSignatureRequest Email { get; set; }
    }
}
