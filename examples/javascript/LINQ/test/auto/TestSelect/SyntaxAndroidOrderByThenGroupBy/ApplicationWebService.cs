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

namespace SyntaxAndroidOrderByThenGroupBy
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public async Task<string> WebMethod2()
        {
            // just a syntax check.

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
            //yield(new { message = "ok" }.ToString());
            return new { message = "ok" }.ToString();
        }

    }
}
