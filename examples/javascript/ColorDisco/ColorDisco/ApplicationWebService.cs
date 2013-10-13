using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ColorDisco
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {

        public string backgroundColor = "white";


        public async Task yield()
        {
            var r = new Random();

            backgroundColor =
                "rgb(" +
                    r.NextByte() + "," +
                    r.NextByte() + "," +
                    r.NextByte() +
                ")";

        }
    }


}
