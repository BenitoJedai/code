using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]
namespace TestPrimaryConstructors
{
    // X:\jsc.svn\examples\javascript\RoslynEndUserPreviewExperiment\RoslynEndUserPreviewExperiment\Application.cs
    [Obsolete("how does this relate to the work jsc does for ActionScript?")]
    public class CustomerPrimaryConstructors(string first, string last = "goo")
    {
        public string First { get; } = first;
        public string Last { get; } = last;


        static void Invoke()
        {
            var x = new CustomerPrimaryConstructors("foo");

        }
    }
}
