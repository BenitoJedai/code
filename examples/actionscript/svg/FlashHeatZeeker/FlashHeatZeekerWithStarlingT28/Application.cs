using FlashHeatZeekerWithStarlingT28.Design;
using FlashHeatZeekerWithStarlingT28.HTML.Pages;
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
using ScriptCoreLib.ActionScript.flash.display;
using Abstractatech.ConsoleFormPackage.Library;
using System.Windows.Forms;

namespace FlashHeatZeekerWithStarlingT28
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



            var con = new ConsoleForm();

            con.InitializeConsoleFormWriter();

            con.Show();

            con.Left = Native.window.Width - con.Width;
            con.Top = 0;

            Native.window.onresize +=
                  delegate
                  {
                      con.Left = Native.window.Width - con.Width;
                      con.Top = 0;
                  };


            con.Opacity = 0.6;

            sprite.fps +=
                fps =>
                {
                    con.Text = new { fps }.ToString();
                };


            sprite.InitializeConsoleFormWriter(
                       Console.Write,
                       Console.WriteLine
                   );

            Native.window.onmessage +=
              e =>
              {
                  sprite.game_postMessage(XElement.Parse((string)e.data));
              };

            if (Native.window.opener != null)
            {
                sprite.context_onmessage +=
                    e =>
                    {
                        Native.window.opener.postMessage(e.ToString());
                    };
            }
            else
            {
                new Button { Text = "Secondary View" }.With(
                    connect =>
                    {
                        connect.AttachTo(con);

                        connect.Left = 8;
                        connect.Top = 8;

                        connect.Click +=
                            delegate
                            {
                                var w = Native.window.open(Native.Document.location.href, "_blank", 600, 600, false);


                                w.onload +=
                                    delegate
                                    {
                                        Console.WriteLine("loaded: " + w.document.location.href);

                                        Native.window.onmessage +=
                                             e =>
                                             {
                                                 if (e.source == w)
                                                     return;

                                                 // relay, not echo
                                                 w.postMessage(e.data);
                                             };

                                        sprite.context_onmessage +=
                                            e =>
                                            {
                                                w.postMessage(e.ToString());
                                            };

                                    };
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
