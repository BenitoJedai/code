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


            var users = new IHTMLTextArea("neo, morpheous, trinity, Agent Smith");

            users.rows = 10;

            var filter = new IHTMLInput(HTMLInputTypeEnum.text, "smith");
            var result = new IHTMLDiv();

            EventHandler Update =
                delegate
                {
                    var user_names = users.value.Split(',').Select(i => i.Trim());
                    var user_filter = filter.value.Trim().ToLower();

                    result.removeChildren();

                    foreach (var v in
                        from i in user_names
                        where i.ToLower().IndexOf(user_filter) > -1
                        select "match: " + i)
                    {
                        result.appendChild(new IHTMLDiv(v));
                    }
                };

            users.onchange += delegate { Update(); };
            users.onkeyup += delegate { Update(); };

            filter.onchange += delegate { Update(); };
            filter.onkeyup += delegate { Update(); };

            Control.appendChild(
                users,
                new IHTMLBreak(),
                filter,
                new IHTMLBreak(),
                result
            );

            Update();

        }



    }


}
