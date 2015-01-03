using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test453If
{
    class CallSite
    {
        public Action<CallSite, object, string> Target;
    }

    class Class1 : ScriptCoreLib.Shared.IAssemblyReferenceToken
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150102
        // X:\jsc.svn\examples\javascript\Test\Test435Using\Test435Using\Class1.cs
        // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs


        static class Invoke_SiteContainer
        {
            public static CallSite Site;
        }

        public static void Invoke(string item)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150103/dynamic

            var foo = new object();

            if (Invoke_SiteContainer.Site == null)
            {


                Invoke_SiteContainer.Site = new CallSite();
            }

            Invoke_SiteContainer.Site.Target(
                Invoke_SiteContainer.Site,
                foo,
                "hello"
            );
        }

    }
}
