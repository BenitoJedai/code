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
    }


}