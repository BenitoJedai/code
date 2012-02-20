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

            var index = 1;

            new Timer(
                delegate
                {

                    index++;

                    if (index > 82)
                        index = 1;

                    img.Orphanize();
                    img = Frames[index].AttachToDocument();
                    img.style.width = (_width * _zoom) + "px";
                    img.style.height = (_height * _zoom) + "px";

                }, 0, 1000 / 24);



        }


        static IHTMLImage[] Frames = new[]
        {
            null,
            (IHTMLImage)new hula_girl_01(),
            (IHTMLImage)new hula_girl_02(),
            (IHTMLImage)new hula_girl_03(),
            (IHTMLImage)new hula_girl_04(),
            (IHTMLImage)new hula_girl_05(),
            (IHTMLImage)new hula_girl_06(),
            (IHTMLImage)new hula_girl_07(),
            (IHTMLImage)new hula_girl_08(),
            (IHTMLImage)new hula_girl_09(),
            (IHTMLImage)new hula_girl_10(),
            (IHTMLImage)new hula_girl_11(),
            (IHTMLImage)new hula_girl_12(),
            (IHTMLImage)new hula_girl_13(),
            (IHTMLImage)new hula_girl_14(),
            (IHTMLImage)new hula_girl_15(),
            (IHTMLImage)new hula_girl_16(),
            (IHTMLImage)new hula_girl_17(),
            (IHTMLImage)new hula_girl_18(),
            (IHTMLImage)new hula_girl_19(),
            (IHTMLImage)new hula_girl_20(),
            (IHTMLImage)new hula_girl_21(),
            (IHTMLImage)new hula_girl_22(),
            (IHTMLImage)new hula_girl_23(),
            (IHTMLImage)new hula_girl_24(),
            (IHTMLImage)new hula_girl_25(),
            (IHTMLImage)new hula_girl_26(),
            (IHTMLImage)new hula_girl_27(),
            (IHTMLImage)new hula_girl_28(),
            (IHTMLImage)new hula_girl_29(),
            (IHTMLImage)new hula_girl_30(),
            (IHTMLImage)new hula_girl_31(),
            (IHTMLImage)new hula_girl_32(),
            (IHTMLImage)new hula_girl_33(),
            (IHTMLImage)new hula_girl_34(),
            (IHTMLImage)new hula_girl_35(),
            (IHTMLImage)new hula_girl_36(),
            (IHTMLImage)new hula_girl_37(),
            (IHTMLImage)new hula_girl_38(),
            (IHTMLImage)new hula_girl_39(),
            (IHTMLImage)new hula_girl_40(),
            (IHTMLImage)new hula_girl_41(),
            (IHTMLImage)new hula_girl_42(),
            (IHTMLImage)new hula_girl_43(),
            (IHTMLImage)new hula_girl_44(),
            (IHTMLImage)new hula_girl_45(),
            (IHTMLImage)new hula_girl_46(),
            (IHTMLImage)new hula_girl_47(),
            (IHTMLImage)new hula_girl_48(),
            (IHTMLImage)new hula_girl_49(),
            (IHTMLImage)new hula_girl_50(),
            (IHTMLImage)new hula_girl_51(),
            (IHTMLImage)new hula_girl_52(),
            (IHTMLImage)new hula_girl_53(),
            (IHTMLImage)new hula_girl_54(),
            (IHTMLImage)new hula_girl_55(),
            (IHTMLImage)new hula_girl_56(),
            (IHTMLImage)new hula_girl_57(),
            (IHTMLImage)new hula_girl_58(),
            (IHTMLImage)new hula_girl_59(),
            (IHTMLImage)new hula_girl_60(),
            (IHTMLImage)new hula_girl_61(),
            (IHTMLImage)new hula_girl_62(),
            (IHTMLImage)new hula_girl_63(),
            (IHTMLImage)new hula_girl_64(),
            (IHTMLImage)new hula_girl_65(),
            (IHTMLImage)new hula_girl_66(),
            (IHTMLImage)new hula_girl_67(),
            (IHTMLImage)new hula_girl_68(),
            (IHTMLImage)new hula_girl_69(),
            (IHTMLImage)new hula_girl_70(),
            (IHTMLImage)new hula_girl_71(),
            (IHTMLImage)new hula_girl_72(),
            (IHTMLImage)new hula_girl_73(),
            (IHTMLImage)new hula_girl_74(),
            (IHTMLImage)new hula_girl_75(),
            (IHTMLImage)new hula_girl_76(),
            (IHTMLImage)new hula_girl_77(),
            (IHTMLImage)new hula_girl_78(),
            (IHTMLImage)new hula_girl_79(),
            (IHTMLImage)new hula_girl_80(),
            (IHTMLImage)new hula_girl_81(),
            (IHTMLImage)new hula_girl_82(),


        };

    }



}
