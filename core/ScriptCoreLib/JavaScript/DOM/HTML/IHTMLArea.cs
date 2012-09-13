using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{

    [Script(IsStringEnum = true)]
    public enum ShapeEnum
    {
        polygon = 0,
        rect = 1,
        circle = 2,
    }

    [Script(InternalConstructor = true)]
    public class IHTMLArea : IHTMLElement
    {
        public string coords;
        public string href;
        public ShapeEnum shape;
        public string target;


        #region Constructor

        public IHTMLArea()
        {
            // InternalConstructor
        }

        static IHTMLArea InternalConstructor()
        {
            return (IHTMLArea) new IHTMLElement(HTMLElementEnum.area);
        }

        #endregion


    }
}
