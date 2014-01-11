using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
//using System.Core?

namespace TestIQueryable
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // http://msdn.microsoft.com/en-us/library/system.linq.iqueryable(v=vs.110).aspx
        // Provides functionality to evaluate queries against a specific data source wherein the type of the data is not specified.

        public void WebMethod2(string e, Action<string> y)
        {
            y(e);
        }

    }
}
