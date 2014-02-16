using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestNestedTypeImport
{
    public class Program
    {
        public class XMain
        {
            public class XMoveNext
            {
                public XMain ref0;
                public Program ref1;

            }
        }
    }
}
