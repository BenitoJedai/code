using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
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
                delegate(string e)
                {
                    Native.Document.body.appendChild("done!");
                    this.Session.ClientName = e;

                    var a = new ArenaControl();

                    a.Control.attachToDocument();

                    // set the map to be somewhere left
                    a.SetLocation(Rectangle.Of(32, 32, 650, 480));

                    // set tha map canvas size to be something big
                    a.SetCanvasSize(new Point(10000, 4000));

                    // put some elements on the canvas
                    a.DrawRectangleToCanvas(Rectangle.Of(48, 48, 128, 64), Color.Green);
                    a.DrawRectangleToCanvas(Rectangle.Of(48, 128, 128, 64), Color.Gray);
                    a.DrawRectangleToCanvas(Rectangle.Of(400, 300, 128, 64), Color.Black);
                    a.DrawRectangleToCanvas(Rectangle.Of(400, 500, 128, 64), 0xff5566);
                    a.DrawRectangleToCanvas(Rectangle.Of(700, 600, 128, 64), 0x3f5466);

                    a.DrawTextToInfo("just some data", new Point(44, 44), Color.Black);
                    a.DrawTextToInfo("just some data", new Point(45, 45), Color.Yellow);



                    var m = new ArenaMinimapControl();

                    m.ZoomValue = 0.02;

                    m.Control.attachToDocument();

                    m.SetLocation(Rectangle.Of(690, 50, 200, 200));
                    m.SetCanvasSize(a.CurrentCanvasSize * m.ZoomValue);

                    m.DrawRectangleToCanvas(Rectangle.Of(4, 6, 23, 5), RandomColor);
                    m.DrawRectangleToCanvas(Rectangle.Of(60, 8, 23, 5), RandomColor);
                    m.DrawRectangleToCanvas(Rectangle.Of(120, 12, 23, 5), RandomColor);
                    m.DrawRectangleToCanvas(Rectangle.Of(300, 12, 23, 5), RandomColor);

                    a.ApplySelection += delegate(Rectangle r)
                    {
                        var c = RandomColor;
                        a.DrawRectangleToCanvas(r, c);
                        m.DrawRectangleToCanvas(r * m.ZoomValue, c);
                    };

                    a.CanvasViewChanged += delegate(Rectangle p)
                    {
                        m.SetSelectionLocation(p * m.ZoomValue);
                        m.MakeSelectionVisible();
                    };

                    a.SetCanvasPosition(Point.Zero);

                    m.SelectionCenterChanged += delegate(Point p)
                    {
                        a.SetCanvasViewCenter(p / m.ZoomValue);
                    };


                    m.ZoomChanged += delegate
                    {
                        if (a.CurrentCanvasSize.X > a.CurrentCanvasSize.Y)
                        {
                            var w = m.CurrentLocation.Width / a.CurrentCanvasSize.X;

                            if (m.ZoomValue < w)
                                m.ZoomValue = w;
                        }
                        else
                        {
                            var h = m.CurrentLocation.Height / a.CurrentCanvasSize.Y;

                            if (m.ZoomValue < h)
                                m.ZoomValue = h;
                        }

                        m.Layers.Canvas.removeChildren();

                        m.SetCanvasSize(a.CurrentCanvasSize * m.ZoomValue);
                        m.SetSelectionLocation(a.CanvasView * m.ZoomValue);
                        m.MakeSelectionVisible();
                    };
                }
            );
        }

        static Color RandomColor
        {
            get
            {
                return Native.Math.floor(Native.Math.random() * 0xFFFFFF);
            }
        }
    }
}
