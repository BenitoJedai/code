using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Input
{
    [Script(ImplementsViaAssemblyQualifiedName = "System.Windows.Input.TouchEventArgs, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
    internal class __TouchEventArgs : __InputEventArgs
    {
        public ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement InternalElement;
        public ScriptCoreLib.JavaScript.DOM.Touch InternalValue;
        public ScriptCoreLib.JavaScript.DOM.TouchEvent InternalEvent;


        __TouchDevice InternalTouchDevice;
        public __TouchDevice TouchDevice
        {
            get
            {
                if (InternalTouchDevice == null)
                {
                    InternalTouchDevice = new __TouchDevice
                    {
                    };

                    if (this.InternalValue != null)
                        InternalTouchDevice.Id = this.InternalValue.identifier;


                }
                return InternalTouchDevice;
            }
        }



        public __TouchPoint GetTouchPoint(IInputElement relativeTo)
        {
            __IInputElement _relativeTo = (__IInputElement)(object)relativeTo;

            #region magic, yet works

            var pp = new Point(0, 0);

            if (this.InternalValue != null)
            {

                pp.X = this.InternalValue.pageX;
                pp.Y = this.InternalValue.pageY;

            };


            var a = GetPositionData.Of(_relativeTo.InternalGetDisplayObjectDirect());
            var b = GetPositionData.Of(this.InternalElement);


            if (b.Count > 0)
            {
                var item = b.Last();

                pp.X -= item.X;
                pp.Y -= item.Y;
            }


            // top elements might be the same so we remove them
            var loop = true;

            while (loop)
            {
                loop = false;

                if (a.Count > 0)
                    if (b.Count > 0)
                        if (a[a.Count - 1].Element == b[b.Count - 1].Element)
                        {
                            a.RemoveAt(a.Count - 1);
                            b.RemoveAt(b.Count - 1);

                            loop = true;
                        }
            }

            if (a.Count > 0)
            {
                var itembb = a.Last();

                pp.X -= itembb.X;
                pp.Y -= itembb.Y;
            }

            if (b.Count > 0)
            {
                var item = b.Last();

                pp.X += item.X;
                pp.Y += item.Y;
            }

            #endregion




            return new __TouchPoint
            {
                TouchDevice = TouchDevice,
                Position = pp
            };
        }

        [Script]
        class GetPositionData
        {
            public IHTMLElement Element;

            public int X;
            public int Y;

            public static List<GetPositionData> Of(IHTMLElement e)
            {
                var a = new List<GetPositionData>();

                var x = 0;
                var y = 0;

                while (ShouldVisitParent(e))
                {
                    x += e.offsetLeft;
                    y += e.offsetTop;

                    a.Add(
                        new GetPositionData
                        {
                            Element = e,
                            X = x,
                            Y = y
                        }
                    );

                    e = (IHTMLElement)e.parentNode;
                }

                return a;
            }

            static bool ShouldVisitParent(IHTMLElement e)
            {
                if (e.parentNode == null)
                    return false;

                return (INode)e.parentNode != Native.Document;
            }
        }
    }
}
