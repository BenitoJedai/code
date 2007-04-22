using ScriptCoreLib;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace CardGames.source.js.Controls
{
    [Script]
    public class SoundTest : SpawnControlBase
    {
        public const string Alias = "fx.SoundTest";

        IHTMLButton Control = new IHTMLButton("Play sound");


        public SoundTest(IHTMLElement e) 
            : base(e)
        {
            Native.Document.body.appendChild(Control);

           

               Control.style.color = Color.Red;

               Control.onmouseup += delegate (IEvent ev)
               {
                   TextWriter w = new TextWriter();

                   w.WriteLine("button " + ev.MouseButton);

                   Native.Window.alert(w.Text);

                   Native.PlaySound("fx/sounds/hint.wav");


               };
            
            
           
        }
    }

  
}
