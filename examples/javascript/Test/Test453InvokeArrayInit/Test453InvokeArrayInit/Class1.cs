using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace Test453InvokeArrayInit
{
    public class Class1
    {
        // X:\jsc.svn\examples\javascript\Test\Test453ArrayInit\Test453ArrayInit\Class1.cs
        // X:\jsc.svn\examples\javascript\Test\Test454AnonymousToStringFake\Test454AnonymousToStringFake\Class1.cs

        //var bytes = new[] { 1, 2, 3 };

        public string ToString()
        {
            // {{ needs to go {
            // format is another lagnuage like idl.

            // b = AgAABie7pjmTXjCN7XJOug('{{ foo = {0}, bar = {1} }}', [1, 2, 3]);
            return __String_Format("{{ foo = {0}, bar = {1} }}", new[] { 1, 2, 3 });
        }

        public static string __String_Format(string f, int[] a) => default(string);
    }
}
