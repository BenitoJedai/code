using FlashAsyncWhenReady;
using FlashAsyncWhenReady.Design;
using FlashAsyncWhenReady.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace FlashAsyncWhenReady
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
            sprite.WhenReady(
                delegate
                {

                    Console.WriteLine("WhenReady");
                }
            );

            sprite.With(
                async delegate
                {
                    Console.WriteLine("before await");

                    var e = new Stopwatch();

                    e.Start();

                    await sprite;

                    Console.WriteLine("after await " + e.ElapsedMilliseconds);
                }
            );


            sprite.AutoSizeSpriteTo(page.ContentSize);
            sprite.AttachSpriteTo(page.Content);
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
