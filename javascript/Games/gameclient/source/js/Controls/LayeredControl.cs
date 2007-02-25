using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

namespace gameclient.source.js.Controls
{
    using shared;

    /// <summary>
    /// provides a draggable control
    /// </summary>
    [Script]
    public abstract class LayeredControl
    {
        public readonly IHTMLDiv Control = new IHTMLDiv();

        [Script]
        public class LayersGroup
        {
            public readonly IHTMLDiv Canvas = new IHTMLDiv();
            public readonly IHTMLDiv CanvasInfo = new IHTMLDiv();

            public readonly IHTMLDiv Info = new IHTMLDiv();
            public readonly IHTMLDiv User = new IHTMLDiv();
        }

        public readonly LayersGroup Layers = new LayersGroup();

        public LayeredControl()
        {
            Control.style.backgroundColor = Color.Black;

            Control.style.position = IStyle.PositionEnum.absolute;
            Control.style.overflow = IStyle.OverflowEnum.hidden;

            this.Layers.Canvas.style.overflow = IStyle.OverflowEnum.hidden;
            //this.Layers.CanvasInfo.style.overflow = IStyle.OverflowEnum.hidden;

            Layers.Canvas.style.position = IStyle.PositionEnum.absolute;
            Layers.CanvasInfo.style.position = IStyle.PositionEnum.absolute;
            Layers.Info.style.position = IStyle.PositionEnum.absolute;
            Layers.User.style.position = IStyle.PositionEnum.absolute;

            Control.appendChild(
                Layers.Canvas,
                Layers.CanvasInfo,
                Layers.Info,
                Layers.User
            );

            Layers.Canvas.style.backgroundColor = Color.Blue;


            Layers.User.style.backgroundColor = Color.Black;
            Layers.User.style.Opacity = 0.0;
        }

        public Point CurrentCanvasPosition = Point.Zero;

        public event EventHandler<Rectangle> CanvasViewChanged;

        protected void InternalSetCanvasPosition(Point p)
        {
            CurrentCanvasPosition = p.Min(new Point(0, 0)).Max(new Point(this.CurrentLocation.Width - this.CurrentCanvasSize.X, this.CurrentLocation.Height - this.CurrentCanvasSize.Y));

            if (this.CurrentLocation.Height > CurrentCanvasSize.Y)
            {
                this.CurrentCanvasPosition.Y = (this.CurrentLocation.Height - this.CurrentCanvasSize.Y) / 2;
            }

            if (this.CurrentLocation.Width > CurrentCanvasSize.X)
            {
                this.CurrentCanvasPosition.X = (this.CurrentLocation.Width - this.CurrentCanvasSize.X) / 2;
            }


            Layers.Canvas.style.SetLocation(CurrentCanvasPosition.X, CurrentCanvasPosition.Y);
            Layers.CanvasInfo.style.SetLocation(CurrentCanvasPosition.X, CurrentCanvasPosition.Y);
        }

        public void SetCanvasPosition(Point p)
        {
            InternalSetCanvasPosition(p);

            RaiseCanvasViewChanged();
        }

        public void SetCanvasViewCenter(Point p)
        {
            this.InternalSetCanvasPosition(new Point(this.CurrentLocation.Width / 2 - p.X, this.CurrentLocation.Height / 2 - p.Y));
        }

        void RaiseCanvasViewChanged()
        {
            if (CanvasViewChanged == null)
                return;

            CanvasViewChanged(CanvasView);
        }

        public Rectangle CanvasView
        {
            get
            {
                var c = new Rectangle();

                c.Left = -this.CurrentCanvasPosition.X;
                c.Top = -this.CurrentCanvasPosition.Y;
                c.Width = this.CurrentLocation.Width;
                c.Height = this.CurrentLocation.Height;

                return c;
            }
        }

        public Point CurrentCanvasSize = Point.Zero;

        public void SetCanvasSize(Point p)
        {
            CurrentCanvasSize = new Point(Native.Math.round( p.X), Native.Math.round(p.Y));

            Layers.Canvas.style.SetSize(p.X, p.Y);
            Layers.CanvasInfo.style.SetSize(p.X, p.Y);

            SetCanvasPosition(CurrentCanvasPosition);
        }

        public Rectangle CurrentLocation = new Rectangle();

        public void SetLocation(Rectangle r)
        {
            CurrentLocation = r;

            Control.style.SetLocation(r);

            Layers.Info.style.SetLocation(0, 0, r.Width, r.Height);
            Layers.User.style.SetLocation(0, 0, r.Width, r.Height);
        }

        public void DrawRectangleToCanvas(Rectangle r, Color c)
        {
            var box = new IHTMLDiv();

            box.style.overflow = IStyle.OverflowEnum.hidden;
            box.style.SetLocation(r.Left, r.Top, r.Width, r.Height);
            box.style.backgroundColor = c;

            this.Layers.Canvas.appendChild(box);
        }

        protected void InitializeCanvasDrag()
        {
            var drag_enabled = false;
            var drag_start = Point.Zero;

            var u = this.Layers.User;

            u.onmousedown +=
                delegate(IEvent e)
                {


                    if (e.MouseButton == IEvent.MouseButtonEnum.Middle)
                    {
                        drag_enabled = true;
                        drag_start = e.OffsetPosition - this.CurrentCanvasPosition;
                    }


                };

            u.onmousemove +=
                delegate(IEvent e)
                {
                    if (drag_enabled)
                    {
                        this.SetCanvasPosition(e.OffsetPosition - drag_start);
                    }

                };

            u.onmouseup +=
                delegate(IEvent e)
                {
                    if (e.MouseButton == IEvent.MouseButtonEnum.Middle)
                    {
                        drag_enabled = false;
                    }

                };

            u.onmouseout +=
                delegate
                {
                    if (drag_enabled)
                    {
                        drag_enabled = false;
                    }
                };
        }

        
    }

}
