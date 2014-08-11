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
        public XElement body;

        public int i;


        //public async Task<string> yield()
        public async Task yield()
        {
            // where else have we done css remoting?

            var r = new Random();

            body.Attribute("style").Value = "background-color: " +
                "rgb(" +
                    r.NextByte() + "," +
                    r.NextByte() + "," +
                    r.NextByte() +
                ")";


            // server side delay. progressbar shows up?
            //await Task.Delay(2500);

            System.Threading.Thread.Sleep(500);

            //return "ok";
        }
    }


}
