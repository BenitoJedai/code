using TestAvalonWorkerConsole;
using TestAvalonWorkerConsole.Design;
using TestAvalonWorkerConsole.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace TestAvalonWorkerConsole
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public readonly ApplicationSprite sprite = new ApplicationSprite();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            sprite.AutoSizeSpriteTo(page.ContentSize);
            sprite.AttachSpriteTo(page.Content);
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            this.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
