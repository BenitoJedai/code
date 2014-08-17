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

namespace TestAndroidOrderByThenGroupBy
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        /// <summary>
        /// The static content defined in the HTML file will be update to the dynamic content once application is running.
        /// </summary>
        //public XElement Header = new XElement(@"h1", @"JSC - The .NET crosscompiler for web platforms. ready.");

        // android cookie garbles, or turnacates data?
        // I/System.Console( 1762): XDocument Parse error: { text = <h1 id="Header">JSC - The .NET crosscompiler for web platforms. ready.</h1∩┐╜∩┐╜ }
        //        I/System.Console( 1762): #4 POST /xml/WebMethod2 HTTP/1.1 error:
        //I/System.Console( 1762): #4 java.lang.RuntimeException: expected: '>' actual: '∩┐┐' (position:END_TAG </h1∩┐╜∩┐╜>@1:77 in java.io.StringReader@40669738)
        //I/System.Console( 1762): #4 java.lang.RuntimeException: expected: '>' actual: '∩┐┐' (position:END_TAG </h1∩┐╜∩┐╜>@1:77 in java.io.StringReader@40669738)

        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Activator.cs





        public void WebMethod2(Action<string> yield)
        {
            // anonymus types GetTypeInfo need RTTI and perhaps analysis too?
            // store it as string/xml/binary/zip ?

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
