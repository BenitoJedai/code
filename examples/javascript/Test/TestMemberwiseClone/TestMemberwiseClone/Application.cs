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
using TestMemberwiseClone;
using TestMemberwiseClone.Design;
using TestMemberwiseClone.HTML.Pages;

namespace TestMemberwiseClone
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
            // http://stackoverflow.com/questions/3272363/why-memberwiseclone-defined-in-system-object-is-protected-why-its-not-public
            // http://msdn.microsoft.com/en-us/library/system.object.memberwiseclone.aspx
            // http://stackoverflow.com/questions/3901085/net-memberwiseclone-shallow-copy-not-working

            //        script: error JSC1000: method was found, but too late: [MemberwiseClone]
            //script: error JSC1000: error at TestMemberwiseClone.Application..ctor,
            // jsc dont support it yet

            var z = this.MemberwiseClone();
        }

    }
}
