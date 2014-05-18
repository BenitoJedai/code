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
using System.Windows.Forms;
using System.Data;

namespace PageNavigationExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        #region /s
        public class SearchPageApplication : ApplicationWebService
        {
            public SearchPageApplication(ISearchPage page, Application master)
            {
                new ContentContainer
                {
                    Content = new { Native.document.location.search }.ToString()
                }.AttachToDocument();


                new IHTMLDiv
                {
                    innerHTML = @"
                    <style>
                    html {
                        transition: border-top 500ms linear;
                        border-top: 4em solid purple;
                    }

                    </style>"
                }.With(
                    async div =>
                    {
                        //await Native.window.requestAnimationFrameAsync;

                        // wont work for html?
                        await Task.Delay(100);

                        div.AttachToDocument();
                    }
                    );

            }
        }
        #endregion


        #region /third-page
        public class ThirdPageApplication : ApplicationWebService
        {
            public ThirdPageApplication(IThirdPage page, Application master, string trace)
            {
                //Native.document.oner

                new IHTMLHeader1
                {
                    innerText = new
                    {
                        trace,

                        Native.document.location.search,
                        Native.document.location.hash
                    }.ToString()
                }.AttachToDocument();

                new IHTMLButton { innerText = "animate" }.AttachToDocument().WhenClicked(
                    delegate
                {
                    IStyleSheet.Default["body.third"].style.borderLeft = "5em red solid";

                }
                );

                IStyleSheet.Default["body.third"].style.borderLeft = "4em blue solid";

                new ContentContainer
                {
                    Content = new ApplicationWebService { }.GetItem(44)
                }.AttachToDocument();

                page.Yield.WhenClicked(
                    async button =>
                    {
                        var z = await this.GetSpecialString();

                        master.yield(z);
                    }
                );

                page.Data.WhenClicked(
                    async button =>
                    {

                        Native.window.history.pushState(
                            new { reason = "new state" },
                            async scope =>
                            {
                                // Uncaught Error: InvalidOperationException: we can only continue with global methods for now... { Target = { Index = 0 } }

                                var f = new Form
                                {
                                    Text = "loading..." + new { scope.state.reason }.ToString(),

                                    //Text = new { this.state.data.TableName }.ToString(),
                                    //ControlBox = false,
                                    ShowIcon = false,

                                    //WindowState = FormWindowState.Maximized
                                };

                                f.FormClosing +=
                                    (s, e) =>
                                    {
                                        if (e.CloseReason == CloseReason.UserClosing)
                                        {
                                            e.Cancel = true;

                                            Native.window.history.back();
                                        }
                                    };

                                var grid = new DataGridView
                                {
                                    // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.set_BorderStyle(System.Windows.Forms.BorderStyle)]
                                    //BorderStyle = BorderStyle.Fixed3D 
                                    //AutoSize = true,
                                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,

                                    //DataSource = this.state.data,
                                    Dock = DockStyle.Fill
                                }.AttachTo(f);

                                // do we need this?

                                //f.GetHTMLTarget().AttachTo(goopage.output);

                                grid.DataSourceChanged +=
                                    delegate
                                {
                                    Console.WriteLine("DataSourceChanged");

                                    (grid.DataSource as DataTable).With(
                                        data =>
                                            {
                                                f.Text = data.TableName;
                                            }
                                    );
                                };

                                f.Show();


                                //f.WindowState = FormWindowState.Maximized;
                                //f.PopupInsteadOfClosing(
                                //    HandleFormClosing: false

                                //    // , 
                                //    // does not play well with maximized yet
                                //    //SpecialNoMovement: true
                                //    );

                                {
                                    var data = await new ApplicationWebService { reason = scope.state.reason }.DoEnterData();

                                    Console.WriteLine("set DataSource");
                                    grid.DataSource = data;

                                    await scope;

                                    f.Close();
                                }

                            }
                        );

                    }
                );
            }
        }
        #endregion


        public class __state
        {
            public Application i;
        }

        static Application __that;

        public Action<string> yield;

        public Application(IApp page)
        {
            __that = this;

            this.yield = message =>
            {
                Native.window.alert("hello! " + new { message });
            };


            //((IHTMLElement)Native.document.body.parentNode).style.borderTop = "1em yellow yellow";

            //IStyleSheet.Default["html"].style.borderTop = "1em yellow yellow";


            IStyleSheet.Default["body"].style.borderLeft = "0em yellow solid";

            // activate all animations?
            IStyleSheet.Default["body"].style.transition = "border-left 300ms linear";
            IStyleSheet.Default["body"].style.borderLeft = "3em yellow solid";

            new IHTMLDiv
            {
                innerHTML = @"
<style>
html {
    transition: border-top 500ms linear;
    border-top: 4em solid cyan;
}

</style>"
            }.With(
           async div =>
           {
               //await Native.window.requestAnimationFrameAsync;

               // wont work for html?
               await Task.Delay(100);

               div.AttachToDocument();
           }
       );

            #region proof we can still find our element by id even if on a sub page
            new IHTMLTextArea { }.AttachTo(Native.document.body.parentNode).With(
                async area =>
                {
                    area.style.position = IStyle.PositionEnum.absolute;
                    area.style.right = "1em";
                    area.style.bottom = "1em";
                    area.style.zIndex = 1000;
                    area.readOnly = true;


                    Action colors = async delegate
                    {
                        for (int i = 0; i < 3; i++)
                        {

                            area.style.backgroundColor = "red";
                            await Task.Delay(200);
                            area.style.backgroundColor = "yellow";
                            await Task.Delay(200);
                        }
                        await Native.window.requestAnimationFrameAsync;
                        area.style.transition = "background-color 10000ms linear";

                        await Native.window.requestAnimationFrameAsync;

                        area.style.backgroundColor = "white";
                    };


                    colors();




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
            #endregion





            //page.Location = Native.document.location.hash;

            // #/OtherPage.htm

            Console.WriteLine(new { Native.document.location.pathname });


            #region GoThirdPage
            Action GoThirdPage = delegate
            {
                //IStyleSheet.Default["body"].style.borderLeft = "0em yellow solid";

                //await Task.Delay(300);

                // Console.WriteLine("pushState");
                // Native.window.history.pushState(
                //    null,
                //    null,
                //     //"/thirdpage.htm"
                //    "/third-page"
                //);

                Console.WriteLine("replaceState");
                Native.window.history.pushState(
                    //"/third-page",
                    new { },
                    "/third-page",
                    async scope =>
                    {
                        // did the server prerender our page?
                        Console.WriteLine("at replaceState");

                        var xtitle = Native.document.title;

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
                        //await Native.window.requestAnimationFrameAsync;
                        //await Native.window.requestAnimationFrameAsync;

                        //await Task.Delay(11);

                        var xthat = __that;
                        __that = null;
                        var x = new ThirdPageApplication(
                            layout,
                            xthat,
                            "pushState"
                        );

                        await scope;
                        __that = xthat;
                        Console.WriteLine("restore state!"); ;
                        Native.document.title = xtitle;

                        //Native.document.body.parentNode.replaceChild(hidden.querySelectorAll("body")[0], Native.document.body);
                        Native.document.body.parentNode.replaceChild(hidden.querySelectorAll("body").First(), Native.document.body);
                    }
                );
            };
            #endregion



            page.GoThirdPageViaCode.WhenClicked(
                  async button =>
                {
                    GoThirdPage();
                }
            );

            page.GoThirdPageViaLocation.WhenClicked(
                  async button =>
                  {
                      Native.document.location.href = "/third-page";
                  }
              );

            //if (Native.document.location.hash.StartsWith("#/"))
            //{
            //    Native.window.history.replaceState(
            //        new { foo = 1 },
            //        "",
            //        Native.document.location.hash.Substring(1)
            //    );

            //    //Native.window.history.replaceState(
            //    //    new { foo = 1 },
            //    //    scope =>
            //    //    {
            //    //        Native.document.body.style.backgroundColor = "yellow";
            //    //    }
            //    //);
            //}


            #region /third-page
            new[] { "ThirdPage.htm", "third-page" }.WithEach(
                async uri =>
                {
                    await Native.window.requestAnimationFrameAsync;

                    var selector = "a[href='" + uri + "']";


                    IStyleSheet.Default[selector].style.color = "red";


                    Native.document.body.querySelectorAll(IHTMLAnchor.HTMLElementEnum.a).WithEach(
                        xx =>
                        {
                            var x = (IHTMLAnchor)xx;


                            if (Native.document.location.href + uri != x.href)
                                return;

                            //Console.WriteLine(new { a = x.href, uri, Native.document.location.href });
                            // { a = http://192.168.43.252:13445/ThirdPage.htm, uri = ThirdPage.htm, href = http://192.168.43.252:13445/ } 

                            x.onclick +=
                                e =>
                                {
                                    e.preventDefault();
                                    GoThirdPage();
                                };
                        }
                    );

                    if (__that != null)
                        if (Native.document.location.pathname == "/" + uri)
                        {
                            //Native.window.history.replaceState(
                            //     null,
                            //     null,
                            //    //"/thirdpage.htm"
                            //    "/ThirdPage.htm"
                            //    //"/third-page"
                            // );


                            var layout = new ThirdPage.FromDocument();

                            new ThirdPageApplication(layout, __that, ".ctor");
                        }
                }
            );
            #endregion





            #region GoSearchPage
            Action GoSearchPage = delegate
            {
                //IStyleSheet.Default["body"].style.borderLeft = "0em yellow solid";

                //await Task.Delay(300);

                // Console.WriteLine("pushState");
                // Native.window.history.pushState(
                //    null,
                //    null,
                //     //"/thirdpage.htm"
                //    "/third-page"
                //);

                Console.WriteLine("replaceState");
                Native.window.history.pushState(
                    //"/third-page",
                    new { },
                    "/s",
                    async scope =>
                    {
                        // did the server prerender our page?
                        Console.WriteLine("at replaceState");

                        var xtitle = Native.document.title;

                        // { nodeName = #text } 
                        var hidden = (IHTMLElement)Native.document.body.querySelectorAll("hidden-body").FirstOrDefault();
                        Console.WriteLine("replaceState " + new { hidden });
                        var layout = default(ISearchPage);

                        if (hidden == null)
                        {
                            hidden = new IHTMLElement("hidden-body");
                            hidden.style.display = IStyle.DisplayEnum.none;

                            layout = new SearchPage();
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

                            layout = new SearchPage.FromDocument();
                        }

                        // ready!

                        // one wait works half time only
                        //await Native.window.requestAnimationFrameAsync;
                        //await Native.window.requestAnimationFrameAsync;

                        //await Task.Delay(11);

                        var xthat = __that;
                        __that = null;
                        var x = new SearchPageApplication(
                            layout,
                            xthat
                        );

                        await scope;
                        __that = xthat;
                        Console.WriteLine("restore state!"); ;
                        Native.document.title = xtitle;
                        //Native.document.body.parentNode.replaceChild(hidden.querySelectorAll("body")[0], Native.document.body);
                        Native.document.body.parentNode.replaceChild(hidden.querySelectorAll("body").First(), Native.document.body);
                    }
                );
            };
            #endregion


            #region /s
            new[] { "SearchPage.htm", "s" }.WithEach(
              async uri =>
              {
                  await Native.window.requestAnimationFrameAsync;

                  var selector = "a[href='" + uri + "']";


                  IStyleSheet.Default[selector].style.color = "orange";

                  Native.document.body.querySelectorAll(IHTMLAnchor.HTMLElementEnum.a).WithEach(
                        xx =>
                        {
                            var x = (IHTMLAnchor)xx;


                            if (Native.document.location.href + uri != x.href)
                                return;

                            x.style.borderBottom = "1px dashed blue";

                            x.onclick +=
                                e =>
                                {
                                    e.preventDefault();
                                    GoSearchPage();
                                };
                        }
                  );

                  if (__that != null)
                      if (Native.document.location.pathname == "/" + uri)
                      {
                          var layout = new SearchPage.FromDocument();

                          new SearchPageApplication(layout, this);
                      }
              }
          );
            #endregion

        }

    }
}
