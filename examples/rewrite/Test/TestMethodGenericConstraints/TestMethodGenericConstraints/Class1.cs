using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestMethodGenericConstraints
{
    public static class Class1
    {
        public static void SpawnTo<T>(this Type alias, Type[] KnownTypes, Action<T, object> h) where T : class, new()
        {

        }
    }
}
