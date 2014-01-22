using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCoalescingOperator
{

    // http://msdn.microsoft.com/en-us/library/ms173224.aspx


    class NullCoalesce
    {
        private static int? OperandSize;

        private object Operand64;
        //static int? GetNullableInt()
        //{
        //    return null;
        //}

        //static string GetStringValue()
        //{
        //    return null;
        //}

        static void Main()
        {
            //int? x = null;

            //// Set y to the value of x if x is NOT null; otherwise, 
            //// if x = null, set y to -1. 
            //int y = x ?? -1;

            //// Assign i to return value of the method if the method's result 
            //// is NOT null; otherwise, if the result is null, set i to the 
            //// default value of int. 
            //int i = GetNullableInt() ?? default(int);

            //new NullCoalesce().Operand64 = new NullCoalesce().Pop64(new NullCoalesce().OperandSize ?? 0);
            //new NullCoalesce().Pop64(new NullCoalesce().OperandSize ?? 0);
            new NullCoalesce().Pop64(OperandSize ?? 0);
            //Pop64(new NullCoalesce().OperandSize ?? 0);

            //string s = GetStringValue();
            //// Display the value of s if s is NOT null; otherwise,  
            //// display the string "Unspecified".
            //Console.WriteLine(s ?? "Unspecified");
        }

        private object Pop64(int p)
        {
            throw new NotImplementedException();
        }
    }
}
