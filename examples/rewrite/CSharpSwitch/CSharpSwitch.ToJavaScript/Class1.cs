using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
[assembly: Obfuscation(Feature = "script")]
namespace CSharpSwitch.ToJavaScript
{
    public class Class1
    {

        public static string Bar(int caseSwitch2)
        {
            switch (caseSwitch2)
            {
                case 1:
                    return ("Case 1");
                case 2:
                    return ("Case 2");
                default:
                    return ("Default case");
            }
        }
    }

    class G
    {
        protected internal static int Foo(object closure1)
        {
            return -1;
        }

        protected internal static void Bar(object closure1)
        {
            int num = 0;
            while (num >= 0)
            {

                if (num == 0x2d)
                    num = Foo(closure1);


                if (num == 0x25)
                    num = Foo(closure1);


                if (num == 0x13)
                    num = Foo(closure1);


                if (num == 0x15)
                    num = Foo(closure1);


                if (num == 0x1d)
                    num = Foo(closure1);


                if (num == 0)
                    num = Foo(closure1);

            }
        }



    }
}
