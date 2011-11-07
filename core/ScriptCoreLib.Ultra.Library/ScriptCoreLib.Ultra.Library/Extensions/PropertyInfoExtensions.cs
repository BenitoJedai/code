using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ScriptCoreLib.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static bool IsStaticProperty(this PropertyInfo e)
        {
            var _get = e.GetGetMethod(true);
            if (_get != null)
                return _get.IsStatic;

            var _set = e.GetSetMethod(true);
            if (_set != null)
                return _set.IsStatic;

            return false;
        }
    }
}
