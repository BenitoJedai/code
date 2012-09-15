using ScriptCoreLib;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;



namespace ScriptCoreLib.JavaScript.Controls
{
    [System.Obsolete("To be moved out of CoreLib")]
    [Script]
    internal class DragHelper
    {

        public bool IsDrag;

        public Point Position = new Point(0, 0);

        public Point OffsetPosition = new Point(0, 0);


        public event System.Action<Predicate> DragStartValidate;
        public event System.Action DragStart;
        public event System.Action DragMove;

        //         public event EventHandler Hover;

        public event System.Action MiddleClick;

        public TimeFilter DragMoveFilter = new TimeFilter(30);

        public event System.Action DragStop;

        IHTMLElement Control;

        System.Action<IEvent> ondocumentmousemove;
        System.Action<IEvent> ondocumentmouseup;

        System.Action<IMouseDownEvent> onmousedown;
        System.Action<TouchEvent> ontouchstart;

        System.Action<TouchEvent> ontouchmove;
        System.Action<TouchEvent> ontouchend;


        // needs to be updated to BCL List
        public global::System.Collections.Generic.List<Point> History;


        private bool _Enabled;

        public bool Enabled
        {
            get { return _Enabled; }
            set
            {
                if (_Enabled != value)
                {
                    if (value)
                    {
                        Control.onmousedown += onmousedown;
                        Control.ontouchstart += ontouchstart;
                    }
                    else
                    {
                        Control.onmousedown -= onmousedown;
                        Control.ontouchstart -= ontouchstart;
                    }
                }


                _Enabled = value;


            }
        }

        public Point DragStartCursorPosition = new Point(0, 0);

        public int HoverTime = 1000;

        public DragHelper(IHTMLElement c)
        {
            Control = c;

            ////var mousehover = new Timer(
            ////    delegate
            ////    {
            ////        Helper.Invoke(Hover);
            ////    }
            ////);

            //Control.onmouseover +=
            //    delegate
            //    {
            //        mousehover.StartTimeout();
            //    };

            //Control.onmouseout +=
            //    delegate
            //    {
            //        mousehover.Stop();
            //    };

            //Control.onmousemove +=
            //    delegate
            //    {
            //        if (!IsDrag)
            //            mousehover.StartTimeout(HoverTime);
            //    };

            // instance of event - important for removal
            #region move
            this.ondocumentmousemove = ev =>
            {
                DragTo(ev.CursorPosition - OffsetPosition);
            };

            this.ontouchmove = ev =>
            {
                var ev_CursorPosition = new Point(ev.touches[0].clientX, ev.touches[0].clientY);
                DragTo(ev_CursorPosition - OffsetPosition);
            };
            #endregion

            #region end
            this.ondocumentmouseup = ev =>
                {

                    Point p = DragStartCursorPosition - ev.CursorPosition;


                    IsDrag = false;

                    Helper.Invoke(DragStop);

                    Control.onmousemove -= ondocumentmousemove;
                    Control.onmouseup -= ondocumentmouseup;



                    if (ev.MouseButton == IEvent.MouseButtonEnum.Middle)
                    {
                        if (p.Z < 128)
                        {
                            Helper.Invoke(MiddleClick);
                        }
                    }
                };

            this.ontouchend = ev =>
            {
                var ev_CursorPosition = new Point(ev.touches[0].clientX, ev.touches[0].clientY);

                Point p = DragStartCursorPosition - ev_CursorPosition;


                IsDrag = false;

                Helper.Invoke(DragStop);

                Control.ontouchmove -= ontouchmove;
                Control.ontouchend -= ontouchend;

            };
            #endregion


            #region onmousedown
            this.onmousedown = ev =>
            {
                //mousehover.Stop();

                ev.PreventDefault();
                ev.StopPropagation();

                DragStartCursorPosition = ev.CursorPosition;



                var p = new Predicate();

                p.Value = true;
                p.Invoke(DragStartValidate);

                if (!p.Value)
                    return;

                if (History != null)
                    History.Add(Position);

                OffsetPosition = ev.CursorPosition - Position;

                IsDrag = true;

                Helper.Invoke(DragStart);

                Control.onmousemove += ondocumentmousemove;
                Control.onmouseup += ondocumentmouseup;

                ev.CaptureMouse();
            };
            #endregion

            #region ontouchstart
            this.ontouchstart = ev =>
            {
                //mousehover.Stop();

                ev.PreventDefault();
                ev.StopPropagation();

                var ev_CursorPosition = new Point(ev.touches[0].clientX, ev.touches[0].clientY);

                DragStartCursorPosition = ev_CursorPosition;



                var p = new Predicate();

                p.Value = true;
                p.Invoke(DragStartValidate);

                if (!p.Value)
                    return;

                if (History != null)
                    History.Add(Position);

                OffsetPosition = ev_CursorPosition - Position;

                IsDrag = true;

                Helper.Invoke(DragStart);

                Control.ontouchmove += ontouchmove;
                Control.ontouchend += ontouchend;

                //ev.CaptureMouse();
            };
            #endregion

        }



        public void DragTo(Point point)
        {

            DragMoveFilter.Invoke(
                delegate
                {
                    Position = point;

                    Helper.Invoke(DragMove);
                });
        }


    }

}
