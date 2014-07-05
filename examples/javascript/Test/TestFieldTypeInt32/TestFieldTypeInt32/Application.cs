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
using TestFieldTypeInt32;
using TestFieldTypeInt32.Design;
using TestFieldTypeInt32.HTML.Pages;

namespace TestFieldTypeInt32
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        class xRow(public string x = "x", public int y = 77) { }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var r = new xRow();

            r.GetType().GetFields().WithEach(
                SourceField =>
                {
                    //{ { Name = x, FieldType = [native] String, isString = true, isInt32 = false } }
                    //{ { Name = y, FieldType = < Namespace >.Int32, isString = false, isInt32 = true } }

                    var isString = SourceField.FieldType == typeof(string);
                    var isInt32 = SourceField.FieldType == typeof(int);

                    //var type$InS4tkYSAj62HI0REItY4Q = InS4tkYSAj62HI0REItY4Q.prototype;
                    //type$InS4tkYSAj62HI0REItY4Q.constructor = InS4tkYSAj62HI0REItY4Q;
                    //type$InS4tkYSAj62HI0REItY4Q.x = null;
                    //type$InS4tkYSAj62HI0REItY4Q.y = 0;

                    new IHTMLPre { new { SourceField.Name, SourceField.FieldType, isString, isInt32 } }.AttachToDocument();
                }
            );


        }

    }
}
