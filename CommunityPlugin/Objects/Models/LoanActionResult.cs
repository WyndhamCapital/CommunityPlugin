using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlugin.Objects.Models
{
    public class LoanActionResult
    {
        public string LoanNumber { get; set; }
        public bool WasSuccessful { get; set; }
        public string Result { get; set; }
        public string ErrorMessage { get; set; }
    }
}
