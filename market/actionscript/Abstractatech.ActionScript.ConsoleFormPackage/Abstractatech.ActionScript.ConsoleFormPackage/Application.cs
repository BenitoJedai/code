using Abstractatech.ActionScript.ConsoleFormPackage.Design;
using Abstractatech.ActionScript.ConsoleFormPackage.HTML.Pages;
using Abstractatech.ConsoleFormPackage.Library;
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

namespace Abstractatech.ActionScript.ConsoleFormPackage
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        //assembly: C:\Users\Arvo\AppData\Local\Temp\ox3kmhez.pak\Abstractatech.JavaScript.FormAsPopup.dll
        //type: ScriptCoreLib.JavaScript.Extensions.X, Abstractatech.JavaScript.FormAsPopup, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        //offset: 0x004d
        // method:ScriptCoreLib.JavaScript.DOM.IWindow postMessage(ScriptCoreLib.JavaScript.DOM.IWindow, System.Xml.Linq.XElement, System.Action`1[System.Xml.Linq.XElement]) }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {


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



            con.HandleFormClosing = false;
            con.PopupInsteadOfClosing();
            #endregion

            Action<string> __SystemConsoleWrite = Console.Write;
            IFunction.OfDelegate(__SystemConsoleWrite).Export("__SystemConsoleWrite");

            if (page.__marker1 == null)
            {
                // the other mode
                #region let flash know js is ready


                Console.WriteLine("looking for myself...");
                //Native.Window.alert("now what?");

                //Action yield = delegate
                //{
                //    Console.WriteLine("looking for myself... done!");

                //};
                new ScriptCoreLib.JavaScript.Runtime.Timer(
                     delegate
                     {
                         Action<IHTMLElement> y =
                             e =>
                             {
                                 var embed = (IHTMLEmbedFlash)e;

                                 try
                                 {

                                     // this is a hack
                                     //var sprite = new ApplicationSprite();
                                     //object sprite_object = sprite;
                                     //dynamic a = sprite_object;

                                     //a.__InternalElement = embed;


                                     //var sprite = (ApplicationSprite)(object)embed;

                                     Console.WriteLine("looking for myself... WhenReady?");


                                     embed.CallFunction("WhenReady", new[] { "foo" });

                                     //sprite.WhenReady(yield);
                                 }
                                 catch
                                 { }
                             };

                         Native.Document.getElementsByTagName("object").WithEach(y);
                         Native.Document.getElementsByTagName("embed").WithEach(y);
                     }
                    ).StartTimeout(500);
                #endregion

                return;
            }


            var sprite = new ApplicationSprite();
            sprite.AutoSizeSpriteTo(page.ContentSize);
            sprite.AttachSpriteTo(page.Content);


            //__SystemConsoleWrite
            //sprite.InitializeConsoleFormWriter();

        }

    }
}
