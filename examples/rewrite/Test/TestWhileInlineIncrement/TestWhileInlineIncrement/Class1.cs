using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWhileInlineIncrement
{
    public class Class1
    {
        static int _ident;
        void WriteIdent()
        {
            int i = _ident;

            while (i-- > 0)
                Console.Write(" ");
        }

        void WriteIdent2()
        {
            int i = _ident;

            while (i++ > 0)
                Console.Write(" ");
        }
    }
}
