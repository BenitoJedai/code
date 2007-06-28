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



namespace MouseWheel.js
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
            // based on http://adomas.org/javascript-mouse-wheel/test.html

            IStyleSheet.Default.AddRule("html").style.overflow = IStyle.OverflowEnum.hidden;

            Native.Document.body.style.margin = "0";
            Native.Document.body.style.padding = "0";

            CreateDiv();
            
            var div2 = CreateDiv();
                
            div2.style.top = "50%";
            div2.style.backgroundColor = Color.Green;

            var div3 = CreateDiv();

            div3.style.left = "60%";
            div3.style.top = "20%";
            div3.style.height = "60%";
            div3.style.width = "30%";
            div3.style.backgroundColor = Color.Red;

        }

        private static IHTMLDiv CreateDiv()
        {
            int y = 0;

            var div = new IHTMLDiv();

            div.attachToDocument();

            div.onmousewheel +=
                delegate(IEvent e)
                {
                    div.innerHTML = "wheel, " + y;
                    div.appendChild(": " + e.WheelDirection);

                    y += e.WheelDirection;
                };

            div.style.position = IStyle.PositionEnum.absolute;
            div.style.height = "50%";
            div.style.width = "100%";
            div.style.backgroundColor = Color.Yellow;

            div.innerHTML = "scroll here";

            return div;
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
