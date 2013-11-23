using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TTFCurrencyExperment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // if we send a type to the client
            // if the client was avare of the type
            // we send the reference id
            // otherwise we have to send the members of this unknown type tooo

            var table1type = typeof(Design.Treasury.Sheet1);

            // X:\jsc.internal.svn\compiler\jsc.internal\jsc.internal\meta\Commands\Reference\ReferenceAssetsLibrary.cs
            var table1 = new Design.Treasury.Sheet1();

            // we can not send the table to the user space
            // we could send a special secured perspective of it?

            // Send it back to the caller.
            y(e);
        }

    }
}
