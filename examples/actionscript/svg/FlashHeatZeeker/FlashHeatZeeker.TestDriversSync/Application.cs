using FlashHeatZeeker.TestDriversSync.Design;
using FlashHeatZeeker.TestDriversSync.HTML.Pages;
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

namespace FlashHeatZeeker.TestDriversSync
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();


        ApplicationSprite leftsprite = new ApplicationSprite();
        ApplicationSprite uppersprite = new ApplicationSprite();
        ApplicationSprite lowersprite = new ApplicationSprite();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {


            InitializeSprites();


            InitializeTransport();


        }

        private void InitializeTransport()
        {
            // Console.WriteLine("leftsprite.__transport_out");
            leftsprite.__transport_out +=
                xml =>
                {
                    uppersprite.__transport_in(xml);
                    lowersprite.__transport_in_fakelag(xml);
                };

            uppersprite.__transport_out +=
                xml =>
                {
                    leftsprite.__transport_in(xml);
                    lowersprite.__transport_in_fakelag(xml);

                };

            lowersprite.__transport_out +=
           xml =>
           {
               leftsprite.__transport_in_fakelag(xml);
               uppersprite.__transport_in_fakelag(xml);

           };


        }

        private void InitializeSprites()
        {
            {

                leftsprite.AttachSpriteToDocument().With(
                       embed =>
                       {
                           embed.style.SetLocation(0, 0);
                           embed.style.SetSize(Native.window.Width / 2 - 1, Native.window.Height);

                           Native.window.onresize +=
                               delegate
                               {
                                   embed.style.SetSize(Native.window.Width / 2 - 1, Native.window.Height);
                               };
                       }
                   );
            }

            {


                uppersprite.AttachSpriteToDocument().With(
                       embed =>
                       {
                           embed.style.SetLocation(Native.window.Width / 2, 0);
                           embed.style.SetSize(Native.window.Width / 2, Native.window.Height / 2 - 1);

                           Native.window.onresize +=
                               delegate
                               {
                                   embed.style.SetLocation(Native.window.Width / 2, 0);
                                   embed.style.SetSize(Native.window.Width / 2, Native.window.Height / 2 - 1);
                               };
                       }
                   );
            }

            {

                lowersprite.AttachSpriteToDocument().With(
                       embed =>
                       {
                           embed.style.SetLocation(Native.window.Width / 2, Native.window.Height / 2);
                           embed.style.SetSize(Native.window.Width / 2, Native.window.Height / 2);

                           Native.window.onresize +=
                               delegate
                               {
                                   embed.style.SetLocation(Native.window.Width / 2, Native.window.Height / 2);
                                   embed.style.SetSize(Native.window.Width / 2, Native.window.Height / 2);
                               };
                       }
                   );
            }
        }

    }



}
