using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ScriptCoreLib.Desktop
{
    public static class MethodBaseExtensions
    {


        public static string ToSignatureString(this MethodBase m)
        {
            var w = new StringBuilder();

            w.Append(m.Name);
            w.Append("(");

            foreach (var item in m.GetParameters())
            {
                if (item.Position > 0)
                    w.Append(", ");

                w.Append(item.ParameterType.Name);
            }

            w.Append(")");

            return w.ToString();
        }
    }
}
