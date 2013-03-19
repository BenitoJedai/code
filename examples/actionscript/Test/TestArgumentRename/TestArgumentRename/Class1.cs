using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestArgumentRename
{
    public class Class2
    {
    }

    public class Class1
    {
        // ActionScript will not like this
        public Class2 Class2 { get; set; }

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201303/20130319-dictionary
        public Class2 Invoke(Class2 Class2)
        {
            this.Class2 = Class2;

            Class2 = this.Class2;

            return Class2;
        }

    }
}
