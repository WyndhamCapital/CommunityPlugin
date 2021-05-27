using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlugin.Objects.Helpers
{
    public static class UIHelper
    {
        public static string FormatListOfExceptionsIntoErrorMessage(List<Exception> exceptions)
        {
           return string.Join(Environment.NewLine + Environment.NewLine, exceptions.Select(x => x.Message).ToList());
        }
    }
}
