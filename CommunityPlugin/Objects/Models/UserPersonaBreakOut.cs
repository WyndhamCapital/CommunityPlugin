using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlugin.Objects.Models
{
    public class UserPersonaBreakOut
    {
        public bool isProcessor;
        public bool isAdmin;
        public bool isLo;
        public bool isCloser;

        public UserPersonaBreakOut()
        {
            isLo = false;
            isAdmin = false;
            isProcessor = false;
            isCloser = false;
        }
    }
}
