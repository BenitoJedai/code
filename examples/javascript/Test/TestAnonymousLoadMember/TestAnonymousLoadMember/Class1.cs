using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestAnonymousLoadMember
{
    public class Class1
    {
        public Class1()
        {
            // Error	1	No implementation found for this native method, please implement [System.Text.StringBuilder.Append(System.String)]	x:\jsc.svn\examples\javascript\Test\TestAnonymousLoadMember\TestAnonymousLoadMember\script	TestAnonymousLoadMember


            var a = new { i = 0 };

            //            // <>f__AnonymousType0`1.get_i
            //type$__bGZd10BFdzSpLW7STZP0LA.AwAABkBFdzSpLW7STZP0LA = function ()
            //{
            //  return this.i;
            //};

            var i = a.i;

            var x = new { a.i };

        }
    }
}
