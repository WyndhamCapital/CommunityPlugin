using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlugin.Objects.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class UIMapAttribute : Attribute
    {
        public UIMapAttribute()
        {
            Editable = true;
        }

        public string DisplayName { get; set; }

        public bool Editable { get; set; }

        public bool Ignore { get; set; }
    }
}
