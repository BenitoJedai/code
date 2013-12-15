using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FormsWithVisibleTitle;
using FormsWithVisibleTitle.Design;
using FormsWithVisibleTitle.HTML.Pages;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;
using System.Drawing;
using ScriptCoreLib.Lambda;
using System.Windows.Forms;

namespace FormsWithVisibleTitle
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp __page)
        {

            FormStyler.AtFormCreated = FormStyler.LikeVisualStudioMetro;

            #region title
            new global::CSSFuzzyGrayBackground.Design.AppStyle().With(
                async link =>
                {
                    var old = new { Native.document.styleSheets.Length };

                    Console.WriteLine("link css " + new { link.Content.href, old });

                    link.Content.AttachToDocument();
                    //link.Content.AttachToHead();

                    while (old.Length == Native.document.styleSheets.Length)
                        await Native.window.requestAnimationFrameAsync;

                    // link css, done { href = http://127.0.0.1:12068/assets/CSSFuzzyGrayBackground/App.css, Length = 3 }

                    // is StyleSheet available yet?
                    await 200;


                    Console.WriteLine("link css, done " + new { link.Content.href, Native.document.styleSheets.Length });

                    //Native.document.styleSheets.Length

                    link.Content.StyleSheet.With(
                        sheet =>
                        {
                            sheet.Rules.WithEach(
                                rule =>
                                {
                                    //[selectorName='yellowheader']

                                    Console.WriteLine(
                                        new
                                        {
                                            rule.selectorText
                                        }
                                    );

                                    //{ selectorText = h1 } view-source:32061
                                    //{ selectorText = body } view-source:32061
                                    //{ selectorText = [selectorname="yellowheader"], h1, p } 

                                    // selectorText parser to expression?
                                    if (rule.selectorText.StartsWith("[selectorname=\"yellowheader\"]"))
                                    {
                                        // we like that style so much, we want to use it
                                        // on our title.
                                        rule.selectorText += ", title";

                                        //Native.css
                                        IStyleSheet.all["head, title"].style.display = IStyle.DisplayEnum.block;

                                        // upate the content of the rule too
                                        rule.style.fontFamily = new global::CSSFuzzyGrayBackground.Design.KGBytheGraceofGod();
                                        rule.style.fontSize = "2em";
                                        rule.style.fontWeight = "bold";
                                        rule.style.margin = "0.67em";
                                        rule.style.whiteSpace = IStyle.WhiteSpaceEnum.pre;

                                    }
                                }
                            );
                        }
                    );

                    //IStyleSheet.all.ru

                    //Native.css
                    //IStyleSheet.all[IHTMLElement.HTMLElementEnum.h1].style.fontFamily = KG By the Grace of God
                    IStyleSheet.all[IHTMLElement.HTMLElementEnum.label].style.color = "yellow";

                }
            );
            #endregion


            // nothng to select or drag
            __page.sizetarget.With(
                async x =>
                {
                    x.readOnly = true;

                    //style.color = "transparent";


                    var style = x.style;

                    style.transition = "border 300ms linear, background-color 300ms linear";




                    while (true)
                    {



                        if (__page.content.querySelectorAll("div").Any())
                        {
                            style.border = "1px dashed rgb(0, 122, 204)";
                            x.css.style.backgroundColor = "rgba(255,255,255,0.1)";
                            style.boxShadow = "rgba(0, 122, 204, 0.298039) 0px 0px 6px 3px";
                        }
                        else
                        {
                            style.boxShadow = "rgba(255, 0, 0, 0.298039) 0px 0px 9px 3px";
                            x.css.style.backgroundColor = "rgba(255,127,127,0.1)";
                            style.border = "1px dashed red";
                        }


                        await Task.Delay(600);
                        style.border = "1px dashed transparent";

                        await Task.Delay(900);

                        if (__page.content.querySelectorAll("div").Any())
                        {
                            style.border = "1px solid rgb(0, 122, 204)";
                        }
                        else
                        {
                            style.border = "1px solid red";

                            style.boxShadow = "rgba(255, 0, 0, 0.298039) 0px 0px 6px 3px";

                        }

                        await Task.Delay(3000);

                        style.border = "1px dashed transparent";

                        await Task.Delay(900);

                    }
                }
            );

            // are we on a cliean slate?
            Native.document.title += "?" + Native.window.history.length;

            //__page.sizetarget.async.onclick += 
            //Native.window.requestAnimationFrameAsync.ContinueWith(





            //__page.sizetarget.async.onclick.ContinueWithResult(
            __page.sizetarget.onclick +=
                async delegate
                {
                    // why do we need this?
                    await Native.window.requestAnimationFrameAsync;

                    Native.document.title += "!";

                    Native.window.history.pushState(
                        state: new { foo = "bar ", __page.sizetarget.value },
                        yield:
                            async scope =>
                            {
                                var page = new App.FromDocument();

                                Native.document.title += ">";

                                page.sizetarget.value = "";
                                page.sizetarget.disabled = true;

                                #region >
                                // Uncaught Error: InvalidOperationException: we can only continue with global methods for now... { Target = [object Object] } 
                                // we could tread the page object special, if it is the only shared reference


                                //await 200;

                                #region Form1
                                var f = new Form1
                                {
                                    Text = "Activity1 ",
                                    BackColor = Color.Transparent
                                    //BackColor = Color.FromArgb(20, 255, 255, 255)
                                };

                        
                                __Form ff = f.AttachControlTo(page.content);


                                // this wont work with maximized mode yet?
                                //ff.ResizeGripElement.Show();

                                ff.HTMLTargetRef.style.bottom = "16px";

                                page.sizetarget.style.minWidth = f.Width + "px";
                                page.sizetarget.style.minHeight = (f.Height + 16) + "px";

                                f.ClientSizeChanged +=
                                    delegate
                                    {
                                        page.sizetarget.style.minWidth = f.Width + "px";
                                        page.sizetarget.style.minHeight = (f.Height + 16) + "px";
                                    };


                                // remove the max blackness. the vista max.
                                ff.InternalStyler.CaptionShadow.style.backgroundColor = "";
                                ff.InternalStyler.Caption.style.backgroundColor = "";

                                // make it large. how large?
                                //ff.InternalStyler.CaptionContent.style.fontSize = "22pt";
                                ff.InternalStyler.CaptionContent.style.lineHeight = "";

                                // magic
                                ff.InternalStyler.CaptionContent.style.height = "47px";
                                ff.CaptionForeground.style.height = "47px";

                                ff.InternalStyler.TargetOuterBorder.style.border = "";
                                ff.InternalStyler.TargetOuterBorder.style.boxShadow = "";

                                ff.InternalStyler.TargetOuterBorder.style.background = "linear-gradient(to bottom, rgba(0,0,0,0.9) 0%,rgba(0,0,0,0) 100%)";


                                #endregion

                                //await scope | f.FormClosed;

                                //var FormClosed = new TaskCompletionSource<Form>();
                                f.FormClosed += delegate { f = null; Native.window.history.back(); };

                                //await Task.WhenAny(scope, FormClosed);
                                await scope;
                                if (f != null)
                                    f.Close();
                                #endregion

                                Native.document.title += "<";

                                page.sizetarget.value = scope.state.value;
                                page.sizetarget.disabled = false;
                            }
                    );
                };

            __page.sizetarget.value = "click to enter\n- or -\nclick forward to redu last step";
            __page.sizetarget.style.color = "yellow";
            __page.sizetarget.style.padding = "1em";
            __page.sizetarget.style.textAlign = IStyle.TextAlignEnum.center;

            // fist timer can auto enter!
            if (Native.window.history.length < 2)
            {
                ((IHTMLAnchor)(object)__page.sizetarget).click();
            }
            else
            {

            }
        }

    }
}
