using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestWhileIncrement
{
    public class Class1
    {
        // X:\jsc.svn\examples\rewrite\Test\TestWhileInlineIncrement\TestWhileInlineIncrement\Class1.cs

        static int _ident;
        void WriteIdent()
        {
            int i = _ident;

            while (i-- > 0)
                Console_Write(" ");
        }

        private void Console_Write(string p)
        {
        }
    }
}
