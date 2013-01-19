using StarlingTextureScrollExperiment.Design;
using StarlingTextureScrollExperiment.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.ActionScript.flash.display;

namespace StarlingTextureScrollExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationSprite sprite = new ApplicationSprite();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //Error	4	The call is ambiguous between the following methods or properties: 'ScriptCoreLib.JavaScript.Extensions.SpriteExtensions.ToHTMLElement(ScriptCoreLib.ActionScript.flash.display.Sprite)' and 'ScriptCoreLib.JavaScript.Extensions.AppletExtensions.ToHTMLElement(java.applet.Applet)'	X:\jsc.svn\examples\actionscript\StarlingTextureScrollExperiment\StarlingTextureScrollExperiment\Application.cs	33	25	StarlingTextureScrollExperiment


            var embed = SpriteExtensions.ToHTMLElement(sprite);


            embed.wmode();

            embed.style.SetLocation(0, 0);
            embed.style.SetSize(Native.Window.Width, Native.Window.Height);

            Native.Window.onresize +=
                delegate
                {
                    embed.style.SetSize(Native.Window.Width, Native.Window.Height);
                };

            sprite.AttachSpriteToDocument();
        }

    }


    public static class XX
    {
        public static void wmode(this IHTMLElement x, string value = "direct")
        {

            var p = x.parentNode;


            x.setAttribute("wmode", value);


        }
    }
}
