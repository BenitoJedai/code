using ScriptCoreLib;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace CardGames.source.js.Controls
{
    [Script]
    public class DemoControl
    {
        public const string Alias = "fx.DemoControl";

        IHTMLDiv Control = new IHTMLDiv();

        public IStyle Style { get { return Control.style; } }

        public DemoControl(IHTMLElement e)
        {
            var i = e as IHTMLInput;

            Console.WriteLine(Alias + " created with data : " + i.value);


            e.insertNextSibling(Control);

            Control.innerHTML = "hello world (javascript) : " + i.value;



            Control.onmouseover += ( _) => Style.color = Color.Blue;
            Control.onmouseout += ( _) => Style.color = Color.None;


            Style.cursor = IStyle.CursorEnum.pointer;

        }
    }

  
}
