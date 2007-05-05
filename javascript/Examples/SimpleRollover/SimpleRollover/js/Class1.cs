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



namespace SimpleRollover.js
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

            Native.Document.body.style.backgroundColor = Color.Gray;


     

            Action<IHTMLImage, IHTMLImage> Spawn = (j1, j2) =>
                j1.InvokeOnComplete((_j1) => j2.InvokeOnComplete((_j2)
                     =>
                     {
                         var x = new IHTMLImage(_j1.src);

                         x.onmouseover += delegate { x.src = _j2.src; };
                         x.onmouseout += delegate { x.src = _j1.src; };

                         new IHTMLElement(IHTMLElement.HTMLElementEnum.center, x).attachToDocument();
                     }
                ));

            Spawn("assets/Untitled-1_03.png", "assets/Untitled-2_03.png");
            Spawn("assets/Untitled-1_07.png", "assets/Untitled-2_07.png");
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
