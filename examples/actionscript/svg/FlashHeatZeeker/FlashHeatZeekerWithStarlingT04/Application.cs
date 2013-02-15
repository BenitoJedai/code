using Abstractatech.ConsoleFormPackage.Library;
using Abstractatech.JavaScript.FormAsPopup;
using FlashHeatZeekerWithStarlingT04.Design;
using FlashHeatZeekerWithStarlingT04.HTML.Pages;
using FlashHeatZeekerWithStarlingT04.Library;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.Shared.Lambda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FlashHeatZeekerWithStarlingT04
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


            sprite.InitializeConsoleFormWriter(
                       Console.Write,
                       Console.WriteLine
                   );

            // !! not compatible yet

            con.HandleFormClosing = false;
            con.PopupInsteadOfClosing();
            #endregion



            sprite.fps +=
                fps =>
                {
                    new { fps }.ToString().ToDocumentTitle();
                };


            #region context_new_remotegame
            sprite.context_new_remotegame +=
                remotegame =>
                {
                    var remotegame_con = new ConsoleForm();

                    remotegame_con.Show();
                    remotegame_con.Left = 0;
                    remotegame_con.Top = Native.Window.Height - remotegame_con.Height;

                    remotegame_con.Opacity = 0.5;

                    remotegame.AtTitleChange +=
                        e => remotegame_con.Text = e;

                    remotegame.AtWriteLine +=
                        e =>
                        {
                            remotegame_con.textBox1.AppendText(e + Environment.NewLine);
                            remotegame_con.textBox1.ScrollToCaret();
                        };


                    remotegame_con.HandleFormClosing = false;
                    remotegame_con.PopupInsteadOfClosing();
                };
            #endregion


            var sprites_events = new BindingListWithEvents<ApplicationSprite>();
            var sprites = sprites_events.Source;

            sprites_events.Added +=
                (fsprite, i) =>
                {
                    // lets do two way binding here.



                    // fsprite -> sprite
                    fsprite.context_onmessage += xml =>
                    {
                        // script: error JSC1000: No implementation found for this native method, please implement [static System.Linq.Enumerable.Except(System.Collections.Generic.IEnumerable`1[[FlashHeatZeekerWithStarlingT04.ApplicationSprite, FlashHeatZeekerWithStarlingT04.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], System.Collections.Generic.IEnumerable`1[[FlashHeatZeekerWithStarlingT04.ApplicationSprite, FlashHeatZeekerWithStarlingT04.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]])]

                        sprites.WithEach(
                            x =>
                            {
                                // prevent echo
                                if (x == fsprite)
                                    return;

                                x.game_postMessage(xml);
                            }
                        );
                    };
                };


            sprites.Add(sprite);

            #region game_InitializeFrameDiagnostics
            sprite.game_InitializeFrameDiagnostics(
                __FrameDiagnostics =>
                {
                    Console.WriteLine("new FrameDiagnostics");

                    var x = new FrameDiagnostics();

                    x.Show();

                    Console.WriteLine("new FrameDiagnostics Show, can you see it?");


                    Native.Window.onresize +=
                      delegate
                      {
                          x.Left = Native.Window.Width - x.Width;
                          x.Top = Native.Window.Height - x.Height;
                      };


                    x.Initialize(__FrameDiagnostics);

                    // Error	8	'Abstractatech.ConsoleFormPackage.Library.ConsoleForm' does not contain a definition for 'PopupInsteadOfClosing' and no extension method 'PopupInsteadOfClosing' accepting a first argument of type 'Abstractatech.ConsoleFormPackage.Library.ConsoleForm' could be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeekerWithStarlingT04\Application.cs	85	17	FlashHeatZeekerWithStarlingT04
                    // wtf?
                    x.PopupInsteadOfClosing();
                    x.Opacity = 0.7;

                    // can we pop it up?


                    x.JoinMultiplayer.Click +=
                        delegate
                        {
                            var f = new Form();
                            // make it small
                            f.Height = 200;

                            var fsprite = new ApplicationSprite();
                            fsprite.src_fixup();
                            fsprite.wmode();


                            var fspriteelement = fsprite.AttachSpriteTo(
                                f.GetHTMLTargetContainer()
                            );

                            #region ClientSizeChanged / PopupInsteadOfClosing has a bug
                            f.ClientSizeChanged +=
                                delegate
                                {
                                    var cs = f.ClientSize;

                                    fspriteelement.style.SetSize(cs.Width,
                                        cs.Height
                                    );
                                };
                            #endregion

                            f.Show();
                            f.PopupInsteadOfClosing();

                            // do events break if popup mode is changed?
                            fsprite.fps +=
                                fps =>
                                {
                                    new { fps }.ToString().ToTitle(f);
                                };




                            sprites.Add(fsprite);
                            // what if we have more of these?
                        };
                }
            );
            #endregion








        }

    }


    public static class XX
    {
        //<embed type="application/x-shockwave-flash" id="__embed_960211569" name="__embed_960211569" allowfullscreeninteractive="true" allowfullscreen="true" allownetworking="all" allowscriptaccess="always" width="500" height="380" src="assets/FlashHeatZeekerWithStarlingT04.Application/FlashHeatZeekerWithStarlingT04.ApplicationSprite.swf" wmode="direct" style="width: 392px; height: 165px;">

        public static void ToTitle(this string e, Form f)
        {
            f.Text = e;
        }

        [Obsolete("JSC should put extra effort to do this automatically on new Sprite!")]
        public static void src_fixup(this Sprite ee)
        {
            var e = (IHTMLEmbed)ee.ToHTMLElement();
            var src = e.src;
            // we will need absolute href!
            Console.WriteLine(
                new { src, Native.Document.location.href }
            );

            e.src = src;
        }

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
