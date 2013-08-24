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
using Abstractatech.JavaScript.NatureBoy.Design;
using Abstractatech.JavaScript.NatureBoy.HTML.Pages;
using ScriptCoreLib.JavaScript.Controls.LayeredControl;
using ScriptCoreLib.Shared.Drawing;

namespace Abstractatech.JavaScript.NatureBoy
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
        public Application(IApp page)
        {
            #region arena
            var map = new Point(2000, 2000);

            var arena = new ArenaControl();

            arena.Layers.Canvas.style.backgroundColor =
                Color.FromGray(0xc0);
            arena.SetLocation(
                Rectangle.Of(0, 0, Native.window.Width, Native.window.Height));

            arena.SetCanvasSize(map);

            arena.Control.AttachToDocument();


            arena.DrawTextToInfo("hello world", new Point(8, 8), Color.Blue);

            Native.window.onresize +=
                delegate
                {
                    arena.SetLocation(
                        Rectangle.Of(0, 0, Native.window.Width, Native.window.Height));

                    arena.SetCanvasPosition(
                        arena.CurrentCanvasPosition
                        );
                };
            #endregion
        }

    }
}
