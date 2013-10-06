using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebServiceWithStringFields
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public string Text = "hello";

        // blog.stephencleary.com/2013/01/async-oop-3-properties.html

        // can the client side call
        // server defined actions already?
        // they should be Task<> based. then client should also be able
        // to define task based ones.. when can we implement that?
        // what if we want to send back some additional client side code instead?
        // as a result?
        // we can start by static methods
        // what about interfaces?
        // the client may or may not be HTML based!
        // what about events and properties?
        // what about arrays and other primitive datatypes?
        // how does the JVMCLR do it? can we plug into it?
        public Action MakeYellow = delegate { };

        public Task<string> this[string index]
        {
            get
            {
                // { Text = look, a field!, z = { index = foo, Text = hello (modified) } }


                // fields are one way until we also support byref
                //this.Text += " (modified)";

                MakeYellow();

                return Task.FromResult(new { index, Text }.ToString());
            }
        }

    }
}
