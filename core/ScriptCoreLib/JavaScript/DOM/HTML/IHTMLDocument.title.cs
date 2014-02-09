using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    public partial class IHTMLDocument
    {
        public string title;

        public event System.Action ontitlechanged
        {
            // jsc experience
            [Script(DefineAsStatic = true)]
            add
            {
                var x = title;

                this.defaultView.onframe +=
                    delegate
                    {
                        if (this.title == x)
                            return;


                        value();

                        x = title;
                    };

            }

            [Script(DefineAsStatic = true)]
            remove
            {

            }
        }
    }
}
