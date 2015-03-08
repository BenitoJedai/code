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
    [Script]
    public class DragHelper
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

        System.Action<IMouseDownEvent<IHTMLElement>> onmousedown;
        System.Action<TouchEvent> ontouchstart;

        System.Action<TouchEvent> ontouchmove;
        System.Action<TouchEvent> ontouchend;


        // needs to be updated to BCL List
        public global::System.Collections.Generic.List<Point> History;




        #region Enabled
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
                        Control.ontouchstart += ontouchstart;
                        Control.ontouchmove += ontouchmove;
                        Control.ontouchend += ontouchend;

                        Control.onmousedown += onmousedown;
                    }
                    else
                    {
                        Control.ontouchstart -= ontouchstart;
                        Control.ontouchmove -= ontouchmove;
                        Control.ontouchend -= ontouchend;

                        Control.onmousedown -= onmousedown;
                    }
                }


                _Enabled = value;


            }
        }
        #endregion


        public Point DragStartCursorPosition = new Point(0, 0);

        public int HoverTime = 1000;

        public DragHelper(IHTMLElement c)
        {
            Control = c;


            // instance of event - important for removal
            #region move
            this.ondocumentmousemove = ev =>
            {
                DragTo(ev.CursorPosition - OffsetPosition);
            };

            this.ontouchmove = ev =>
            {
                ev.stopPropagation();

                var ev_CursorPosition = new Point(ev.touches[0].clientX, ev.touches[0].clientY);
                DragTo(ev_CursorPosition - OffsetPosition);
            };
            #endregion

            #region ondocumentmouseup
            this.ondocumentmouseup = ev =>
                {



                    IsDrag = false;

                    Helper.Invoke(DragStop);

                    Control.onmousemove -= ondocumentmousemove;
                    Control.onmouseup -= ondocumentmouseup;



                    if (ev.MouseButton == IEvent.MouseButtonEnum.Middle)
                    {
                        Point p = DragStartCursorPosition - ev.CursorPosition;
                        if (p.Z < 128)
                        {
                            Helper.Invoke(MiddleClick);
                        }
                    }
                };
            #endregion

            #region ontouchend
            this.ontouchend = ev =>
            {
                ev.stopPropagation();

                IsDrag = false;

                Helper.Invoke(DragStop);
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

                ev.preventDefault();
                ev.stopPropagation();
                //ev.StopPropagation();

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
