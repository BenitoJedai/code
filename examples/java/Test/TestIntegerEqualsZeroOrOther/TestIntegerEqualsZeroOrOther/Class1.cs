using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestIntegerEqualsZeroOrOther
{
    public class Class1
    {
        int __1__state;

        public void Invoke()
        {
            if (this.__1__state == 0 ||
                     this.__1__state == 2 ||
                     this.__1__state == 4)
            {

            }
        }

    }
}
