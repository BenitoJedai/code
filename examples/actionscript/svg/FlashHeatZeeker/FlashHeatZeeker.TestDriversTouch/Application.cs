using FlashHeatZeeker.TestDriversTouch.Design;
using FlashHeatZeeker.TestDriversTouch.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
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

namespace FlashHeatZeeker.TestDriversTouch
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        //W/dalvikvm(23772): JNI WARNING: too many PopLocalFrame calls
        //W/dalvikvm(23772): JNI WARNING: too many PopLocalFrame calls
        //W/dalvikvm(23772): JNI WARNING: too many PopLocalFrame calls
        //W/dalvikvm(23772): JNI WARNING: too many PopLocalFrame calls
        //E/dalvikvm(23772): JNI ERROR (app bug): attempt to use stale local reference 0x1
        //E/dalvikvm(23772): VM aborting
        // AIR 15 crashes on android! wtf???

        public readonly ApplicationSprite sprite = new ApplicationSprite();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            sprite.AttachSpriteToDocument().With(
                   embed =>
                   {
                       embed.style.SetLocation(0, 0);
                       embed.style.SetSize(Native.window.Width, Native.window.Height);

                       Native.window.onresize +=
                           delegate
                           {
                               embed.style.SetSize(Native.window.Width, Native.window.Height);
                           };
                   }
               );
        }

    }


}
