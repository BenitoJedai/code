using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using FlashHeatZeekerReferenced.Design;
using FlashHeatZeekerReferenced.HTML.Pages;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript;

namespace FlashHeatZeekerReferenced
{
    [SWF(frameRate = 60)]
    public sealed class ApplicationSprite : global::FlashHeatZeekerWithStarlingT26.ApplicationSpriteContent
    {
        public ApplicationSprite()
        {
            //var content = new global::FlashHeatZeekerWithStarlingT26.ApplicationSpriteContent();


            //this.addChild(content);
        }

    }

    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();


        public ApplicationSprite sprite = new ApplicationSprite();


        public Application(IApp page)
        {

            sprite.wmode();

            sprite.AttachSpriteToDocument().With(
                   embed =>
                   {
                       embed.style.SetLocation(0, 0);
                       embed.style.SetSize(Native.Window.Width, Native.Window.Height);

                       Native.Window.onresize +=
                           delegate
                           {
                               embed.style.SetSize(Native.Window.Width, Native.Window.Height);
                           };
                   }
               );

        }

    }

    public static class XX
    {
        public static void wmode(this Sprite s, string value = "direct")
        {
            var x = s.ToHTMLElement();

            var p = x.parentNode;
            if (p != null)
            {
                // if we continue, element will be reloaded!
                return;
            }

            x.setAttribute("wmode", value);


        }
    }
}
