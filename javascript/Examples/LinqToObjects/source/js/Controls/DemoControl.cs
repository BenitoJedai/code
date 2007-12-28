using ScriptCoreLib;

using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
//using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Drawing;

using System.Collections.Generic;
using System.Linq;
using System;

namespace LinqToObjects.source.js.Controls
{
    [Script, ScriptApplicationEntryPoint]
    public class DemoControl 
    {

        IHTMLDiv Control = new IHTMLDiv();


        public DemoControl(IHTMLElement e)
        {
            e.insertNextSibling(Control);


            var users = new IHTMLTextArea("_martin, mike, mac, ken, neo, zen, jay, morpheous, trinity, Agent Smith, _psycho");

            users.rows = 10;

            var filter = new IHTMLInput(HTMLInputTypeEnum.text, "or");
            var filter2 = new IHTMLInput(HTMLInputTypeEnum.text, "a");
            var result = new IHTMLDiv();


            Action Update =
                delegate
                {
                    var user_filter = filter.value.Trim().ToLower();
                    var user_filter2 = filter2.value.Trim().ToLower();

                    result.removeChildren();

                    var __users = users.value.Split(',');


                    var query = from i in __users
                                where i.ToLower().Contains(user_filter) 
                                let name = i.Trim()
                                let isspecial = i.ToLower().Contains(user_filter2)
                                orderby isspecial ascending, name.Length descending, name 
                                select new { isspecial, length = name.Length, name };

                    foreach (var v in query)
                    {
                        var m = new IHTMLDiv("match: " + v).AttachTo(result);

                        if (v.isspecial)
                            m.style.color = Color.Blue;
                    }
              
                };

            users.onchange += delegate { Update(); };
            users.onkeyup += delegate { Update(); };

            filter.onchange += delegate { Update(); };
            filter.onkeyup += delegate { Update(); };

            Func<IHTMLBreak> br = () => new IHTMLBreak();

            Control.appendChild(
                new IHTMLLabel("Enter a list of names separated by commas", users),
                br(),
                users,
                br(),

                new IHTMLLabel("Enter a partial name to be found from the list above.", filter),
                br(),
                filter,
                br(),

                new IHTMLLabel("Enter a partial name to make the entry special", filter2),
                br(),
                filter2,

                br(),
                new IHTMLLabel("Found matches:", result),
                br(),
                result
            );

            Update();

        }


        static DemoControl()
        {
            typeof(DemoControl).SpawnTo(i => new DemoControl(i));
        }
    }


}
