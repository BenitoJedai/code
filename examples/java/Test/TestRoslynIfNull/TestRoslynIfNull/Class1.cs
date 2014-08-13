using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]
namespace TestRoslynIfNull
{
    public enum xFoo { };

    public class Class1
    {
        //d = !(b == null);

        //if (!d)

        public static object Invoke(xFoo a)
        {
            //  if (a == null)
            if (a == default(xFoo))

                // if (!(a == 0))
                //if (a != default(xFoo))
                return null;

            return null;
        }

        //    c = (b > null);

        //if (!c)
        //{

        // x:\jsc.svn\examples\javascript\test\testienumerableforservice\testienumerableforservice\applicationwebservice.cs

        ////public static object Invoke(object a)
        //public static object Invoke(Iobject a)
        //{
        //    var e = a;

        //    //if (e != null)
        //    //    return null;

        //    //    if (!(iobject0 > null))
        //    if (e == null)
        //        return null;

        //    return null;
        //}

        //public interface Iobject
        //{
        //}
    }
}
