using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<AssemblyCopyrightAttribute> GetAssemblyCopyrightAttributes(this Assembly a)
        { 
            return a.GetCustomAttributes(
                                    attributeType: typeof(AssemblyCopyrightAttribute),
                                    inherit: false
                                ).Cast<AssemblyCopyrightAttribute>();
        }
    }
}
