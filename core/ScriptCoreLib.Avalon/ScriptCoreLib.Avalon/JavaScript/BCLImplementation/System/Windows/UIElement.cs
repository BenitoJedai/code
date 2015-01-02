﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows.Input;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media.Animation;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Input;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.BCLImplementation.System;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows
{
    // http://referencesource.microsoft.com/#PresentationCore/src/Core/CSharp/System/Windows/UIElement.cs
    // https://github.com/grokys/Avalonia
    // https://github.com/grokys/Perspex

    [Script(Implements = typeof(global::System.Windows.UIElement))]
    public class __UIElement : __Visual, __IAnimatable, __IInputElement
    {
        #region __IInputElement Members

        public IHTMLElement InternalGetDisplayObjectDirect()
        {
            return InternalGetDisplayObject();
        }

        #endregion

        public virtual IHTMLElement InternalGetDisplayObject()
        {
            throw new NotImplementedException();
        }



        public static implicit operator __UIElement(UIElement e)
        {
            return (__UIElement)(object)e;
        }

        public virtual IHTMLElement InternalGetOpacityTarget()
        {
            return InternalGetDisplayObject();
        }


        #region Opacity
        double InternalOpacity;

        public double Opacity
        {
            get
            {
                // fixme: we cannot get the opacity value from DOM at the moment
                return InternalOpacity;
            }
            set
            {
                InternalOpacity = value;
                InternalGetOpacityTarget().style.Opacity = value;
            }
        }
        #endregion



        public event MouseEventHandler MouseMove
        {
            add
            {

                InternalGetDisplayObject().onmousemove +=
                    e =>
                    {
                        value(this, __MouseEventArgs.Of(e, this));
                    };
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        public event MouseEventHandler MouseEnter
        {
            add
            {

                InternalGetDisplayObject().onmouseover +=
                    e =>
                    {
                        value(this, __MouseEventArgs.Of(e, this));
                    };
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        public event MouseEventHandler MouseLeave
        {
            add
            {

                InternalGetDisplayObject().onmouseout +=
                    e =>
                    {
                        value(this, __MouseEventArgs.Of(e, this));
                    };
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        public bool CaptureMouse()
        {
            var Release = this.InternalGetDisplayObject().CaptureMouse();

            return true;
        }

        Action __ReleaseMouse;

        public event MouseButtonEventHandler MouseLeftButtonDown
        {
            add
            {

                InternalGetDisplayObject().onmousedown +=
                    e =>
                    {
                        e.PreventDefault();

                        __ReleaseMouse = this.InternalGetDisplayObject().CaptureMouse();

                        if (e.MouseButton == ScriptCoreLib.JavaScript.DOM.IEvent.MouseButtonEnum.Left)
                            value(this, (__MouseButtonEventArgs)e);
                    };
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        public event MouseButtonEventHandler MouseLeftButtonUp
        {
            add
            {

                InternalGetDisplayObject().onmouseup +=
                    e =>
                    {
                        if (__ReleaseMouse != null)
                            __ReleaseMouse();

                        e.PreventDefault();

                        __Keyboard.Modifiers = ModifierKeys.None;

                        if (e.shiftKey)
                            __Keyboard.Modifiers |= ModifierKeys.Shift;

                        if (e.ctrlKey)
                            __Keyboard.Modifiers |= ModifierKeys.Control;

                        if (e.altKey)
                            __Keyboard.Modifiers |= ModifierKeys.Alt;

                        if (e.MouseButton == ScriptCoreLib.JavaScript.DOM.IEvent.MouseButtonEnum.Left)
                            value(this, (__MouseButtonEventArgs)e);
                    };
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        public event MouseWheelEventHandler MouseWheel
        {
            add
            {

                InternalGetDisplayObject().onmousewheel +=
                    e =>
                    {
                        value(this, (__MouseWheelEventArgs)e);
                    };
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        public event KeyEventHandler KeyDown
        {
            add
            {

                InternalGetDisplayObject().onkeydown +=
                    e =>
                    {
                        __KeyEventArgs.InternalInvoke(value, this, e);
                    };
            }
            remove
            {
                throw new NotImplementedException();
            }
        }


        public event KeyEventHandler KeyUp
        {
            add
            {

                InternalGetDisplayObject().onkeyup +=
                    e =>
                    {
                        __KeyEventArgs.InternalInvoke(value, this, e);
                    };
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        public Visibility Visibility
        {
            get
            {
                var s = this.InternalGetDisplayObject().style;

                if (s.display == ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.empty)
                    return Visibility.Visible;

                return Visibility.Hidden;
            }
            set
            {
                if (value == Visibility.Visible)
                    this.InternalGetDisplayObject().style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.empty;
                else
                    this.InternalGetDisplayObject().style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.none;

            }
        }

        public event RoutedEventHandler GotFocus
        {
            add
            {

                InternalGetDisplayObject().onfocus +=
                    e =>
                    {
                        value(this, null);
                    };
            }
            remove
            {
                throw new NotImplementedException();
            }
        }


        public event RoutedEventHandler LostFocus
        {
            add
            {

                InternalGetDisplayObject().onblur +=
                    e =>
                    {
                        value(this, null);
                    };
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        public bool Focus()
        {
            var k = this.InternalGetDisplayObjectDirect();

            k.focus();

            return true;
        }

        public Geometry Clip
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                var rg = value as RectangleGeometry;

                if (rg == null)
                    throw new NotSupportedException();

                var r = rg.Rect;

                var e = this.InternalGetDisplayObject();


                // http://www.w3schools.com/CSS/pr_pos_clip.asp

                // shape  	Sets the shape of the element. The valid shape value is: rect (top, right, bottom, left)
                // auto 	Default. The browser sets the shape of the element
                var rect = "rect(";

                rect += Convert.ToInt32(r.Y) + "px, ";
                rect += Convert.ToInt32(r.Right) + "px, ";
                rect += Convert.ToInt32(r.Bottom) + "px, ";
                rect += Convert.ToInt32(r.X) + "px";

                rect += ")";

                e.style.clip = rect;
            }
        }

        public virtual double InternalGetWidth()
        {
            throw new NotImplementedException();
        }

        public virtual double InternalGetHeight()
        {
            throw new NotImplementedException();
        }

        bool InternalClipToBounds;

        public bool ClipToBounds
        {
            get
            {
                return InternalClipToBounds;
            }
            set
            {
                InternalClipToBounds = value;
                InternalUpdateClip();

            }
        }

        internal void InternalUpdateClip()
        {
            // fixme: value = false

            if (InternalClipToBounds)
                ((UIElement)this).ClipTo(0, 0, Convert.ToInt32(InternalGetWidth()), Convert.ToInt32(InternalGetHeight()));
        }

        public Style FocusVisualStyle
        {
            set
            {
                if (value != null)
                    throw new NotImplementedException();

                // hide focus rect if possible
            }
        }

        public bool Focusable
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                var x = this.InternalGetDisplayObject();

                x.tabIndex = 0;

                //x.tabEnabled = true;
                //x.tabIndex
            }
        }

        public static implicit operator UIElement(__UIElement e)
        {
            return (UIElement)(object)e;
        }

        internal Transform InternalRenderTransform;

        public Transform RenderTransform
        {
            get
            {
                return InternalRenderTransform;
            }
            set
            {
                InternalRenderTransform = value;

                #region ScaleTransform
                var AsScaleTransform = value as ScaleTransform;
                if (AsScaleTransform != null)
                {

                    var o = InternalGetDisplayObject();

                    o.style.SetMatrixTransform(
                        AsScaleTransform.ScaleX,
                        0,
                        0,
                        AsScaleTransform.ScaleY,
                        0,
                        0
                    );
                    return;
                }
                #endregion

                #region TranslateTransform
                var AsTranslateTransform = value as TranslateTransform;
                if (AsTranslateTransform != null)
                {
                    var p = (__TranslateTransform)(object)AsTranslateTransform;

                    var o = InternalGetDisplayObject();

                    o.style.SetMatrixTransform(
                        1,
                        0,
                        0,
                        1,
                        p.X,
                        p.Y
                    );
                    return;
                }
                #endregion

                #region MatrixTransform
                var AsMatrixTransform = value as MatrixTransform;
                if (AsMatrixTransform != null)
                {
                    var p = (__MatrixTransform)(object)AsMatrixTransform;

                    var o = InternalGetDisplayObject();

                    o.style.SetMatrixTransform(
                        p.m11,
                        p.m12,
                        p.m21,
                        p.m22,
                        p.offsetX,
                        p.offsetY

                    );
                    return;
                }
                #endregion
            }
        }


        // .NET 4, yay :)



        #region touch

        public event EventHandler<TouchEventArgs> TouchDown
        {
            add
            {
                var s = this.InternalGetDisplayObject();

                s.ontouchstart +=
                    e =>
                    {
                        if (e.touches != null)
                            for (uint i = 0; i < e.touches.length; i++)
                            {
                                var args = new __TouchEventArgs
                                {
                                    InternalElement = s,
                                    InternalValue = e.touches[i],
                                    InternalEvent = e
                                };

                                value(this, args);


                                if (args.Handled)
                                    e.PreventDefault();
                            }
                    };

            }
            remove
            {
                throw new NotImplementedException();
            }
        }



        public event EventHandler<TouchEventArgs> TouchMove
        {
            add
            {
                var s = this.InternalGetDisplayObject();

                s.ontouchmove +=
                    e =>
                    {
                        if (e.touches != null)
                            for (uint i = 0; i < e.touches.length; i++)
                            {
                                var args = new __TouchEventArgs
                                {
                                    InternalElement = s,
                                    InternalValue = e.touches[i],
                                    InternalEvent = e
                                };

                                value(this, args);


                                if (args.Handled)
                                    e.PreventDefault();
                            }
                    };
            }
            remove
            {
                throw new NotImplementedException();
            }
        }
        public event EventHandler<TouchEventArgs> TouchUp
        {
            add
            {
                var s = this.InternalGetDisplayObject();

                s.ontouchend +=
                    e =>
                    {
                        if (e.changedTouches != null)
                            for (uint i = 0; i < e.changedTouches.length; i++)
                            {
                                var args = new __TouchEventArgs
                                {
                                    InternalElement = s,
                                    InternalValue = e.changedTouches[i],
                                    InternalEvent = e
                                };

                                value(this, args);


                                if (args.Handled)
                                    e.PreventDefault();
                            }
                    };
            }
            remove
            {
                throw new NotImplementedException();
            }
        }
        #endregion


        public double InternalLeft;
        public double InternalTop;
    }
}
