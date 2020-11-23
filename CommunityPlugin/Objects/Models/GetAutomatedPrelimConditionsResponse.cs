using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlugin.Objects.Models
{
    public class GetAutomatedPrelimConditionsResponse
    {
        public List<AutomedPrelimCondition> Conditions { get; set; }
        public List<string> Errors { get; set; }
    }
}
