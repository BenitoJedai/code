using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ScriptCoreLib.Library
{
    public class TouchEventsTrigger
    {
        public Action<TouchEventArgs> TouchDown;
        public Action<TouchEventArgs> TouchMove;
        public Action<TouchEventArgs> TouchUp;
    }

    public class TouchEvents
    {
        public event Action<TouchEventArgs> TouchDown;
        public event Action<TouchEventArgs> TouchMove;
        public event Action<TouchEventArgs> TouchUp;


        public TouchEvents(TouchEventsTrigger t)
        {
            t.TouchDown =
               e =>
               {
                   if (TouchDown != null)
                       TouchDown(e);
               };

            t.TouchMove =
               e =>
               {
                   if (TouchMove != null)
                       TouchMove(e);
               };

            t.TouchUp =
               e =>
               {
                   if (TouchUp != null)
                       TouchUp(e);
               };
        }
    }

    public class ToTouchEvents<T>
    {
        public readonly List<T> Touches = new List<T>();

        internal class TouchEventsTriggerTuple
        {
            public T Touch;
            public TouchEventsTrigger Trigger;
        }

        public ToTouchEvents(UIElement that, Func<TouchEvents, T> NextTouchContext)
        {
            var s = new Stack<TouchEventsTriggerTuple>();
            var x = new Dictionary<int, TouchEventsTriggerTuple>();

            Func<TouchEventsTriggerTuple> Pop = delegate
            {
                if (s.Count > 0)
                    return s.Pop();

                var tet = new TouchEventsTrigger();
                var te = new TouchEvents(tet);
                var n = NextTouchContext(te);

                this.Touches.Add(n);

                return new TouchEventsTriggerTuple { Touch = n, Trigger = tet };
            };


            that.TouchDown += (k, e) =>
            {
                var id = e.TouchDevice.Id;

                if (x.ContainsKey(id))
                {
                    return;
                }

                var m = Pop();
                x[id] = m;

                if (this.TouchDown != null)
                    this.TouchDown(m.Touch, e);

                m.Trigger.TouchDown(e);
            };

            that.TouchMove += (k, e) =>
            {
                var id = e.TouchDevice.Id;

                if (!x.ContainsKey(id))
                {
                    var m = Pop();
                    x[id] = m;

                    if (this.TouchDown != null)
                        this.TouchDown(m.Touch, e);

                    m.Trigger.TouchDown(e);

                }


                {
                    var m = x[id];


                    if (this.TouchMove != null)
                        this.TouchMove(m.Touch, e);

                    m.Trigger.TouchMove(e);
                }
            };

            that.TouchUp += (k, e) =>
            {
                var id = e.TouchDevice.Id;


                if (!x.ContainsKey(id))
                    return;


                {
                    var m = x[id];

                    s.Push(m);

                    x.Remove(id);

                    if (this.TouchUp != null)
                        this.TouchUp(m.Touch, e);

                    m.Trigger.TouchUp(e);
                }
            };
        }

        public event Action<T, TouchEventArgs> TouchDown;
        public event Action<T, TouchEventArgs> TouchMove;
        public event Action<T, TouchEventArgs> TouchUp;
    }
}
