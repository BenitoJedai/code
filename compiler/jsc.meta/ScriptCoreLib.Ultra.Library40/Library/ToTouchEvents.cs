using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ScriptCoreLib.Library
{
    public class ToTouchEvents<T>
    {


        public ToTouchEvents(UIElement that, Func<T> NextTouchContext)
        {
            var s = new Stack<T>();
            var x = new Dictionary<int, T>();

            Func<T> Pop = delegate
            {
                if (s.Count > 0)
                    return s.Pop();

                return NextTouchContext();
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
                    this.TouchDown(m, e);
            };

            that.TouchMove += (k, e) =>
            {
                var id = e.TouchDevice.Id;

                if (!x.ContainsKey(id))
                {
                    var m = Pop();
                    x[id] = m;

                    if (this.TouchDown != null)
                        this.TouchDown(m, e);
                }


                {
                    var m = x[id];


                    if (this.TouchMove != null)
                        this.TouchMove(m, e);
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
                        this.TouchUp(m, e);
                }
            };
        }

        public event Action<T, TouchEventArgs> TouchDown;
        public event Action<T, TouchEventArgs> TouchMove;
        public event Action<T, TouchEventArgs> TouchUp;
    }
}
