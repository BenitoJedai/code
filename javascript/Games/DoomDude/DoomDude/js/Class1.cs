//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Query;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

//using global::System.Collections.Generic;



namespace DoomDude.js
{


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

            var x = new
                    {
                        about = "this is an anonymous type",
                        pos = new
                        {
                            x = 8,
                            y = 9
                        }
                    };

            Console.WriteLine(x.about);
            Console.WriteLine(x.ToString());


            IHTMLDiv Control = new IHTMLDiv();


            DataElement.insertNextSibling(
                Control

            );


            #region linq example

            // http://www.imdb.com/title/tt0133093/

            var users1 = new IHTMLTextArea("neo, morpheous, trinity, Agent Smith");
            var users2 = new IHTMLTextArea("Rhineheart, Agent Jones, Agent Brown, Dozer, Switch, Mouse");
            var users3 = new IHTMLTextArea("Apoc, Tank, Cypher, Oracle");

            users1.rows = 10;
            users2.rows = 10;
            users3.rows = 10;

            var filter = new IHTMLInput(HTMLInputTypeEnum.text, "smith");
            var result = new IHTMLElement(IHTMLElement.HTMLElementEnum.ol);




            Func<IHTMLTextArea, string[]> ToNames = i =>
            {
                var u = new[] { ',' };

                // jsc cannot handle the params attribute that well :)

                return i.value.Split(u);
            };


            EventHandler Update =
                delegate
                {
                    var user_filter = filter.value.Trim().ToLower();

                    result.removeChildren();



                    var items = ToNames(users1)
                        .Concat(ToNames(users2))
                        .Concat(ToNames(users3));


                    foreach (var v in
                           from i in items.Select(i => i.Trim())
                           where i.ToLower().IndexOf(user_filter) > -1
                           let xlength = i.Length
                           select new { i, xlength }
                    )
                    {
                        var item = new IHTMLElement(IHTMLElement.HTMLElementEnum.li,
                                       new IHTMLAnchor(
                                           "http://www.imdb.com/Find?select=Characters&for=" + Native.Window.escape(v.i),
                                           "imdb: " + v.i + "(length: " + v.xlength + ")"));

                        result.appendChild(item);
                    }
                };

            users1.onchange += delegate { Update(); };
            users1.onkeyup += delegate { Update(); };

            users2.onchange += delegate { Update(); };
            users2.onkeyup += delegate { Update(); };

            users3.onchange += delegate { Update(); };
            users3.onkeyup += delegate { Update(); };

            filter.onchange += delegate { Update(); };
            filter.onkeyup += delegate { Update(); };

            Func<IHTMLBreak> br = () => new IHTMLBreak();
            Func<string, IHTMLElement> h1 = (i) => new IHTMLElement(IHTMLElement.HTMLElementEnum.h1, i);
            Func<string, IHTMLElement> paragraph = (i) => new IHTMLElement(IHTMLElement.HTMLElementEnum.p, i);

            Control.appendChild(
                h1("linq to objects"),
                paragraph(
                    @"This example demostrates the use of 'linq to objects' within jsc:javascript.
The actual source code is in c#. It is compiled to an assembly. Then it is recompiled into javascript.
This example makes heavy use of delegates, dom, and query operators.
                "),
                 new IHTMLAnchor("http://jsc.svn.sourceforge.net/viewvc/jsc/templates/DoomDude/js/Class1.cs?view=markup", "view source"),
                 br(),
                new IHTMLLabel("Enter a list of names separated by commas"),
                br(),
                users1, users2, users3,
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


            //SelectManyExample();


            var DOMdump = Lambadas.Y<int, INode>(
                    f =>
                        (int indent, INode target) =>
                        {
                            for (int i = 0; i < indent; i++)
                            {
                                Console.Write("\t");
                            }

                            Console.WriteLine(target.nodeName);


                            foreach (INode n in target.childNodes.Where(i => i.nodeType == INode.NodeTypeEnum.ElementNode))
                            {



                                f(indent + 1, n);
                            }

                        }
                ).FixFirstParam(0);



            DOMdump(Native.Document);

            var doc = new IXMLDocument("mytag");

            doc.documentElement.appendChild("hello world");
            doc.documentElement.appendChild(new IXMLElement(doc, "yoda", "whut"));
            doc.documentElement.appendChild(new IXMLElement(doc, "yoda", "whut"));
            doc.documentElement.appendChild(new IXMLElement(doc, "yoda", "whut"));



            Console.WriteLine("dynamic xml: ");

            DOMdump(doc);

            Console.WriteLine(doc.ToXMLString());

            // not array
            // is object
            // no prototype
        }




        static Class1()
        {
            //Console.EnableActiveXConsole();

            // spawn this class when document is loaded 
            Native.Spawn(
                new Pair<string, EventHandler<IHTMLElement>>(Alias, e => new Class1(e))
                );

        }


    }

}
