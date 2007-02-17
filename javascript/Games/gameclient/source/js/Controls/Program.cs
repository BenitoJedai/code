using ScriptCoreLib;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

namespace gameclient.source.js.Controls
{
    using shared;

    [Script]
    public class MySession : ClientToServerBase, Message.IClient
    {
        #region IClient Members

        public void CreateExplosionByServer(int x, int y, string text)
        {

        }

        public void DisplayNotification(string text, int color)
        {

        }

        public void ForceReload()
        {

        }

        #endregion
    }


    [Script]
    public class Program
    {
        public const string Alias = "fx.Program";

        public readonly MySession Session = new MySession();


        public Program(IHTMLElement placeholder)
        {
            Native.Document.body.appendChild("loading...");

            this.Session.ToServer_EnterLobby(
                delegate (string e)
                {
                    Native.Document.body.appendChild("done!");
                    this.Session.ClientName = e;

                    var a = new ArenaControl();

                    a.Control.attachToDocument();

                    // set the map to be somewhere left
                    a.SetLocation(Rectangle.Of(32, 32, 650, 480));

                    // set tha map canvas size to be something big
                    a.SetCanvasSize(new Point(1000, 1000));

                    // put some elements on the canvas
                    a.DrawRectangleToCanvas(Rectangle.Of(48, 48, 128, 64), Color.Green);
                    a.DrawRectangleToCanvas(Rectangle.Of(48, 128, 128, 64), Color.Gray);
                    a.DrawRectangleToCanvas(Rectangle.Of(400, 300, 128, 64), Color.Black);
                    a.DrawRectangleToCanvas(Rectangle.Of(400, 500, 128, 64), 0xff5566);
                    a.DrawRectangleToCanvas(Rectangle.Of(700, 600, 128, 64), 0x3f5466);

                    a.DrawTextToInfo("just some data", new Point(44, 44), Color.Black);
                    a.DrawTextToInfo("just some data", new Point(45, 45), Color.Yellow);

                    a.ApplySelection += r => a.DrawRectangleToCanvas(r, RandomColor);

                }
            );
        }

        static Color RandomColor
        {
            get
            {
                return Native.Math.floor( Native.Math.random() * 0xFFFFFF);
            }
        }
    }
}
