using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

using global::System.Collections.Generic;

namespace OrcasScriptApplication.js
{
    [Script]
    internal static class SequenceWrapper
    {
        // LINQ may 2006 CTP seems to have some issues if the extension class
        // is inside another assembly which is not the System.Query.dll
        //
        // it seems to be the same for March CTP 2007, i wonder why
        //
        // current version of jsc cannot provide IEnumerable<T> implementation
        // for an array like T[], use method overloading as a workaround

        public static IEnumerable<S> Select<T, S>(this T[] source, Func<T, S> selector)
        {
            return ScriptCoreLib.Shared.Query.Sequence.Select(source, selector);
        }

        public static IEnumerable<S> Select<T, S>(this IEnumerable<T> source, Func<T, S> selector)
        {
            return ScriptCoreLib.Shared.Query.Sequence.Select(source, selector);
        }

        public static IEnumerable<T> Where<T>(this T[] source, Func<T, bool> predicate)
        {
            return ScriptCoreLib.Shared.Query.Sequence.Where(source, predicate);
        }

        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return ScriptCoreLib.Shared.Query.Sequence.Where(source, predicate);
        }

        /* is missing from current query dll
         * 
        public static int Count<T>(this T[] source)
        {
            return ScriptCoreLib.Shared.Query.Sequence.Count(source);
        }*/

        public static int Count<T>(this IEnumerable<T> source)
        {
            return ScriptCoreLib.Shared.Query.Sequence.Count(source);
        }

    }

    [Script]
    public class Class1
    {
        public const string Alias = "Class1";
        public const string DefaultData = "Class1Data";

        /// <summary>
        /// Creates a new control
        /// </summary>
        /// <param name="DataElement">The hidden data element</param>
        public Class1(IHTMLElement DataElement)
        {
            // this ctor creates a new div which has a text and a button element
            // on mouseover over the color text is changed
            // on pressing the button the next message in text element is displayed


            var btn = new IHTMLButton("Toggle message");

            var index = 0;
            var messages = new[] {
                               "1. Hello world!",
                               "2. This control is powered by jsc",
                               "3. Visit the project at <a href='http://jsc.sf.net'>jsc.sf.net</a>",
                               "4. This is the last message"
                      };

            var text = new IHTMLDiv(Alias + " created");


            IHTMLDiv Control = new IHTMLDiv();

            Control.appendChild(text, btn);

            DataElement.insertNextSibling(
                Control
         
            );

            text.onmouseover += e => text.style.color = Color.Red;
            text.onmouseout += e => text.style.color = Color.None;

            btn.onclick +=
                delegate
                {
                    text.innerHTML = messages[index % messages.Length];

                    index++;
                };


            #region linq example
            

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
                 br(),
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

            #endregion

        }

        static Class1()
        {
            // spawn this class when document is loaded 
            Native.Spawn(
                new Pair<string, EventHandler<IHTMLElement>>(Alias, e => new Class1(e))
                );
        }
    }
}
