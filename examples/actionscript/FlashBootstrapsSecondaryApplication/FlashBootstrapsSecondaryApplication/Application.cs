using FlashBootstrapsSecondaryApplication.Design;
using FlashBootstrapsSecondaryApplication.HTML.Pages;
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

namespace FlashBootstrapsSecondaryApplication
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed partial class Application
    {


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            Console.WriteLine("Application ctor");

            if (page != null)
                if (page.Content != null)
                    if (page.Content.childNodes.Length == 0)
                    {
                        var f = new IFunction("");

                        f.apply(null);

                        ApplicationWebService service = new ApplicationWebService();

                        ApplicationSprite sprite = new ApplicationSprite();

                        sprite.AutoSizeSpriteTo(page.ContentSize);
                        sprite.AttachSpriteTo(page.Content);

                        //Console.WriteLine("init... WhenReady?");

                        //  a.__out_MethodInterface.MgAABkE_bRDa_ancxRw7JDdQ(PQAABosGVzucrRDVfJkoTA(a, b));
                        //sprite.WhenReady(
                        //    delegate
                        //    {
                        //        Console.WriteLine("init... Ready!");
                        //    }
                        //);

                        return;
                    }

            Console.WriteLine("looking for myself...");
            //Native.Window.alert("now what?");

            //Action yield = delegate
            //{
            //    Console.WriteLine("looking for myself... done!");

            //};
            Native.Window.requestAnimationFrame +=
                delegate
                {
                    Native.Document.getElementsByTagName("embed").WithEach(
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

                                Initialize(
                                    args =>
                                    {
                                        embed.CallFunction("WhenReady", new[] { args });
                                    }
                                );

                                //sprite.WhenReady(yield);
                            }
                            catch
                            { }
                        }
                    );
                };
        }

    

        static Application()
        {
            Console.WriteLine("Application cctor");

        }
    }
}
