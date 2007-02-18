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

            Console.WriteLine("canvas: " + p);
            Console.WriteLine("CurrentLocation: " + CurrentLocation);
            Console.WriteLine("CurrentCanvasSize: " + CurrentCanvasSize);

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

    [Script]
    public class ArenaMinimapControl : LayeredControl
    {
        [Script]
        public class ZoomValue
        {
            public double Value = 0.1;
            public double Step = 0.005;

            public event EventHandler Validate;
            public event EventHandler Changed;

            public void SetValue(double e)
            {
                var v = Value;

                Value = e;
                
                if (Validate != null)
                    Validate();

                if (v == Value)
                    return;

                if (Changed != null)
                    Changed();
            }
        }

        public readonly ZoomValue Zoom = new ZoomValue();


        IHTMLDiv Selection;

        public ArenaMinimapControl()
        {
            base.InitializeCanvasDrag();

            this.InitializeSelectionDrag();
            this.InitializeWheel();
        }

        void InitializeWheel()
        {
            this.Layers.User.onmousewheel +=
                delegate(IEvent e)
                {
 
                    this.Zoom.SetValue(this.Zoom.Value + e.WheelDirection * this.Zoom.Step);
                };
        }

        void InitializeSelectionDrag()
        {
            Selection = new IHTMLDiv();
            Selection.style.overflow = IStyle.OverflowEnum.hidden;
            Selection.style.SetLocation(0, 0, 1, 1);
            Selection.style.border = "1px solid #ffffff";
            
            this.Layers.CanvasInfo.appendChild(Selection);

            var u = this.Layers.User;

            var selection_enabled = false;

            u.onmousedown +=
                delegate(IEvent e)
                {
                    if (e.MouseButton == IEvent.MouseButtonEnum.Left)
                    {
                        selection_enabled = true;
                    }
                };

            u.onmousemove +=
                delegate(IEvent e)
                {
                    if (selection_enabled)
                    {
                        var selection_end = e.OffsetPosition + this.CanvasView.Location;



                        this.SetSelectionCenter(selection_end);
                        this.MakeSelectionVisible();

                    }
                    
                };

            u.onmouseup +=
                delegate(IEvent e)
                {
                    if (e.MouseButton == IEvent.MouseButtonEnum.Left)
                    {
                        selection_enabled = false;
                    }
                };


            u.onmouseout +=
                delegate
                {
                    if (selection_enabled)
                        selection_enabled = false;
                };

        }

        public Rectangle CurrentSelectionLocation = new Rectangle();

        public event EventHandler<Point> SelectionCenterChanged;

        public void SetSelectionCenter(Point e)
        {
            var r = new Rectangle();
            var s = this.CurrentSelectionLocation;
            var w = s.Width / 2;
            var h = s.Height / 2;
            var p = e.Max(new Point(w, h)).Min(new Point(this.CurrentCanvasSize.X - w, this.CurrentCanvasSize.Y - h));

            r.Left = p.X - w;
            r.Top = p.Y - h;
            r.Width = s.Width;
            r.Height = s.Height;

            SetSelectionLocation(r);

            if (SelectionCenterChanged != null)
                SelectionCenterChanged(p);
        }

        public void SetSelectionLocation(Rectangle e)
        {
            this.CurrentSelectionLocation = e;
            this.Selection.style.SetLocation(e);
        }

        public void MakeSelectionVisible()
        {
            var c = this.CanvasView;
            var s = this.CurrentSelectionLocation;

            if (s.Right > c.Right)
            {
                this.SetCanvasPosition(
                    new Point(this.CurrentLocation.Width - s.Right, this.CurrentCanvasPosition.Y)
                );
            }

            if (this.CurrentSelectionLocation.Bottom > c.Bottom)
            {
                this.SetCanvasPosition(
                    new Point(this.CurrentCanvasPosition.X, this.CurrentLocation.Height - s.Bottom)
                );
            }

            if (this.CurrentSelectionLocation.Left < c.Left)
            {
                this.SetCanvasPosition(
                       new Point(-this.CurrentSelectionLocation.Left, this.CurrentCanvasPosition.Y)
                   );
            }

            if (this.CurrentSelectionLocation.Top < c.Top)
            {
                this.SetCanvasPosition(
                       new Point(this.CurrentCanvasPosition.X, -this.CurrentSelectionLocation.Top )
                   );
            }
        }
    }

    /// <summary>
    /// here be tanks
    /// </summary>
    [Script]
    public class ArenaControl : LayeredControl
    {


        public ArenaControl()
        {
            InitializeCanvasSelection();
            InitializeCanvasDrag();
        }

        public int SelectionMinimumSize = 4;

        bool IsSelectionMinimumSize(Rectangle e)
        {
            var _w = e.Width < SelectionMinimumSize;
            var _h = e.Height < SelectionMinimumSize;

            return _w || _h;
        }

        void InitializeCanvasSelection()
        {
            var u = this.Layers.User;

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

                    
                    if (IsSelectionMinimumSize(selection_rect))
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
                    if (selection_enabled)
                    {
                        selection_end = e.OffsetPosition - this.CurrentCanvasPosition;

                        UpdateSelection();
                    }
                };

            u.onmouseup +=
                delegate(IEvent e)
                {
                    if (selection_enabled)
                    {
                        if (e.MouseButton == IEvent.MouseButtonEnum.Left)
                        {
                            selection_enabled = false;

                            if (IsSelectionMinimumSize(selection_rect))
                            {
                                if (SelectionClick != null)
                                    SelectionClick(selection_rect);
                            }
                            else
                            {
                                if (ApplySelection != null)
                                    ApplySelection(selection_rect);
                            }


                            this.Layers.CanvasInfo.removeChild(selection);
                        }
                    }
                };

            u.onmouseout +=
                delegate
                {
                    if (selection_enabled)
                    {
                        selection_enabled = false;
                        this.Layers.CanvasInfo.removeChild(selection);
                    }
                };
        }

        public event EventHandler<Point> SelectionClick;

        public event EventHandler<Rectangle> ApplySelection;

        public void DrawTextToInfo(string text, Point p, Color c)
        {
            var box = new IHTMLSpan(text);

            box.style.SetLocation(p.X, p.Y);
            box.style.color = c;

            this.Layers.Info.appendChild(box);
        }


    }

}
