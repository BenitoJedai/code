using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using JavaDosBoxQuakeBeta.HTML.Pages;

namespace JavaDosBoxQuakeBeta
{
    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            // view-source:http://vpsland.superglobalmegacorp.com/jdosbox/qb8img.html
            // http://www.dedoimedo.com/games/reviving/dosbox_multiplayer.html

            //            [jdosbox]00dd Create jdos.hardware.IPX$ReceiverThread
            //System.TypeLoadException: Declaration referenced in a method implementation cannot be a final method.  Type: 'ReceiverThread'.  Assembly: 'jdosbox, Version=0.0.0.0, Culture=neutral, PublicKeyToken=nul

            // CreateType:  jdos.gui.MyApplet1
            // error: System.InvalidOperationException: Unable to change after type has been created.

            // Initialize MyApplet1
            new jdos.gui.MyApplet1().AttachAppletTo(page.Content);
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);
        }

    }
}
