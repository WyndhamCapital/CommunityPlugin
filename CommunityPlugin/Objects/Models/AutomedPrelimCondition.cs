using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlugin.Objects.Models
{
    public class AutomedPrelimCondition
    {
        public string BusinessRuleName { get; set; }
        public string UwConditionName { get; set; }
        public string UwTemplateId { get; set; }

        public string BusinesRuleLastModifiedBy { get; set; }
    }
}
