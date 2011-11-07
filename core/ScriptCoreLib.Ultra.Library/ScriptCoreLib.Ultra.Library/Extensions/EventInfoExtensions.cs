using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ScriptCoreLib.Extensions
{
    public static class EventInfoExtensions
    {
        public static bool IsStaticEvent(this EventInfo e)
        {
            var _add = e.GetAddMethod(true);
            if (_add != null)
                return _add.IsStatic;

            var _remove = e.GetRemoveMethod(true);
            if (_remove != null)
                return _remove.IsStatic;

            return false;
        }
    }
}
