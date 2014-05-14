using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]
namespace TestRoslynIfNull
{
    public class Class1
    {
        public static object Invoke(object e)
        {
            //if (e != null)
            //    return null;

            if (e == null)
                return null;

            return null;
        }
    }
}
