using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;


namespace ScriptCoreLib.JavaScript.Silverlight
{
    [Script(HasNoPrototype = true)]
    public class SilverlightControl : IHTMLElement
    {
        // http://msdn2.microsoft.com/en-us/library/bb229694.aspx
        public bool FullScreen;

        public DependencyObject FindName(string name)
        {
            return default(DependencyObject);
        }
    }


}