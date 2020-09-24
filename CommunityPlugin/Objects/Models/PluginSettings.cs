using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CommunityPlugin.Objects.Models
{
    public class PluginSettings
    {
        public string PluginName { get; set; }

        public Permission Permissions { get; set; }
        public Dictionary<string, JObject> Settings { get; set; }

        public PluginSettings()
        {
            Permissions = new Permission();
            Settings = new Dictionary<string, JObject>();
        }

        public class Permission
        {
            public bool TestEnvironmentRun { get; set; }
            public bool SuperAdminRun { get; set; }
            public bool Everyone { get; set; }
            public List<string> Personas { get; set; }
            public List<string> UserIDs { get; set; }
        }
    }
}