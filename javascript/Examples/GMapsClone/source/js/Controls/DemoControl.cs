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

namespace GMapsClone.source.js.Controls
{
    [Script]
    public class DemoControl : SpawnControlBase
    {
        // http://www.kmzlinks.com/

        public const string Alias = "fx.DemoControl";

        IHTMLDiv Control = new IHTMLDiv();

        public IStyle Style { get { return Control.style; } }

        void CreateDisposableButton(string e, EventHandler a)
        {


            var btn = new IHTMLButton(e);

            var div = new IHTMLDiv(btn);

            div.style.margin = "1em";

            this.Control.appendChild(div);

            btn.onclick +=
                delegate
                {
                    Control.Dispose();

                    a();
                };
        }

        public DemoControl(IHTMLElement e)
            : base(e)
        {
            Native.Document.body.style.background = "#6591cd url(fx/gfx/editorBg.gif) repeat-x";
            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

            e.insertNextSibling(Control);

            Control.appendChild(new IHTMLElement(IHTMLElement.HTMLElementEnum.h1, "This project demostrates how you could implement basic tiled map functionality with the help of jsc."));

            Control.appendChild("Select a view, after selecting a view you should refresh to be able to select again.");

            CreateDisposableButton("Show Earth",
                delegate { new GoogleMaps("http://kh3.google.com/kh?v=14&t=t", null); }
            );




            CreateDisposableButton("Show Moon",
                delegate { new GoogleMaps("http://moon.google.com/kh?v=2&t=t", null); }
            );

            CreateDisposableButton("Show Mars",
                delegate { new GoogleMaps("http://kh.google.com/movl?ov=52&t=t", null); }
            );


            CreateDisposableButton("Show Earth - extra zoom 1",
                          delegate { new GoogleMaps("http://kh2.google.com/kh?n=404&v=15&t=trtttrsqrsststtqsqtq", null); }
                      );

            CreateDisposableButton("Show Earth - extra zoom 2",
                          delegate { new GoogleMaps("http://kh0.google.com/kh?n=404&v=15&t=tqssrrtrsrtrqssq", null); }
                      );

            CreateDisposableButton("Show Earth - extra zoom 3",
                          delegate { new GoogleMaps("http://kh1.google.com/kh?n=404&v=15&t=tsqqrqsrqtqsttrqtqt", null); }
                      );


            CreateDisposableButton("Show Earth - extra zoom 4",
                          delegate
                          {
                              new GoogleMaps("http://kh0.google.com/kh?n=404&v=15&t=tsqqrqrsrrqtqqrsrs", null) { BaseZoomLevel = 18 };
                          }
                      );
            
           
        }



    }


}
