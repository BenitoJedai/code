using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;

using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;


namespace OrcasWebApplication.JavaScript
{
    [Script]
    public class WebCustomControl1
    {

        public WebCustomControl1(IHTMLElement e)
        {
            e.style.color = Color.Red;

            e.onmouseover += delegate { e.style.backgroundColor = Color.Yellow; };
            e.onmouseout += delegate { e.style.backgroundColor = Color.None; };

            e.onclick += delegate
            {
                e.innerHTML = "this content has changed at " + IDate.Now;
            };
        }

        static WebCustomControl1()
        {
            typeof(WebCustomControl1).SpawnTo(e => new WebCustomControl1(e));
        }

    }
}
