using ScriptCoreLib;

using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
//using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace ButterFly.source.js.Controls
{
    [Script]
    public class DemoControl : SpawnControlBase
    {
        public const string Alias = "fx.DemoControl";

        IHTMLDiv Control = new IHTMLDiv();

        public IStyle Style { get { return Control.style; } }

        public DemoControl(IHTMLElement e)
            : base(e)
        {
            e.insertNextSibling(Control);

            Control.appendChild("There is a buttrfly under your mouse. Can you see it? :)");

            Butterfly.Spawn(Control);
 

        }



    }


}
