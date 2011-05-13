using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ScriptCoreLib.Extensions
{
    public static class MethodBaseExtensions
    {
        public static string[] GetParameterTypeFullNames(this MethodBase m)
        {
            // Will run under JVM!

            var p = m.GetParameters();
            var y = new string[p.Length];

            for (int i = 0; i < y.Length; i++)
            {
                y[i] = p[i].ParameterType.FullName;
            }

            return y;
        }
    }
}
