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
using com.abstractatech.gamification.sweeper.Design;
using com.abstractatech.gamification.sweeper.HTML.Pages;
using ScriptCoreLib.Shared.Drawing;

namespace com.abstractatech.gamification.sweeper
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
            "Minesweeper".ToDocumentTitle();

            Action Dispose = delegate { };

            var oldX = 0;
            var oldY = 0;
            Action Create = delegate
            {
                var ButtonsX = ((int)Math.Floor(Native.window.Width / (MineSweeper.js.MineSweeperControl.ButtonSize * 2.0))) - 2;
                var ButtonsY = ((int)Math.Floor(Native.window.Height / (MineSweeper.js.MineSweeperControl.ButtonSize * 2.0))) - 3;

                if (ButtonsX == oldX)
                    if (ButtonsY == oldY)
                        return;

                oldX = ButtonsX;
                oldY = ButtonsY;
                Dispose();

                var m = new MineSweeper.js.MineSweeperPanel(
                    ButtonsX: ButtonsX,
                    ButtonsY: ButtonsY
                );
                m.Control.style.border = "";
                m.Control.style.transform = "scale(2.0)";
                m.Control.style.transformOrigin = "0% 0%";
                m.Control.AttachToDocument();

                Dispose = delegate
                {
                    m.Control.Orphanize();

                    Dispose = delegate { };
                };
            };

            Native.Document.body.style.backgroundColor = Color.FromGray(192);

            Create();

            Native.window.onresize +=
                delegate
                {
                    Create();
                };

            Native.Document.body.onselectstart +=
              e =>
              {
                  e.preventDefault();
                  e.stopPropagation();

              };

            Native.Document.oncontextmenu +=
              e =>
              {
                  e.preventDefault();
                  e.stopPropagation();



              };

        }

    }
}
