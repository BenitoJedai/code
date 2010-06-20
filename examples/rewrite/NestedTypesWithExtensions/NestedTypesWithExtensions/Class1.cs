using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NestedTypesWithExtensions
{
    public class Class1
    {
        public class Class2
        {
            Class1 Value;

            public void Invoke()
            {
                Value.Invoke();
                this.Invoke2();
            }
        }
    }

    public static class Extension
    {
        public static void Invoke<T>(this T e) where T : Class1
        {

        }

        public static void Invoke2<T>(this T e) where T : Class1.Class2
        {

        }
    }
}
