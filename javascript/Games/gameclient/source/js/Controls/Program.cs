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

        public event EventHandler<Message._IClient_DrawRectangle> OnIClient_DrawRectangle;

        public void IClient_DrawRectangle(RectangleInfo rect, int color)
        {
            if (OnIClient_DrawRectangle == null) return;

            var p = new Message._IClient_DrawRectangle { rect, color };

            OnIClient_DrawRectangle(p);
        }
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
                    a.SetCanvasSize(new Point(8000, 8000));

                    // put some elements on the canvas
                    a.DrawRectangleToCanvas(Rectangle.Of(48, 48, 128, 64), Color.Green);
                    a.DrawRectangleToCanvas(Rectangle.Of(48, 128, 128, 64), Color.Gray);
                    a.DrawRectangleToCanvas(Rectangle.Of(400, 300, 128, 64), Color.Black);
                    a.DrawRectangleToCanvas(Rectangle.Of(400, 500, 128, 64), 0xff5566);
                    a.DrawRectangleToCanvas(Rectangle.Of(700, 600, 128, 64), 0x3f5466);

                    a.DrawTextToInfo("just some data", new Point(44, 44), Color.Black);
                    a.DrawTextToInfo("just some data", new Point(45, 45), Color.Yellow);



                    var m = new ArenaMinimapControl();

                    m.Zoom.Validate += delegate
                    {
                        if (a.CurrentCanvasSize.X > a.CurrentCanvasSize.Y)
                        {
                            var w = m.CurrentLocation.Width / a.CurrentCanvasSize.X;

                            if (m.Zoom.Value < w)
                                m.Zoom.Value = w;


                        }
                        else
                        {
                            var h = m.CurrentLocation.Height / a.CurrentCanvasSize.Y;

                            if (m.Zoom.Value < h)
                                m.Zoom.Value = h;
                        }
                    };

                    m.Zoom.Changed += delegate
                    {
                        m.Layers.Canvas.removeChildren();

                        m.SetCanvasSize(a.CurrentCanvasSize * m.Zoom.Value);
                        m.SetSelectionLocation(a.CanvasView * m.Zoom.Value);
                        m.MakeSelectionVisible();
                    };

                    m.Control.attachToDocument();

                    m.SetLocation(Rectangle.Of(690, 50, 200, 200));

                    EventHandler<Rectangle, Color> DrawRectangleLocal = delegate(Rectangle r, Color c)
                    {
                        a.DrawRectangleToCanvas(r, c);
                        m.DrawRectangleToCanvas(r * m.Zoom.Value, c);
                    };

                    EventHandler<Rectangle, Color> DrawRectangle = delegate(Rectangle r, Color c)
                    {
                        DrawRectangleLocal(r, c);

                        this.Session.IServer_DrawRectangle(r, c);
                    };

                    this.Session.OnIClient_DrawRectangle += delegate(Message._IClient_DrawRectangle p)
                    {
                        var r = new Rectangle {
                            p.rect.Left,
                            p.rect.Top,
                            p.rect.Width,
                            p.rect.Height,
                        };

                        DrawRectangleLocal(r, p.color);
                    };


                    a.SelectionClick += delegate(Point p)
                    {
                        DrawRectangle(p.WithMargin(a.SelectionMinimumSize / 2), RandomColor);
                    };

                    a.ApplySelection += delegate(Rectangle r)
                    {
                        DrawRectangle(r, RandomColor);
                    };

                    a.CanvasViewChanged += delegate(Rectangle p)
                    {
                        m.SetSelectionLocation(p * m.Zoom.Value);
                        m.MakeSelectionVisible();
                    };

                    a.SetCanvasPosition(Point.Zero);

                    m.SelectionCenterChanged += delegate(Point p)
                    {
                        a.SetCanvasViewCenter(p / m.Zoom.Value);
                    };

                    m.Zoom.SetValue(0);

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
