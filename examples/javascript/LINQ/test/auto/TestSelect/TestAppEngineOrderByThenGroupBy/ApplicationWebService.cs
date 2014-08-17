using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ScriptCoreLib.Query.Experimental;

namespace TestAppEngineOrderByThenGroupBy
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140817/appengine

        // step 1. run it under 199
        // step 2. add a button do show sql syntax
        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\JVMCLRSyntaxOrderByThenGroupBy\Program.cs

        public XElement Header = new XElement(@"h1", @"JSC - The .NET crosscompiler for web platforms. ready.");

        // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRAsync\TestJVMCLRAsync\Program.cs
        // can we send in the caller IButtonProxy ?
        //public async Task<string> WebMethod2()
        public void WebMethod2(Action<string> yield)
        {
            var f = (
                from x in new xTable()

                orderby x.field1 ascending

                group x by 1 into gg

                select new
                {
                    gg.Last().Tag
                }

            ).FirstOrDefault();

            //return new { message = "ok" }.ToString();
            yield(new { message = "ok" }.ToString());
        }
    }
}
