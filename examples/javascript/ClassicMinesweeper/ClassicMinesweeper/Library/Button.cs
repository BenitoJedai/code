using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Diagnostics;

namespace MineSweeper.js
{
    class Property<T>
    {
        public event Action Changed;

        T _Value;


        public T Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                if (Changed != null)
                    Changed();
            }
        }

        public override string ToString()
        {

            return new { Value }.ToString();
        }
    }


    class Button
    {
        public readonly IHTMLDiv Control = new IHTMLDiv();

        public event Action Click;
        public event Action ContextClick;

        public bool ContextEnabled { get; set; }
        public bool Enabled { get; set; }

        string _Source;
        public string Source { get { return _Source; } set { _Source = value; Update(); } }
        string _MouseDownSource;
        public string MouseDownSource { get { return _MouseDownSource; } set { _MouseDownSource = value; Update(); } }

        Action Update;

        public Action<bool> MouseDownChanged;

        public Button(int Width, int Height)
        {
            Enabled = true;
            ContextEnabled = true;

            Control.style.SetSize(Width, Height);

            var mousedown = new Property<bool> { Value = false };

            mousedown.Changed +=
                delegate
                {
                    if (MouseDownChanged != null)
                        MouseDownChanged(mousedown.Value);
                };

            Func<string> GetSource =
                delegate
                {
                    if (Enabled)
                        if (mousedown.Value)
                            return MouseDownSource;

                    return Source;
                };

            var LongClick = new Stopwatch();



            Control.ontouchstart += e =>
            {
                e.preventDefault();
                //e.stopImmediatePropagation();
                e.stopPropagation();

                mousedown.Value = true; LongClick.Start();
            };

            Control.ontouchmove += e =>
           {
               e.preventDefault();
               //e.stopImmediatePropagation();
               e.stopPropagation();
           };

            Control.ontouchend += e =>
            {
                e.preventDefault();
                //e.stopImmediatePropagation();
                e.stopPropagation();

                mousedown.Value = false; LongClick.Stop();

                if (LongClick.ElapsedMilliseconds > 200)
                {
                    if (ContextClick != null)
                        ContextClick();

                    return;
                }
                RaiseClick();
            };

            Control.onmousedown += e => { e.preventDefault(); mousedown.Value = true; LongClick.Start(); };
            Control.onmouseup += e => { e.preventDefault(); mousedown.Value = false; LongClick.Stop(); };
            Control.onmouseout += e => { e.preventDefault(); mousedown.Value = false; LongClick.Stop(); };

            Control.onselectstart +=
                e =>
                {
                    e.preventDefault();
                    e.stopPropagation();

                };

            Control.oncontextmenu +=
              e =>
              {
                  e.preventDefault();
                  e.stopPropagation();

                  if (ContextClick != null)
                      ContextClick();

              };
            Control.onclick +=
                e =>
                {
                    e.preventDefault();
                    e.stopPropagation();

                    if (ContextEnabled)
                        if (e.altKey || e.ctrlKey || e.shiftKey || LongClick.ElapsedMilliseconds > 200)
                        {
                            if (ContextClick != null)
                                ContextClick();

                            return;
                        }
                    RaiseClick();
                };


            Update =
                delegate
                {
                    Control.style.SetBackground(GetSource());
                };

            mousedown.Changed += Update;
        }

        public void RaiseClick()
        {

            if (Enabled)
                if (Click != null)
                    Click();
        }





    }

}
