using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public class IHTMLElementTemplate : IHTMLElement
    {


        #region Constructor

        public IHTMLElementTemplate()
        {
            // InternalConstructor
        }

        [Script(OptimizedCode = @"")]
        static IHTMLElementTemplate InternalConstructor()
        {
            return default(IHTMLElementTemplate);
        }

        #endregion


    }
}
