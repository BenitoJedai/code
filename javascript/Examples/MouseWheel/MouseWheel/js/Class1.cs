//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Query;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

//using global::System.Collections.Generic;



namespace MouseWheel.js
{


    [Script, ScriptApplicationEntryPoint]
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


            // ie7 scrollbar be gone!
            IStyleSheet.Default.AddRule("html").style.overflow = IStyle.OverflowEnum.hidden;
            IStyleSheet.Default.AddRule(".block").style.background = "url(assets/shadow-bottom.png) repeat-x bottom";

            Native.Document.body.style.margin = "0";
            Native.Document.body.style.padding = "0";

            CreateDiv();
            
            var div2 = CreateDiv();
                
            div2.div.style.top = "50%";
            div2.div.style.backgroundColor = Color.Green;

            var div3 = CreateDiv();

            div3.div.style.left = "60%";
            div3.y = 50;

            div3.div.style.top = "20%";
            div3.div.style.height = "60%";
            div3.div.style.width = "30%";
            div3.div.style.backgroundColor = Color.Red;


            div3.div.onmousewheel +=
                delegate(IEvent ev)
                {
                    div3.div.style.left =  (10 + System.Math.Abs(div3.y % 60)) + "%";
                };

            div3.div.appendChild(new IHTMLDiv("You can move this block scrolling your mouse wheel over here."));
        }

        [Script]
        class Tuple
        {
            public IHTMLDiv div;
            public int y;
        }

        private static Tuple CreateDiv()
        {
            var t = new Tuple
                    {
                        y = 0,
                        div = new IHTMLDiv { className = "block" }
                    };

            var info = new IHTMLSpan();

            t.div.appendChild(info);

            t.div.AttachToDocument();

            t.div.onmousewheel +=
                delegate(IEvent e)
                {
                    info.innerHTML = "wheel, " + t.y;
                    info.appendChild(": " + e.WheelDirection);

                    t.y += e.WheelDirection;
                };

            t.div.style.position = IStyle.PositionEnum.absolute;
            t.div.style.height = "50%";
            t.div.style.width = "100%";
            t.div.style.backgroundColor = Color.Yellow;

            info.innerHTML = "scroll here";

            return t;
        }




        static Class1()
        {
            typeof(Class1).SpawnTo(i => new Class1(i));

            ////Console.EnableActiveXConsole();

            //// spawn this class when document is loaded 
            //Native.Spawn(
            //    new Pair<string, EventHandler<IHTMLElement>>(Alias, e => new Class1(e))
            //    );

        }


    }

}
