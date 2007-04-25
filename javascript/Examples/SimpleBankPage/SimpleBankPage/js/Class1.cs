using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Query;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

using global::System.Collections.Generic;

namespace SimpleBankPage.js
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


           

            IHTMLDiv Control = new IHTMLDiv();


            DataElement.insertNextSibling(
                Control

            );

            Control.appendChild(new IHTMLElement(IHTMLElement.HTMLElementEnum.h1, "This page will ask you to confirm in order to unload the page"));



            Native.Window.onbeforeunload +=
                delegate (IWindow.Confirmation ev)
                {

                    Timer.DoAsync(
                        delegate
                        {
                            Native.Document.body.style.backgroundColor = Color.Red;


                            new Timer((t) => Native.Document.body.style.backgroundColor = Color.White, 500, 0);
                        }
                    );

                    ev.Text = "This is a sequre website, do you want to leave?";
                };

            var anchor = new IHTMLAnchor("http://example.com", "example.com");

            anchor.target = "_self";

            Control.appendChild(anchor);


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
