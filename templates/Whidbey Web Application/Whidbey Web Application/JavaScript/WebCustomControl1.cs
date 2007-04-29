using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;

using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;


namespace Whidbey_Web_Application.JavaScript
{
    [Script]
    public class WebCustomControl1
    {
        public const string Alias = "ui.WebCustomControl1";

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
            Native.Spawn(Alias, delegate(IHTMLElement e) { new WebCustomControl1(e); });
        }

    }
}
