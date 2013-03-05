extern alias foo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace mscorlibSystemExtensionAttributeUser
{
    [foo::System.Runtime.CompilerServices.ExtensionAttribute]
    class Program
    {
        static void Main(string[] args)
        {
            //Additional information: Could not load file or assembly 'mscorlibSystemExtensionAttribute, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.
            // Additional information: Could not load type 'mscorlibSystem.ExtensionAttribute' from assembly 'mscorlibSystemExtensionAttribute, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201303/20130304-net-4-0
            // this does NOT work!
            AppDomain.CurrentDomain.TypeResolve += (sender, e) =>
            {
                Console.WriteLine("TypeResolve " + new { e.Name });

                if (e.Name == "System.Runtime.CompilerServices.ExtensionAttribute")
                    return typeof(global::System.Runtime.CompilerServices.ExtensionAttribute).Assembly;

                return null;
            };
          
            var x = typeof(Program).GetCustomAttributes(true);
            //foo::foo.goo
        }
    }

    [foo::System.Runtime.CompilerServices.ExtensionAttribute]
    public static class foo
    {
        [foo::System.Runtime.CompilerServices.ExtensionAttribute]
        public static void goo(object e)
        {
        }
    }
}
