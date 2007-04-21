using ScriptCoreLib;


namespace ScriptCoreLib.JavaScript.Silverlight
{
    [Script(HasNoPrototype = true)]
    public abstract class UIElement : DependencyObject
    {
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
    }


}