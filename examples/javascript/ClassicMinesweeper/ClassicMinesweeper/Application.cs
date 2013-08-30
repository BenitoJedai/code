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
using ClassicMinesweeper.Design;
using ClassicMinesweeper.HTML.Pages;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System.Drawing;

namespace ClassicMinesweeper
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            new MineSweeper.js.MineSweeperGame();

            new IHTMLElement(IHTMLElement.HTMLElementEnum.hr).AttachToDocument();

            new MineSweeper.js.RedNumberDisplay().Control.AttachToDocument();

            new IHTMLElement(IHTMLElement.HTMLElementEnum.hr).AttachToDocument();

            new MineSweeper.js.MineSweeperPanel().Control.AttachToDocument();

            new IHTMLElement(IHTMLElement.HTMLElementEnum.hr).AttachToDocument();

            new MineSweeper.js.MineSweeperControl().Control.AttachToDocument();

            FormStyler.AtFormCreated = FormStyler.LikeWindows3;

            var f = new Form { Text = "Minesweeper" };
            var p = new Panel { Dock = DockStyle.Fill }.AttachTo(f);

            var c = p.GetHTMLTargetContainer();
            var g = new MineSweeper.js.MineSweeperGame(_Owner: c);

            var cs = new Size(g.Panel.ControlWidth + 8, g.Panel.ControlHeight + 8);

            //f.Text += cs;


            f.ClientSize = cs;

            f.Show();

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
