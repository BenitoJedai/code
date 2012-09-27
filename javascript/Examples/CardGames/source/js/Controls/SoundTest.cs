using ScriptCoreLib;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace CardGames.source.js.Controls
{
    [Script, ScriptApplicationEntryPoint]
    public class SoundTest //: SpawnControlBase
    {
        //public const string Alias = "fx.SoundTest";

        IHTMLButton Control = new IHTMLButton("Play sound");

        static SoundTest()
        {
            typeof(SoundTest).SpawnTo(i => new SoundTest(i));
        }


        public SoundTest(IHTMLElement e)
        //: base(e)
        {
            Native.Document.body.appendChild(Control);



            Control.style.color = Color.Red;

            Control.onmouseup += delegate(IEvent ev)
            {
                var w = new System.Text.StringBuilder();

                w.AppendLine("mousebutton: " + ev.MouseButton);

                Native.Window.alert(w.ToString());

                Native.PlaySound("assets/CardGames/sounds/hint.wav");


            };



        }
    }


}
