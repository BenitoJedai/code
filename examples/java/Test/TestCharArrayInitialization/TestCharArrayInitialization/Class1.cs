using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]

namespace TestCharArrayInitialization
{
    public class Class1
    {


        byte[] __byte = { 5 };
        short[] __short = { 5 };
        int[] __int = { 5 };
        long[] __long = { 5 };


        char[] AMPERSAND_CHAR_ARRAY_1 = { '&' };
        char[] AMPERSAND_CHAR_ARRAY_2 = { '&' };
        char[] AMPERSAND_CHAR_ARRAY_3 = { '&' };

        static void foo()
        {
            char[] AMPERSAND_CHAR_ARRAY_1 = { '&' };
            char[] AMPERSAND_CHAR_ARRAY_2 = { '&' };
            char[] AMPERSAND_CHAR_ARRAY_3 = { '&' };
        }

        //static int foo1(int u = 0, int x = 0, int r = 0)
        //{
        //    return foo1(foo1() + new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }.Length, foo1() + (u == 7 ? 3 : r == 3 ? 1 : 2), foo1() + 3);
        //}
    }

    //public class Class1_int
    //{
    //    int[] __int_1 = { 5 };
    //    int[] __int_2 = { 5 };
    //}
}
