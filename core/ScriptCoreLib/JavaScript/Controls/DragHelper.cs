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


        public event EventHandler<Predicate> DragStartValidate;
        public event EventHandler DragStart;
        public event EventHandler DragMove;

//         public event EventHandler Hover;
        
        public event EventHandler MiddleClick;

        public TimeFilter DragMoveFilter = new TimeFilter(30);

        public event EventHandler DragStop;

        IHTMLElement Control;

        EventHandler<IEvent> ondocumentmousemove;
        EventHandler<IEvent> ondocumentmouseup;
        EventHandler<IEvent> onmousedown;

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
                        Control.onmousedown += onmousedown;
                    else
                        Control.onmousedown -= onmousedown;
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
            ondocumentmousemove =
                delegate(IEvent ev)
                {

                    DragTo(ev.CursorPosition - OffsetPosition);
                };

            ondocumentmouseup = delegate(IEvent ev)
                {

                    Point p = DragStartCursorPosition - ev.CursorPosition;

         
                    IsDrag = false;

                    Helper.Invoke(DragStop);

                    Native.Document.onmousemove -= ondocumentmousemove;
                    Native.Document.onmouseup -= ondocumentmouseup;


                
                    if (ev.MouseButton == IEvent.MouseButtonEnum.Middle)
                    {
                        if (p.Z < 128)
                        {
                            Helper.Invoke(MiddleClick);
                        }
                    }
                };

            onmousedown +=
                delegate(IEvent ev)
                {
                    //mousehover.Stop();

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

                    Native.Document.onmousemove += ondocumentmousemove;
                    Native.Document.onmouseup += ondocumentmouseup;
                };



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
