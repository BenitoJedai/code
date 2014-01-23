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

namespace TestDateTime
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public Task<xRow> Goo()
        {
            //<document><TaskComplete><TaskResult>&lt;_02000003&gt;
            //  &lt;_04000001&gt;946764000000&lt;/_04000001&gt;
            //&lt;/_02000003&gt;</TaskResult></TaskComplete></document>

            return new xRow
            {

                Timestamp = new DateTime(2000, 1, 2)
            }.AsResult();
        }

    }

    public class xRow
    {
        public DateTime Timestamp;
    }
}
