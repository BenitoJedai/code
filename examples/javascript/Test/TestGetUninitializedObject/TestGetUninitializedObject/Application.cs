using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestGetUninitializedObject;
using TestGetUninitializedObject.Design;
using TestGetUninitializedObject.HTML.Pages;
using System.Runtime.Serialization;

namespace TestGetUninitializedObject
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\Serialization\FormatterServices.cs

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var o = new xFoo();
            Console.WriteLine(new { o });

            //var x = typeof(o);
            var x = o.GetType();

            Console.WriteLine("GetUninitializedObject");

            // sometime later
            var oo = FormatterServices.GetUninitializedObject(x);

            //var isFoo = oo as xFoo;
            var isFoo = oo is xFoo;

            // Uncaught TypeError: Cannot read property 'LgEABvEJMzKFrGX3QxjvzA' of null 

            var om = FormatterServices.GetSerializableMembers(x);
            var ov = FormatterServices.GetObjectData(o, om);

            FormatterServices.PopulateObjectMembers(oo, om, ov);

            // 0:34ms {{ isFoo = true, oo = xFoo: {{ xField1 = field1 {{ Counter = 1 }} }} }}
            // 0:80ms {{ isFoo = true, oo = xFoo: {{ xField1 = null }} }}
            // do we still see ToString?
            Console.WriteLine(new { isFoo, oo });


        }
    }

    public class xFoo
    {
        //0:32ms xFoo ctor {{ Counter = 2 }
        //0:32ms GetUninitializedObject
        //0:33ms xFoo ctor {{ Counter = 4 }}
        //0:33ms {{ oo = {{ xField1 = field1 {{ Counter = 3 }} }} }}

        static int Counter = 1;

        public string xField1 = "field1 " + new { Counter = Counter++ };

        public xFoo()
        {
            Console.WriteLine("xFoo ctor " + new { Counter = Counter++ });
        }

        public override string ToString()
        {
            return "xFoo: " + new { xField1 }.ToString();
        }
    }
}
