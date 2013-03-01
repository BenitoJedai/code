using Abstractatech.ConsoleFormPackage.Library;
using FlashHeatZeeker.UnitPedSync.Design;
using FlashHeatZeeker.UnitPedSync.HTML.Pages;
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
using System.Windows.Forms;
using Abstractatech.JavaScript.FormAsPopup;

namespace FlashHeatZeeker.UnitPedSync
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

            InitializeConsole();

            InitializeTransport();


        }

        private void InitializeTransport()
        {
            Console.WriteLine("leftsprite.__transport_out");
            leftsprite.__transport_out +=
                xml =>
                {
                    //Console.WriteLine(new { xml });

                    uppersprite.__transport_in(xml);

                };

            Console.WriteLine("before WhenReady");
            leftsprite.WhenReady(
                delegate
                {


                    var __xml = new XElement("check", new XAttribute("bugfix", "bugfix"));
                    var __xmlstring = __xml.ToString();

                    Console.WriteLine(new { __xmlstring });

                    leftsprite.__raise_transport_out(__xmlstring);
                    Console.WriteLine("after __raise_transport_out");
                }
            );
        }

        private void InitializeConsole()
        {
            #region con
            var con = new ConsoleForm();

            con.InitializeConsoleFormWriter();

            con.Show();

            con.Height = 150;
            con.Left = Native.Window.Width - con.Width;
            con.Top = 0;

            Native.Window.onresize +=
                  delegate
                  {
                      con.Left = Native.Window.Width - con.Width;
                      con.Top = 0;
                  };


            con.Opacity = 0.6;




            // !! not compatible yet
            //FormAsPopupExtensions
            con.HandleFormClosing = false;
            con.PopupInsteadOfClosing();
            #endregion

            Action<string> Console_Write =
               x =>
               {
                   Console.Write(x);
               };


            Action<string> Console_WriteLine =
               x =>
               {
                   Console.WriteLine(x);
               };

            leftsprite.InitializeConsoleFormWriter(
              Console_Write, Console_WriteLine
           );
        }

        private void InitializeSprites()
        {
            {
                leftsprite.wmode();

                leftsprite.AttachSpriteToDocument().With(
                       embed =>
                       {
                           embed.style.SetLocation(0, 0);
                           embed.style.SetSize(Native.Window.Width / 2 - 1, Native.Window.Height);

                           Native.Window.onresize +=
                               delegate
                               {
                                   embed.style.SetSize(Native.Window.Width / 2 - 1, Native.Window.Height);
                               };
                       }
                   );
            }

            {

                uppersprite.wmode();

                uppersprite.AttachSpriteToDocument().With(
                       embed =>
                       {
                           embed.style.SetLocation(Native.Window.Width / 2, 0);
                           embed.style.SetSize(Native.Window.Width / 2, Native.Window.Height / 2 - 1);

                           Native.Window.onresize +=
                               delegate
                               {
                                   embed.style.SetLocation(Native.Window.Width / 2, 0);
                                   embed.style.SetSize(Native.Window.Width / 2, Native.Window.Height / 2 - 1);
                               };
                       }
                   );
            }

            {
                lowersprite.wmode();

                lowersprite.AttachSpriteToDocument().With(
                       embed =>
                       {
                           embed.style.SetLocation(Native.Window.Width / 2, Native.Window.Height / 2);
                           embed.style.SetSize(Native.Window.Width / 2, Native.Window.Height / 2);

                           Native.Window.onresize +=
                               delegate
                               {
                                   embed.style.SetLocation(Native.Window.Width / 2, Native.Window.Height / 2);
                                   embed.style.SetSize(Native.Window.Width / 2, Native.Window.Height / 2);
                               };
                       }
                   );
            }
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
