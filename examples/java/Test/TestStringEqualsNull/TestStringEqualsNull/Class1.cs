using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestStringEqualsNull
{
    public class Class1
    {
        //public static bool foo0(string memo)
        //{
        //    var flag = memo != null;
        //    if (flag)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        public static bool foo1(string memo, bool flag2)
        {
            var flag = memo != null && flag2;
            if (flag)
            {
                return true;
            }

            return false;
        }

        public static bool foo2(string memo, bool flag2)
        {
            var flag = memo == null && flag2;
            if (flag)
            {
                return true;
            }

            return false;
        }


        public static bool foo1i(string memo, bool flag2)
        {
            if (memo != null && flag2)
            {
                return true;
            }

            return false;
        }

        public static bool foo2i(string memo, bool flag2)
        {
            if (memo == null && flag2)
            {
                return true;
            }

            return false;
        }
    }
}
