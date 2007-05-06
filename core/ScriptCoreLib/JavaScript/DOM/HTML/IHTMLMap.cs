using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public class IHTMLMap : IHTMLElement
    {
        [Script(IsStringEnum=true)]
        public enum ShapeEnum
        {
            polygon,
            rec,
            circle
        }

        public ShapeEnum shape;
        public string coords;
        public string href;
        public string target;

        #region Constructor

        public IHTMLMap()
        {
            // InternalConstructor
        }

        static IHTMLMap InternalConstructor()
        {
            return (IHTMLMap) new IHTMLElement(HTMLElementEnum.map);
        }

        #endregion


    }
}
