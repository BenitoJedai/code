using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestWebServiceTaskFields
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // what about properties?
        // events?
        // can we get property changed events?

        public int Foo = 3;


        public Task yield()
        {
            Foo = 7;

            return new object().ToTaskResult();

            // time to serialize fields into cookie
        }

    }
}
