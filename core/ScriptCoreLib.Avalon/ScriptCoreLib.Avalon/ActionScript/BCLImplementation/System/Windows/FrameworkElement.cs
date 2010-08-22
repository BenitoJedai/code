using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows
{
    [Script(Implements = typeof(global::System.Windows.FrameworkElement))]
    internal class __FrameworkElement : __UIElement
    {
        public int InternalZIndex;
        public bool InternalZIndexPending;

        public string Name
        {
            set
            {
                this.InternalGetDisplayObjectDirect().name = value;
            }
        }

        public virtual void InternalSetWidth(double value)
        {
            throw new NotImplementedException();
        }

        public virtual void InternalSetHeight(double value)
        {
            throw new NotImplementedException();
        }





        public double Width
        {
            get
            {
                return VirtualGetWidth();

            }
            set
            {
                InternalSetWidth(value);
                InternalRaiseSizeChanged();
            }
        }


        public double Height
        {
            get
            {
                return VirtualGetHeight();

            }
            set
            {
                InternalSetHeight(value);
                InternalRaiseSizeChanged();
            }
        }

        public event SizeChangedEventHandler SizeChanged;

        Size InternalPreviousSize;

        bool InternalRaiseSizeChangedReentryGuard;

        private void InternalRaiseSizeChanged()
        {
            if (InternalRaiseSizeChangedReentryGuard)
                return;


            if (SizeChanged == null)
                return;

            InternalRaiseSizeChangedReentryGuard = true;
            var NewSize = new Size(this.Width, this.Height);

            SizeChanged(this,
                (SizeChangedEventArgs)(object)new __SizeChangedEventArgs
                {
                    NewSize = NewSize,
                    PreviousSize = InternalPreviousSize
                }
            );

            InternalPreviousSize = NewSize;
            InternalRaiseSizeChangedReentryGuard = false;
        }



        public Cursor InternalCursorValue;

        public void InternalSetCursor(Cursor value)
        {
            if (InternalCursorValue == null)
            {
                this.InternalGetDisplayObjectDirect().mouseOver +=
                    delegate
                    {
                        if (InternalCursorValue == Cursors.None)
                            global::ScriptCoreLib.ActionScript.flash.ui.Mouse.hide();
                    };

                this.InternalGetDisplayObjectDirect().mouseOut +=
                    delegate
                    {
                        if (InternalCursorValue == Cursors.None)
                            global::ScriptCoreLib.ActionScript.flash.ui.Mouse.show();
                    };
            }


            InternalCursorValue = value;

            if (value == Cursors.Hand)
            {
                var Sprite = this.InternalGetDisplayObjectDirect() as global::ScriptCoreLib.ActionScript.flash.display.Sprite;

                if (Sprite != null)
                {
                    Sprite.buttonMode = true;
                    Sprite.useHandCursor = true;
                }


            }

            // are we showing and hiding once too often?
            if (InternalCursorValue == Cursors.None)
                global::ScriptCoreLib.ActionScript.flash.ui.Mouse.hide();
            else
                global::ScriptCoreLib.ActionScript.flash.ui.Mouse.show();

        }

        public Cursor Cursor
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                InternalSetCursor(value);
            }
        }

        public DependencyObject InternalParent;

        public DependencyObject Parent { get { return this.InternalParent; } }

        public static implicit operator global::System.Windows.FrameworkElement(__FrameworkElement e)
        {
            return (global::System.Windows.FrameworkElement)(object)e;
        }

        public static implicit operator __FrameworkElement(global::System.Windows.FrameworkElement e)
        {
            return (__FrameworkElement)(object)e;
        }
    }
}
