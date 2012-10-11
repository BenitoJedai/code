using ScriptCoreLib;

using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
//using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;
using System.Linq;
using HulaGirl.HTML.Images.FromAssets;

namespace HulaGirl.source.js.Controls
{
    public class HulaGirl
    {

        IHTMLDiv Control = new IHTMLDiv();

        public IStyle Style { get { return Control.style; } }

        public HulaGirl(IHTMLElement e)
        {
            e.insertNextSibling(Control);

            Control.innerHTML = "hello world (javascript) : " /* base.SpawnString*/;

            Control.onmouseover += delegate { Style.color = Color.Blue; };
            Control.onmouseout += delegate { Style.color = Color.None; };

            Style.cursor = IStyle.CursorEnum.pointer;

            var btn = IHTMLButton.Create("go!",
                    delegate
                    {
                        Control.innerHTML = "you clicked me!";

                    }
                );








            var img = Frames[52];

            img.AttachToDocument();

            var _width = 120;
            var _height = 100;
            var _zoom = 1.0;

            Native.Document.body.onmousewheel +=
                ev =>
                {
                    _zoom += 0.1 * ev.WheelDirection;

                    img.style.width = (_width * _zoom) + "px";
                    img.style.height = (_height * _zoom) + "px";
                };

            var index = 0;

            new Timer(
                delegate
                {

                    index++;

                    if (index >= Frames.Length)
                        index = 0;

                    img.Orphanize();
                    img = Frames[index].AttachToDocument();
                    img.style.width = (_width * _zoom) + "px";
                    img.style.height = (_height * _zoom) + "px";

                }, 0, 1000 / 24);



        }


        static IHTMLImage[] Frames = new HTML.Pages.FramesImages().Images;


    }



}
