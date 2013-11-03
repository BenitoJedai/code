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

namespace ElementsChangedByWebService
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // <body>xxx</body>
        public XElement content;

        public async Task yield()
        {
            content.AddFirst(
                new XElement("p", "hi from server")
            );

            Console.WriteLine(new
            {
                content
            });
        }

    }
}
