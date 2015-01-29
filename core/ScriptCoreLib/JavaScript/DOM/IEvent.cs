using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib;
using System;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/Event.webidl

    [Script(HasNoPrototype = true)]
    public class IEvent<TTargetElement> : IEvent where TTargetElement : INode
    {
        [Obsolete("experimental")]
        public new TTargetElement Element
        {
            // X:\jsc.svn\examples\javascript\linq\WebSQLXElement\WebSQLXElement\Application.cs
            [Script(DefineAsStatic = true)]
            get
            {
                return (TTargetElement)((IEvent)this).Element;
            }
        }

    }


    // http://src.chromium.org/viewvc/blink/trunk/Source/core/events/Event.idl

    // http://www.w3.org/TR/DOM-Level-2-Events/idl-definitions.html
    // http://www.w3.org/TR/DOM-Level-3-Events/
    [Script(HasNoPrototype = true)]
    public class IEvent
    {
        // x:\jsc.svn\examples\javascript\webgl\WebGLGodRay\WebGLGodRay\Application.cs
        public static implicit operator bool (IEvent e)
        {
            // future C# may allow if (obj)
            // but for now booleans are needed

            // enable 
            // while (await Native.window.async.onresize);
            return ((object)e != null);
        }

        #region Element
        [Script(OptimizedCode = @"
            if (a0['target'] != void(0)) 
                return a0.target;
            if (a0['srcElement'] != void(0)) 
                return a0.srcElement;
            ")]
        internal static INode InternalElement(object a0) { return default(INode); }

        public INode Element
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return InternalElement(this);
            }
        }

        #endregion

        // http://www.javascriptkit.com/jsref/event.shtml
        // http://msdn.microsoft.com/library/default.asp?url=/workshop/author/dhtml/reference/objects/obj_event.asp

        public bool ctrlKey;
        public bool shiftKey;
        public bool altKey;

        internal readonly int button;
        internal readonly int which;

        public bool IsReturn
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return KeyCode == 13;
            }
        }

        public bool IsEscape
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return KeyCode == 27;
            }
        }




        /// <summary>
        /// returns the character code, escape (27) or enter (13)
        /// </summary>
        public int KeyCode
        {
            [Script(DefineAsStatic = true)]
            get
            {
                // http://qooxdoo.org/documentation/user_manual/keyboard_events

                int x = 0;

                if (Expando.InternalIsMember(this, "charCode"))
                {
                    x = (int)Expando.InternalGetMember(this, "charCode");

                    if (x == 0)
                    {
                        if (Expando.InternalIsMember(this, "keyCode"))
                        {
                            int z = (int)Expando.InternalGetMember(this, "keyCode");

                            x = z;
                        }
                    }
                }
                else
                {
                    if (Expando.InternalIsMember(this, "keyCode"))
                    {
                        x = (int)Expando.InternalGetMember(this, "keyCode");
                    }
                }

                return x;
            }

        }

        public int WheelDirection
        {
            [Script(DefineAsStatic = true)]
            get
            {
                int i = 0;

                if (Expando.InternalIsMember(this, "detail"))
                    i = -(int)Expando.InternalGetMember(this, "detail");

                if (Expando.InternalIsMember(this, "wheelDelta"))
                    i = (int)Expando.InternalGetMember(this, "wheelDelta");

                if (i == 0)
                    return 0;

                if (i > 0)
                    return 1;

                return -1;
            }
        }

        public int OffsetX
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var x = Expando.GetMemberOf<int>(this, "layerX", "offsetX", 0);

                return x;
            }
        }

        public int OffsetY
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var y = Expando.GetMemberOf<int>(this, "layerY", "offsetY", 0);

                return y;
            }
        }

        #region cursor position
        // ff
        internal int pageX;
        internal int pageY;

        // ie
        internal int clientX;
        internal int clientY;

        public Point CursorPosition
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return new Point(CursorX, CursorY);
            }
        }

        public Point OffsetPosition
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return new Point(OffsetX, OffsetY);
            }
        }

        public int CursorX
        {
            [Script(DefineAsStatic = true)]
            get
            {
                int x = 0;

                if (Expando.InternalIsMember(this, "pageX"))
                {
                    x = this.pageX;

                }
                else if (Expando.InternalIsMember(this, "clientX"))
                {
                    x = this.clientX;
                }

                return x + ((HTML.IHTMLElement)this.Element.ownerDocument.documentElement).scrollLeft;
            }
        }

        public int CursorY
        {
            [Script(DefineAsStatic = true)]
            get
            {
                int r = 0;

                if (Expando.InternalIsMember(this, "pageY"))
                    r = this.pageY;

                if (Expando.InternalIsMember(this, "clientY"))
                    r = this.clientY;

                return r + ((HTML.IHTMLElement)this.Element.ownerDocument.documentElement).scrollTop;
            }
        }

        #endregion


        public void stopImmediatePropagation()
        {
        }

        #region StopPropagation

        [Script(DefineAsStatic = true)]
        public void stopPropagation() { InternalStopPropagation(this); }


        [Script(DefineAsStatic = true)]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("stopPropagation")]
        public void StopPropagation() { InternalStopPropagation(this); }

        [Script(OptimizedCode = @"
            if (a0['cancelBubble'] != void(0)) 
                a0.cancelBubble = true;

            if (a0['stopPropagation'] != void(0)) 
                a0.stopPropagation(); 
            ")]
        static internal void InternalStopPropagation(object a0) { }
        #endregion

        public enum MouseButtonEnum
        {
            Unknown,

            Left,
            Middle,
            Right
        }

        /// <summary>
        /// firefox may not set this value at mousemove
        /// </summary>
        public MouseButtonEnum MouseButton
        {
            [Script(DefineAsStatic = true)]
            get
            {

                if (Expando.InternalIsMember(this, "which"))
                {

                    if (which == 3)
                        return MouseButtonEnum.Right;

                    if (which == 2)
                        return MouseButtonEnum.Middle;

                    if (which == 1)
                        return MouseButtonEnum.Left;
                }


                if (Expando.InternalIsMember(this, "button"))
                {
                    if (button == 2)
                        return MouseButtonEnum.Right;

                    if (button == 4)
                        return MouseButtonEnum.Middle;

                    if (button == 1)
                        return MouseButtonEnum.Left;
                }






                return MouseButtonEnum.Unknown;
            }
        }

        [Obsolete]
        internal bool IsMozilla
        {
            [Script(DefineAsStatic = true)]
            get
            { return InternalIsMozilla(this); }
        }

        [Script(OptimizedCode = @"
            return !window['event'];
            ", UseCompilerConstants = true)]
        static internal bool InternalIsMozilla(object a0) { return false; }

        #region PreventDefault
        [Script(DefineAsStatic = true)]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("preventDefault")]
        public void PreventDefault() { InternalPreventDefault(this); }

        [Script(DefineAsStatic = true)]
        public void preventDefault() { InternalPreventDefault(this); }

        internal string returnValue;

        [Script(OptimizedCode = @"
           
            if ('returnValue' in a)
                a.returnValue = false;

            if ('stopPropagation' in a) 
                a.preventDefault(); 
            ")]
        static internal void InternalPreventDefault(object a) { }
        #endregion




        //internal void initMouseEvent(object type, object canBubble, object cancelable, object view,
        //             object detail, object screenX, object screenY, object clientX, object clientY,
        //             object ctrlKey, object altKey, object shiftKey, object metaKey,
        //             object button, object relatedTarget)
        //{

        //}

        internal string type;
        internal bool bubbles;
        internal bool cancelable;
        internal object view;
        internal int detail;
        internal int screenX;
        internal int screenY;
        internal bool metaKey;
        internal object relatedTarget;

        // http://dvcs.w3.org/hg/pointerlock/raw-file/default/index.html
        public int movementX
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var f = new IFunction(@"
		if (this.movementX) {
		    return this.movementX;
		}
		else if (this.webkitMovementX) {
		    return this.webkitMovementX;
		}
                    return 0;
                ");

                return (int)f.apply(this);
            }
        }
        public int movementY
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var f = new IFunction(@"
		if (this.movementY) {
		    return this.movementY;
		}
		else if (this.webkitMovementY) {
		    return this.webkitMovementY;
		}
                    return 0;
                ");

                return (int)f.apply(this);
            }
        }
    }
}
