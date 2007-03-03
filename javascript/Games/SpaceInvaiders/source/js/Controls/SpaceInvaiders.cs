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

namespace SpaceInvaiders.source.js.Controls
{
    [Script]
    public class SpaceInvaiders : SpawnControlBase
    {
        public const string Alias = "SpaceInvaiders";

        IHTMLDiv Control = new IHTMLDiv();

        public IStyle Style { get { return Control.style; } }

        // http://www.digitalinsane.com/archives/2007/01/21/space_invaders/

        public SpaceInvaiders(IHTMLElement e)
            : base(e)
        {
            e.insertNextSibling(Control);

            Control.innerHTML = "hello world (javascript) : " + base.SpawnString;

            Control.onmouseover += ( _) => Style.color = Color.Blue;
            Control.onmouseout += ( _) => Style.color = Color.None;

            Style.cursor = IStyle.CursorEnum.pointer;

            var btn = IHTMLButton.Create("go!",
                    delegate
                    {
                        Control.innerHTML = "you clicked me!";

                    }
                );



        }



    }


}
