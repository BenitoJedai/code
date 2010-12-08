using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.lang;

namespace ScriptCoreLibJava.Extensions
{
    [Script]
    public static class BCLImplementationExtensions
    {
        public static Class ToClass(this System.Type t)
        {
            var tt = (ScriptCoreLibJava.BCLImplementation.System.__Type)t;

            return tt.InternalTypeDescription;
        }
    }
}
