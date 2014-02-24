using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestGenericArrayReturnToLocalAndField
{
    public class Class1
    {
        public Class1[] x;

        static void Invoke(Class1 c, ref Class1 r)
        {
            var a = InvokeCallGenericArray<Class1>();

            c.x = InvokeCallGenericArray<Class1>();


            r.x = InvokeCallGenericArray<Class1>();

            //class1Array0 = __6000001_0001__generic_array_creation(Class1.<Class1>InvokeCallGenericArray());
            //        c.x = Class1.<Class1>InvokeCallGenericArray();
            //        ref_arg2[0].x = Class1.<Class1>InvokeCallGenericArray();

        }

        static T[] InvokeCallGenericArray<T>()
        {
            return new T[0];
        }
    }
}
