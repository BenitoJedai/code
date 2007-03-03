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
    using ScriptCoreLib.JavaScript.Query;

    [Script]
    internal static class SequenceWrapper 
    {
        // LINQ may 2006 CTP seems to have some issues if the extension class
        // is inside another assembly which is not the System.Query.dll

        public static IEnumerable<S> Select<T, S>(this T[] source, Func<T, S> selector)
        {
            return ScriptCoreLib.JavaScript.Query.Sequence.Select(source, selector);
        }

        public static IEnumerable<S> Select<T, S>(this IEnumerable<T> source, Func<T, S> selector)
        {
            return ScriptCoreLib.JavaScript.Query.Sequence.Select(source, selector);
        }

        public static IEnumerable<T> Where<T>(this T[] source, Func<T, bool> predicate)
        {
            return ScriptCoreLib.JavaScript.Query.Sequence.Where(source, predicate);
        }

        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return ScriptCoreLib.JavaScript.Query.Sequence.Where(source, predicate);
        }
    }

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
                    var user_filter = filter.value.Trim().ToLower();

                    result.removeChildren();

                    foreach (var v in
                        //Sequence.Select(
                        //    Sequence.Where(
                        //        Sequence.Select(
                        //            users.value.Split(',')
                        //            , i => i.Trim()
                        //        ), i => i.ToLower().IndexOf(user_filter) > -1
                        //    ), i => "match: " + i
                        //)

                           from i in users.value.Split(',').Select(i => i.Trim())
                           where i.ToLower().IndexOf(user_filter) > -1
                           select "match: " + i
                    )

                    {
                        result.appendChild(new IHTMLDiv(v));
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
                new IHTMLLabel("Found matches:", result),
                br(),
                result
            );

            Update();

        }



    }


}
