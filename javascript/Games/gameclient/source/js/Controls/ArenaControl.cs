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
    /// here be tanks
    /// </summary>
    [Script]
    public class ArenaControl
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

        public ArenaControl()
        {
            Control.style.backgroundColor = Color.Red;
            Control.style.position = IStyle.PositionEnum.absolute;
            Control.style.overflow = IStyle.OverflowEnum.hidden;

            this.Layers.CanvasInfo.style.overflow = IStyle.OverflowEnum.hidden;

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

            InitializeCanvasDrag();
        }

        
        void InitializeCanvasDrag()
        {
            var drag_enabled = false;
            var drag_start = Point.Zero;

            var u = this.Layers.User;

            //u.DisableContextMenu();

            //DisableContextMenu(u);

            var selection = new IHTMLDiv();

            selection.style.border = "1px solid #ffffff";
            selection.style.overflow = IStyle.OverflowEnum.hidden;

            var selection_enabled = false;
            var selection_start = Point.Zero;
            var selection_end = Point.Zero;
            var selection_rect = new Rectangle();

            EventHandler UpdateSelection =
                delegate
                {
                    var size = selection_end - selection_start;

                    if (size.X < 0)
                    {
                        selection_rect.Left = selection_start.X + size.X;
                        selection_rect.Width = -size.X;
                    }
                    else
                    {
                        selection_rect.Left = selection_start.X;
                        selection_rect.Width = size.X;
                    }

                    if (size.Y < 0)
                    {
                        selection_rect.Top = selection_start.Y + size.Y;
                        selection_rect.Height = -size.Y;
                    }
                    else
                    {
                        selection_rect.Top = selection_start.Y;
                        selection_rect.Height = size.Y;
                    }

                    var _w = selection_rect.Width < 4;
                    var _h = selection_rect.Height < 4;

                    if (_w || _h)
                    {
                        selection.style.display = IStyle.DisplayEnum.none;
                    }
                    else
                    {
                        selection.style.display = IStyle.DisplayEnum.block;
                        selection.style.SetLocation(selection_rect);
                    }
                };

            u.onmousedown +=
                delegate(IEvent e)
                {


                    if (e.MouseButton == IEvent.MouseButtonEnum.Middle)
                    {
                        drag_enabled = true;
                        drag_start = e.OffsetPosition - this.CurrentCanvasPosition;
                    }

                    if (e.MouseButton == IEvent.MouseButtonEnum.Left)
                    {
                        selection_enabled = true;

                        this.Layers.CanvasInfo.appendChild(selection);

                        selection_start = e.OffsetPosition - this.CurrentCanvasPosition;
                        selection_end = selection_start;

                        UpdateSelection();

                    }
                };

            u.onmousemove +=
                delegate(IEvent e)
                {
                    if (drag_enabled)
                    {
                        this.SetCanvasPosition(e.OffsetPosition - drag_start);
                    }

                    if (selection_enabled)
                    {
                        selection_end = e.OffsetPosition - this.CurrentCanvasPosition;

                        UpdateSelection();
                    }
                };

            u.onmouseup +=
                delegate(IEvent e)
                {
                    if (e.MouseButton == IEvent.MouseButtonEnum.Middle)
                    {
                        drag_enabled = false;
                    }

                    if (selection_enabled)
                    {
                        if (e.MouseButton == IEvent.MouseButtonEnum.Left)
                        {
                            selection_enabled = false;

                            if (ApplySelection != null)
                                ApplySelection(selection_rect);
                            

                            this.Layers.CanvasInfo.removeChild(selection);
                        }
                    }
                };

            var s = u.style;

            s.backgroundColor = Color.Black;
            s.Opacity = 0.0;
        }

        public event EventHandler<Rectangle> ApplySelection;

        public void DrawTextToInfo(string text, Point p, Color c)
        {
            var box = new IHTMLSpan(text);

            box.style.SetLocation(p.X, p.Y);
            box.style.color = c;

            this.Layers.Info.appendChild(box);
        }

        public void DrawRectangleToCanvas(Rectangle r, Color c)
        {
            var box = new IHTMLDiv();

            box.style.overflow = IStyle.OverflowEnum.hidden;
            box.style.SetLocation(r.Left, r.Top, r.Width, r.Height);
            box.style.backgroundColor = c;

            this.Layers.Canvas.appendChild(box);
        }

        public Point CurrentCanvasPosition = Point.Zero;

        public void SetCanvasPosition(Point p)
        {
            CurrentCanvasPosition = p.Min(new Point(0, 0)).Max(new Point(this.CurrentLocation.Width - this.CurrentCanvasSize.X, this.CurrentLocation.Height - this.CurrentCanvasSize.Y));

            Layers.Canvas.style.SetLocation(CurrentCanvasPosition.X, CurrentCanvasPosition.Y);
            Layers.CanvasInfo.style.SetLocation(CurrentCanvasPosition.X, CurrentCanvasPosition.Y);

        }

        public Point CurrentCanvasSize = Point.Zero;

        public void SetCanvasSize(Point p)
        {
            CurrentCanvasSize = p;

            Layers.Canvas.style.SetSize(p.X, p.Y);
            Layers.CanvasInfo.style.SetSize(p.X, p.Y);
        }

        public Rectangle CurrentLocation = new Rectangle();

        public void SetLocation(Rectangle r)
        {
            CurrentLocation = r;

            Control.style.SetLocation(r);

            Layers.Info.style.SetLocation(0, 0, r.Width, r.Height);
            Layers.User.style.SetLocation(0, 0, r.Width, r.Height);
        }
    }

}
