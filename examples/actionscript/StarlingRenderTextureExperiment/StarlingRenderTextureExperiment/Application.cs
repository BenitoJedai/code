using StarlingRenderTextureExperiment.Design;
using StarlingRenderTextureExperiment.HTML.Pages;
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

namespace StarlingRenderTextureExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {

        public readonly ApplicationSprite sprite = new ApplicationSprite();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //Error	4	The call is ambiguous between the following methods or properties: 'ScriptCoreLib.JavaScript.Extensions.SpriteExtensions.ToHTMLElement(ScriptCoreLib.ActionScript.flash.display.Sprite)' and 'ScriptCoreLib.JavaScript.Extensions.AppletExtensions.ToHTMLElement(java.applet.Applet)'	X:\jsc.svn\examples\actionscript\StarlingRenderTextureExperiment\StarlingRenderTextureExperiment\Application.cs	33	25	StarlingRenderTextureExperiment


            var embed = SpriteExtensions.ToHTMLElement(sprite);

            embed.style.SetLocation(0, 0);
            embed.style.SetSize(Native.window.Width, Native.window.Height);

            Native.window.onresize +=
                delegate
                {
                    embed.style.SetSize(Native.window.Width, Native.window.Height);
                };

            sprite.AttachSpriteToDocument();
        }

    }



}
