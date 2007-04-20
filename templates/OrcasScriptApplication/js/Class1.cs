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



namespace OrcasScriptApplication.js
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

            text.onmouseover += e => { text.style.color = Color.Red; };
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


                    var items = users.value.Split(',');


                    foreach (var v in
                        //Sequence.Select(
                        //    Sequence.Where(
                        //        Sequence.Select(
                        //            users.value.Split(',')
                        //            , i => i.Trim()
                        //        ), i => i.ToLower().IndexOf(user_filter) > -1
                        //    ), i => "match: " + i
                        //)

                           from i in items.Select(i => i.Trim())
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


            //SelectManyExample();

            var x = Expando.Of(Native.Document.childNodes);

            Console.WriteLine("childNodes is Array : " + x.IsArray);
            Console.WriteLine("TypeString : " + x.TypeString);
            Console.WriteLine("constructor : " + x.constructor);
            Console.WriteLine("prototype : " + x.prototype);

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
            Console.EnableActiveXConsole();

            // spawn this class when document is loaded 
            Native.Spawn(
                new Pair<string, EventHandler<IHTMLElement>>(Alias, e => new Class1(e))
                );

        }


    }

}
