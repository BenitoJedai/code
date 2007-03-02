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

using global::System.Collections.Generic;

namespace LinqToObjects.source.js.Controls
{
    using Query;

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

            var a = new [] { "_a", "_x", "cc" };



            foreach (var v in 
                from i in a 
                where i.StartsWith("_")
                select "select: " + i)
            {
                Control.appendChild(new IHTMLDiv(v));
            }

        }



    }


}
