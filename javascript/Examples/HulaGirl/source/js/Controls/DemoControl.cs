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
using System.Linq;

namespace HulaGirl.source.js.Controls
{
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

            Control.innerHTML = "hello world (javascript) : " + base.SpawnString;

            Control.onmouseover += delegate { Style.color = Color.Blue; };
            Control.onmouseout += delegate { Style.color = Color.None; };

            Style.cursor = IStyle.CursorEnum.pointer;

            var btn = IHTMLButton.Create("go!",
                    delegate
                    {
                        Control.innerHTML = "you clicked me!";

                    }
                );

            System.Func<int, string> GetFilename = i => "hula_girl_" + ("" + i).PadLeft(2, '0') + ".png";

            
            for (int i = 1; i <= 82; i++)
            {
                Console.WriteLine(("" + i).PadLeft(2, '0'));
            }


            



            var img = new IHTMLImage("gfx_hula_girl_100/" + GetFilename(52));
                
                img.attachToDocument();

            var _width = 120;
            var _zoom = 1.0;

            img.onmousewheel += 
                ev =>
                    {
                        _zoom += 0.1 * ev.WheelDirection;
                        img.style.width = (_width * _zoom) + "px";
                    };

            var index = 1;

            new Timer(
                delegate
                {

                    index++;

                    if (index > 82)
                        index = 1;


                    img.src = "gfx_hula_girl_100/" + GetFilename(index);

                }, 0, 1000 / 24);
                


        }



    }


}
