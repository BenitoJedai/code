using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestTryCatch
{
    internal class __Activator
    {
        public static object CreateInstance(object type)
        {
            object t = type;
            var o = default(object);

            try
            {
                o = new object();
            }
            catch //(csharp.ThrowableException e)
            {
                throw; // new csharp.RuntimeException(e.ToString());
            }

            return o;
        }
    }
}
