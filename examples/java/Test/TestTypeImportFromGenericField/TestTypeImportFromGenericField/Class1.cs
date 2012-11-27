using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]
namespace TestTypeImportFromGenericField
{
    public class Class1
    {
        public static Class2<Class3<object, Class1>> f;
    }

    public class Class2<T>
    {
    }


    public class Class3<T, X>
    {
    }
}
