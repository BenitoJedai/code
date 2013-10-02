using JellyworldExperiment.HardwareDetection.Design;
using JellyworldExperiment.HardwareDetection.HTML.Pages;
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

namespace JellyworldExperiment.HardwareDetection
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {

        public readonly ApplicationSprite sprite = new ApplicationSprite();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            sprite.FoundCamera +=
                delegate
                {
                    Console.WriteLine("FoundCamera");
                };


            sprite.LookingForCamera +=
              delegate
              {
                  Console.WriteLine("LookingForCamera");
              };

            sprite.FoundMutedCamera +=
                  delegate
                  {
                      Console.WriteLine("FoundMutedCamera");
                  };

            sprite.FoundUnmutedCamera +=
              delegate
              {
                  Console.WriteLine("FoundUnmutedCamera");
              };

            // Initialize ApplicationSprite
            sprite.AttachSpriteTo(page.Content);

            sprite.InitializeContent();

            Native.window.ondeviceorientation +=
              eventData =>
              {
                  // works on nexus 7
                  new IHTMLPre { innerText = "ondeviceorientation" }.AttachToDocument();
              };
        }

    }


}
