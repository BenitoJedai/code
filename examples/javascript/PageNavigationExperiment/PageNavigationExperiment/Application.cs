using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PageNavigationExperiment;
using PageNavigationExperiment.Design;
using PageNavigationExperiment.HTML.Pages;
using System.Diagnostics;

namespace PageNavigationExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public class ThirdPageApplication : ApplicationWebService
        {
            public ThirdPageApplication(IThirdPage x, Application scope)
            {

                //IStyleSheet.Default["body.third"].style.borderLeft = "3em red solid";

                new IHTMLButton { innerText = "animate" }.AttachToDocument().WhenClicked(
                    delegate
                    {
                        IStyleSheet.Default["body.third"].style.borderLeft = "5em red solid";

                    }
                );

                //Native.window.requestAnimationFrame +=
                //    delegate
                //    {
                IStyleSheet.Default["body.third"].style.borderLeft = "4em blue solid";

                //};
            }
        }

        //public Application(
        //    IApp page, 
        //    IThirdPage thirdpage
        //    )
        //    : this(page)
        //{
        //    // master page upgraded
        //    // animate this secondary page

        //    new IHTMLButton { innerText = "animate" }.AttachToDocument().WhenClicked(
        //        delegate
        //        {
        //            IStyleSheet.Default["body.third"].style.borderLeft = "5em red solid";

        //        }
        //    );

        //    //Native.window.requestAnimationFrame +=
        //    //    delegate
        //    //    {
        //    IStyleSheet.Default["body.third"].style.borderLeft = "4em blue solid";
        //}



        public Application(IApp page)
        {
            IStyleSheet.Default["body"].style.borderLeft = "0em yellow solid";

            // activate all animations?
            IStyleSheet.Default["body"].style.transition = "border-left 300ms linear";
            IStyleSheet.Default["body"].style.borderLeft = "3em yellow solid";

            new IHTMLTextArea { }.AttachTo(Native.document.body.parentNode).With(
                async area =>
                {
                    area.style.position = IStyle.PositionEnum.absolute;
                    area.style.right = "1em";
                    area.style.bottom = "1em";
                    area.style.zIndex = 1000;
                    area.readOnly = true;
                    area.style.backgroundColor = "yellow";
                    area.style.transition = "background-color 10000ms linear";

                    await Native.window.requestAnimationFrameAsync;

                    area.style.backgroundColor = "white";


                    var st = new Stopwatch();
                    st.Start();

                    while (true)
                    {
                        // proof we can still find our element by id even if on a sub page
                        area.value = page.message.innerText + "\n" + st.ToString();

                        await Task.Delay(500);
                    }
                }
            );


            //page.Location = Native.document.location.hash;

            // #/OtherPage.htm

            Console.WriteLine(new { Native.document.location.pathname });

            Action GoThirdPage = delegate
            {
                //IStyleSheet.Default["body"].style.borderLeft = "0em yellow solid";

                //await Task.Delay(300);

                Native.window.history.pushState(
                   null,
                   null,
                    //"/thirdpage.htm"
                   "/third-page"
               );

                Native.window.history.replaceState(
                    //"/third-page",
                    new { },
                    async scope =>
                    {
                        // did the server prerender our page?
                        Console.WriteLine("replaceState");

                        // { nodeName = #text } 
                        var hidden = (IHTMLElement)Native.document.body.querySelectorAll("hidden-body").FirstOrDefault();
                        Console.WriteLine("replaceState " + new { hidden });
                        var layout = default(IThirdPage);

                        if (hidden == null)
                        {
                            hidden = new IHTMLElement("hidden-body");
                            hidden.style.display = IStyle.DisplayEnum.none;

                            layout = new ThirdPage();
                            Native.document.title = layout.title.innerText;

                            var page_body = Native.document.body;

                            layout.body.appendChild(hidden);
                            page_body.parentNode.replaceChild(layout.body, page_body);

                            // we can also keep it memory
                            hidden.appendChild(page_body);
                        }
                        else
                        {
                            //{ nodeName = YDOB } 
                            var page_ydob = (IElement)hidden.querySelectorAll("ydob").FirstOrDefault();
                            if (page_ydob != null)
                            {
                                // chrome will skip body. have to repair on the client

                                var page_body = new IHTMLBody();

                                page_ydob.attributes.ToArray().WithEach(a => { page_ydob.removeAttribute(a.name); page_body.setAttribute(a.name, a.value); });
                                page_ydob.childNodes.ToArray().WithEach(a => { page_ydob.removeChild(a); page_body.appendChild(a); });

                                hidden.replaceChild(page_body, page_ydob);

                            }

                            layout = new ThirdPage.FromDocument();
                        }

                        // ready!

                        // one wait works half time only
                        await Native.window.requestAnimationFrameAsync;
                        await Native.window.requestAnimationFrameAsync;


                        var x = new ThirdPageApplication(
                            layout,
                            null
                        );


                        await scope;

                        Console.WriteLine("restore state!"); ;

                        Native.document.body.parentNode.replaceChild(hidden.querySelectorAll("body")[0], Native.document.body);
                    }
                );
            };

            page.UseHistoryAPI.onclick +=
                e =>
                {
                    e.preventDefault();
                    GoThirdPage();
                };

            page.GoThirdPage.WhenClicked(
                async delegate
                {
                    GoThirdPage();
                }
            );

            if (Native.document.location.hash.StartsWith("#/"))
            {
                Native.window.history.replaceState(
                    new { foo = 1 },
                    "",
                    Native.document.location.hash.Substring(1)
                );

                //Native.window.history.replaceState(
                //    new { foo = 1 },
                //    scope =>
                //    {
                //        Native.document.body.style.backgroundColor = "yellow";
                //    }
                //);
            }




            if (Native.document.location.pathname == "/ThirdPage.htm")
            {
                //Native.window.history.replaceState(
                //     null,
                //     null,
                //    //"/thirdpage.htm"
                //    "/ThirdPage.htm"
                //    //"/third-page"
                // );
                var layout = new ThirdPage.FromDocument();

                new ThirdPageApplication(layout, this);
            }

        }

    }
}
