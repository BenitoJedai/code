using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RewriteToJavaConsoleApplicationWithDelegatesD
{
    class Foo : IFoo
    {
        public string Invoke1(string e)
        {
            return "e: " + e;
        }
        public string Invoke2(string e)
        {
            return "e: " + e;
        }





        public void Invoke3()
        {
        }
    }
}
