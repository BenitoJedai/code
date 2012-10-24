﻿//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Query;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

using IDisposable = global::System.IDisposable;
using System;



namespace ScriptCoreLib.JavaScript.Controls.LayeredControl
{

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
            public IHTMLDiv Canvas;
            public IHTMLDiv CanvasInfo;

            public IHTMLDiv Info;
            public IHTMLDiv User;
        }

        public readonly LayersGroup Layers = new LayersGroup();

        public LayeredControl()
        {

            Layers.Canvas = new IHTMLDiv { name = "LayersGroup_Canvas" };
            Layers.CanvasInfo = new IHTMLDiv { name = "LayersGroup_CanvasInfo" };
            Layers.Info = new IHTMLDiv { name = "LayersGroup_Info" };
            Layers.User = new IHTMLDiv { name = "LayersGroup_User" };


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

            Control.style.zIndex = 0;

            Layers.Canvas.style.zIndex = 1000;
            Layers.CanvasInfo.style.zIndex = 2000;
            Layers.Info.style.zIndex = 3000;
            Layers.User.style.zIndex = 4000;

            Layers.Canvas.style.backgroundColor = Color.Blue;


            //Layers.User.style.backgroundColor = Color.Red;
            //Layers.User.style.zIndex = 0x1000;
            Layers.User.style.Opacity = 0.0;

            // safari not supported: something wrong with zIndex
            // also check http://unixpapa.com/js/mouse.html


            Layers.User.style.backgroundColor = Color.Yellow;
            //Layers.User.style.Opacity = 0.5;
            //Layers.User.style.zIndex = 99999;

            //Layers.User.style.backgroundImage = "url(" + Assets.Path + "/empty.gif)";
        }

        public Point CurrentCanvasPosition = Point.Zero;

        public event System.Action<Rectangle> CanvasViewChanged;

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
            CurrentCanvasSize = new Point((int)System.Math.Round((double)p.X), (int)System.Math.Round((double)p.Y));

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


        [Script]
        public class CanvasRectangle : IDisposable
        {
            public Rectangle Location;
            public Color BackgroundColor;

            internal Action<CanvasRectangle> _Update;
            internal Action<CanvasRectangle> _Dispose;

            public void Update()
            {
                _Update(this);
            }

            public void Dispose()
            {
                _Dispose(this);
            }
        }

        public void DrawRectangleToCanvas(CanvasRectangle c)
        {
            var x = new IHTMLDiv();

            x.style.overflow = IStyle.OverflowEnum.hidden;

            c._Dispose =
                delegate(CanvasRectangle u)
                {
                    if (x == null)
                        return;
                    x.Dispose();
                    x = null;
                };

            c._Update =
                delegate(CanvasRectangle u)
                {
                    if (x == null)
                        return;

                    var r = u.Location;

                    x.style.SetLocation(r.Left, r.Top, r.Width, r.Height);
                    x.style.backgroundColor = u.BackgroundColor;
                };

            c.Update();

            this.Layers.Canvas.appendChild(x);

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

            #region onmousemove
            u.onmousedown +=
                e =>
                {


                    if (e.MouseButton == IEvent.MouseButtonEnum.Middle)
                    {
                        drag_enabled = true;
                        drag_start = e.OffsetPosition - this.CurrentCanvasPosition;
                        //e.CaptureMouse();
                        // can we do this?
                        u.requestPointerLock();
                    }


                };

            u.onmousemove +=
                delegate(IEvent e)
                {
                    if (drag_enabled)
                    {
                        if (Native.Document.pointerLockElement == u)
                        {
                            this.SetCanvasPosition(
                                new Point(
                                    this.CurrentCanvasPosition.X + e.movementX,
                                    this.CurrentCanvasPosition.Y + e.movementY
                                )
                            );
                        }
                        else
                        {
                            this.SetCanvasPosition(e.OffsetPosition - drag_start);
                        }
                    }

                };

            u.onmouseup +=
                delegate(IEvent e)
                {
                    if (e.MouseButton == IEvent.MouseButtonEnum.Middle)
                    {
                        drag_enabled = false;

                        if (Native.Document.pointerLockElement == u)
                        {
                            Native.Document.exitPointerLock();
                        }
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
            #endregion



            #region onmousemove
            u.ontouchstart +=
                e =>
                {
                    e.PreventDefault();
                    drag_enabled = true;
                    var OffsetPosition = new Point(e.touches[0].clientX, e.touches[0].clientY);

                    drag_start = OffsetPosition - this.CurrentCanvasPosition;
                };

            u.ontouchmove +=
                e =>
                {
                    if (drag_enabled)
                    {
                        e.PreventDefault();
                        var OffsetPosition = new Point(e.touches[0].clientX, e.touches[0].clientY);

                        this.SetCanvasPosition(OffsetPosition - drag_start);
                    }

                };

            u.ontouchend +=
                e =>
                {
                    e.PreventDefault();
                    drag_enabled = false;


                };


            #endregion
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
            InitializeMouseMove();


        }

        void InitializeMouseMove()
        {
            var state = true;
            var u = this.Layers.User;

            u.onmousedown += delegate { state = false; };
            u.onmouseup += delegate { state = true; };

            u.onmousemove +=
                delegate(IEvent ev)
                {
                    try
                    {
                        if (state)
                        {
                            Helper.Invoke(this.MouseMove, ev.OffsetPosition - this.CurrentCanvasPosition);
                        }
                    }
                    catch
                    {
                        // document unloaded
                    }
                };
        }

        public event System.Action<Point> MouseMove;

        public int SelectionMinimumSize = 4;

        bool IsSelectionMinimumSize(Rectangle e)
        {
            var _w = e.Width < SelectionMinimumSize;
            var _h = e.Height < SelectionMinimumSize;

            return _w || _h;
        }

        /// <summary>
        /// while this property is set to false the selection rectangle wont be shown
        /// </summary>
        public bool ShowSelectionRectangle = true;

        public event Action<Rectangle> SelectionPreview;
        public event Action<Point, Point> SelectionPointsPreview;

        bool _InSelectionMode = false;

        public bool InSelectionMode
        {
            get
            {
                return _InSelectionMode;
            }
        }

        void InitializeCanvasSelection()
        {
            var u = this.Layers.User;

            var selection = new IHTMLDiv();

            selection.style.border = "1px solid #ffffff";
            selection.style.overflow = IStyle.OverflowEnum.hidden;


            var selection_start = Point.Zero;
            var selection_end = Point.Zero;
            var selection_rect = new Rectangle();

            #region UpdateSelection
            System.Action UpdateSelection =
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


                    if (ShowSelectionRectangle)
                        if (IsSelectionMinimumSize(selection_rect))
                        {
                            selection.style.display = IStyle.DisplayEnum.none;
                        }
                        else
                        {
                            selection.style.display = IStyle.DisplayEnum.block;
                            selection.style.SetLocation(selection_rect);
                        }

                    if (SelectionPreview != null)
                        SelectionPreview(selection_rect);

                    if (SelectionPointsPreview != null)
                        SelectionPointsPreview(selection_start, selection_end);
                };
            #endregion

            Action ReleaseCapture = null;

            u.onmousedown +=
                e =>
                {

                    if (e.MouseButton == IEvent.MouseButtonEnum.Left)
                    {
                        _InSelectionMode = true;

                        if (ShowSelectionRectangle)
                            this.Layers.CanvasInfo.appendChild(selection);

                        selection_start = e.OffsetPosition - this.CurrentCanvasPosition;
                        selection_end = selection_start;

                        UpdateSelection();

                        Console.WriteLine("CaptureMouse");
                        e.PreventDefault();
                        e.StopPropagation();
                        ReleaseCapture = u.CaptureMouse();
                    }
                };




            u.onmousemove +=
                delegate(IEvent e)
                {
                    if (_InSelectionMode)
                    {
                        selection_end = e.OffsetPosition - this.CurrentCanvasPosition;

                        e.PreventDefault();
                        e.StopPropagation();
                        UpdateSelection();
                    }
                };

            u.onmouseup +=
                delegate(IEvent e)
                {
                    if (_InSelectionMode)
                    {
                        if (e.MouseButton == IEvent.MouseButtonEnum.Left)
                        {
                            _InSelectionMode = false;
                            e.PreventDefault();
                            e.StopPropagation();

                            if (IsSelectionMinimumSize(selection_rect))
                            {
                                var p = new Point(selection_end.X, selection_end.Y);

                                if (SelectionClick != null)
                                    SelectionClick(p, e);
                            }
                            else
                            {
                                var r = new Rectangle
                                        {
                                            Left = selection_rect.Left,
                                            Top = selection_rect.Top,
                                            Width = selection_rect.Width,
                                            Height = selection_rect.Height,
                                        };

                                if (ApplySelection != null)
                                    ApplySelection(r, e);

                                if (ApplyPointsSelection != null)
                                    ApplyPointsSelection(selection_start, selection_end, e);
                            }

                            if (ShowSelectionRectangle)
                                this.Layers.CanvasInfo.removeChild(selection);

                            if (ReleaseCapture != null)
                            {
                                Console.WriteLine("ReleaseCapture");
                                ReleaseCapture();
                                ReleaseCapture = null;
                            }
                        }
                    }
                };

            //u.onmouseout +=
            //    delegate
            //    {
            //        if (_InSelectionMode)
            //        {
            //            _InSelectionMode = false;

            //            if (ShowSelectionRectangle)
            //                this.Layers.CanvasInfo.removeChild(selection);

            //            if (ReleaseCapture != null)
            //            {
            //                Console.WriteLine("ReleaseCapture");
            //                ReleaseCapture();
            //                ReleaseCapture = null;
            //            }
            //        }
            //    };
        }

        public event System.Action<Point, IEvent> SelectionClick;

        public event System.Action<Rectangle, IEvent> ApplySelection;
        public event Action<Point, Point, IEvent> ApplyPointsSelection;

        public void DrawTextToInfo(string text, Point p, Color c)
        {
            var box = new IHTMLSpan(text);

            box.style.SetLocation(p.X, p.Y);
            box.style.color = c;

            this.Layers.Info.appendChild(box);
        }




    }

}
