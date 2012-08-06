using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]
namespace TestGenericNullCheck
{
    public static class GenericExtensions
    {

        [System.Diagnostics.DebuggerStepThrough]
        public static T With<T>(this T e, object x) where T : class
        {
            if (e != null)
                return e;

            var u = (T)x;

            return u;
        }
    }
}
