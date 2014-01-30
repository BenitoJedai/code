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
using testCharValidation;
using testCharValidation.Design;
using testCharValidation.HTML.Pages;

namespace testCharValidation
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            new IHTMLPre { innerText = "Char C isNumber= " + char.IsNumber('C').ToString() }.AttachToDocument();
            new IHTMLPre { innerText = "Char 2 isNumber= " + char.IsNumber('2').ToString() }.AttachToDocument();
            new IHTMLPre { innerText = "Char C isUpper= " + char.IsUpper('C').ToString() }.AttachToDocument();
            new IHTMLPre { innerText = "Char c isUpper= " + char.IsUpper('c').ToString() }.AttachToDocument();
            new IHTMLPre { innerText = "Char C isLower= " + char.IsLower('C').ToString() }.AttachToDocument();
            new IHTMLPre { innerText = "Char c isLower= " + char.IsLower('c').ToString() }.AttachToDocument();
            new IHTMLPre { innerText = "String ccc isLower= " + "ccc".Any(c=> char.IsLower(c)).ToString() }.AttachToDocument();
            new IHTMLPre { innerText = "String 222 isNumber= " + "222".Any(c => char.IsNumber(c)).ToString() }.AttachToDocument();
            new IHTMLPre { innerText = "String ccc isupper= " + "ccc".Any(c => char.IsUpper(c)).ToString() }.AttachToDocument();
            new IHTMLPre { innerText = "String CCC isupper= " + "CCC".Any(c => char.IsUpper(c)).ToString() }.AttachToDocument();
            new IHTMLPre { innerText = "String CCC isLower= " + "CCC".Any(c => char.IsLower(c)).ToString() }.AttachToDocument();
            new IHTMLPre { innerText = "String CCC isNumber= " + "CCC".Any(c => char.IsNumber(c)).ToString() }.AttachToDocument();

        }

    }
}
