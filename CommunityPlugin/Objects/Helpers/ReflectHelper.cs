using System;
using System.Reflection;

namespace CommunityPlugin.Objects.Helpers
{
    public static class ReflectHelper
    {
        public static object GetField(string Name, object obj)
        {
            FieldInfo field = obj.GetType().GetField(Name, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return field.GetValue(obj);
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static T ConvertToTyped<T>(Object rule)
        {
            return (T)Convert.ChangeType(rule, typeof(T));
        }

    }
}
