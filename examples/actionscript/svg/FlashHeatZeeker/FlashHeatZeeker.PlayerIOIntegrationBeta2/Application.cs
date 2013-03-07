using Abstractatech.ConsoleFormPackage.Library;
using FlashHeatZeeker.PlayerIOIntegrationBeta2.Design;
using FlashHeatZeeker.PlayerIOIntegrationBeta2.HTML.Pages;
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
using Abstractatech.JavaScript.FormAsPopup;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlashHeatZeeker.PlayerIOIntegrationBeta2
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // crx
        // https://chrome.google.com/webstore/detail/operation-heat-zeeker/iiabebggdceojiejhopnopmbkgandhha?hl=et&utm_source=chrome-ntp-launcher
        // use non chrome to download without install
        // http://clients2.google.com/service/update2/crx?response=redirect&x=id%3Diiabebggdceojiejhopnopmbkgandhha%26uc%26lang%3Den-US&prod=chrome
        // Download interrupted
        // means servers are not yet in sync, wait some
        // pbaaphpbkehboammnlcihpemkkimdfgo
        // http://clients2.google.com/service/update2/crx?response=redirect&x=id%3Dpbaaphpbkehboammnlcihpemkkimdfgo%26uc%26lang%3Den-US&prod=chrome
        // error-unknownApplication
        // Error 404

        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationSprite sprite = new ApplicationSprite();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
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

#if DEBUG
            var con = new ConsoleForm();

            con.InitializeConsoleFormWriter();

            con.Show();

            con.Left = Native.Window.Width - con.Width;
            con.Top = 0;

            Native.Window.onresize +=
                  delegate
                  {
                      con.Left = Native.Window.Width - con.Width;
                      con.Top = 0;
                  };


            con.Opacity = 0.6;

            
            sprite.InitializeConsoleFormWriter(
                       Console.Write,
                       Console.WriteLine
            );

            con.HandleFormClosing = false;
            con.PopupInsteadOfClosing();
#endif
            "Operation «Heat Zeeker»".ToDocumentTitle();


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
