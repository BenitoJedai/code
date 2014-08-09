using Abstractatech.ConsoleFormPackage.Library;
using FlashHeatZeeker.Shop.Design;
using FlashHeatZeeker.Shop.HTML.Pages;
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
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Abstractatech.JavaScript.FormAsPopup;
using System.Windows.Forms;

namespace FlashHeatZeeker.Shop
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
            var f = new Form();

            var fweb = new WebBrowser().AttachTo(f);
            fweb.Dock = DockStyle.Fill;
            var fiframe = (IHTMLIFrame)fweb.GetHTMLTargetContainer();
            f.Show();


            fiframe.onload += delegate
            {
                f.Text = "onload";

                fiframe.contentWindow.onbeforeunload += delegate
                {
                    f.Text = "onbeforeunload";
                };
            };


            //sprite.wmode();

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

            #region con
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
            #endregion


            sprite.InitializeConsoleFormWriter(
                       Console.Write,
                       Console.WriteLine
            );

            fiframe.id = "foo7";
            fiframe.name = "foo7";
            sprite.SetIFrameName(fiframe.name);

            con.HandleFormClosing = false;
            con.PopupInsteadOfClosing();
            "Operation «Heat Zeeker»".ToDocumentTitle();
        }

    }



}
