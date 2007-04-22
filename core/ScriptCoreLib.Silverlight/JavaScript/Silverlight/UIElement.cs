using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Silverlight.Input;

using ScriptCoreLib.Shared;


namespace ScriptCoreLib.JavaScript.Silverlight
{


    [Script(IsStringEnum=true)]
    public enum MouseCursor
    {
        /// <summary>
        /// An arrow cursor. This is typically the default cursor.
        /// </summary>
        Arrow,
        /// <summary>
        /// Specifies that an element expresses no cursor preference. If the element's parent specifies a cursor, that cursor is displayed.
        /// </summary>
        Default,
        /// <summary>
        /// A hand cursor. This cursor typically indicates that the pointer is over a link.
        /// </summary>
        Hand,

        /// <summary>
        /// An I-beam cursor. This cursor typically indicates that text can be manipulated or selected.
        /// </summary>
        IBeam,
        /// <summary>
        /// No cursor.
        /// </summary>
        None,

        /// <summary>
        /// A wait (or hourglass) cursor. This cursor typically indicates that the runtime is busy performing an operation.
        /// </summary>
        Wait

    }


    [Script(HasNoPrototype = true)]
    public abstract class UIElement : DependencyObject, ISilverlightEventSink
    {
        // http://msdn2.microsoft.com/en-us/library/bb232899.aspx

        public MouseCursor Cursor;
        public double Opacity;

        public SilverlightControl GetHost()
        {
            return default(SilverlightControl);
        }

        public void AddEventListener(string eventType, string functionName)
        {
        }

        public void RemoveEventListener(string eventType, string functionName)
        {
        }





        public bool CaptureMouse()
        {
            return default(bool);
        }

        public void ReleaseMouseCapture()
        {
        }

        public DependencyObject FindName(string name)
        {
            return default(DependencyObject);
        }


        // http://msdn2.microsoft.com/en-us/library/bb190687.aspx
        public double CanvasLeft
        {
            [Script(DefineAsStatic = true)]
            get { return this.GetValue<double>("Canvas.Left"); }
            [Script(DefineAsStatic = true)]
            set { this.SetValue("Canvas.Left", value); }
        }

        public double CanvasTop
        {
            [Script(DefineAsStatic = true)]
            get { return this.GetValue<double>("Canvas.Top"); }
            [Script(DefineAsStatic = true)]
            set { this.SetValue("Canvas.Top", value); }
        }


        #region events

        public event Action<UIElement, MouseEventArgs> MouseLeftButtonUp
        {
            [Script(DefineAsStatic = true)]
            add { this.AddEventListenerAsProxy("MouseLeftButtonUp", value); }
            [Script(DefineAsStatic = true)]
            remove { this.RemoveEventListenerAsProxy("MouseLeftButtonUp", value); }
        }

        public event Action<UIElement, MouseEventArgs> MouseLeftButtonDown
        {
            [Script(DefineAsStatic = true)]
            add { this.AddEventListenerAsProxy("MouseLeftButtonDown", value); }
            [Script(DefineAsStatic = true)]
            remove { this.RemoveEventListenerAsProxy("MouseLeftButtonDown", value); }
        }

        public event Action<UIElement, MouseEventArgs> MouseEnter
        {
            [Script(DefineAsStatic = true)]
            add { this.AddEventListenerAsProxy("MouseEnter", value); }
            [Script(DefineAsStatic = true)]
            remove { this.RemoveEventListenerAsProxy("MouseEnter", value); }
        }

        public event Action<UIElement, MouseEventArgs> MouseLeave
        {
            [Script(DefineAsStatic = true)]
            add { this.AddEventListenerAsProxy("MouseLeave", value); }
            [Script(DefineAsStatic = true)]
            remove { this.RemoveEventListenerAsProxy("MouseLeave", value); }
        }

        public event Action<UIElement, MouseEventArgs> MouseMove
        {
            [Script(DefineAsStatic = true)]
            add { this.AddEventListenerAsProxy("MouseLeave", value); }
            [Script(DefineAsStatic = true)]
            remove { this.RemoveEventListenerAsProxy("MouseLeave", value); }
        }

        #endregion
    }


}