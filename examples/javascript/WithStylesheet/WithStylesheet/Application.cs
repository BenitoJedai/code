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
using WithStylesheet.HTML.Pages;

namespace WithStylesheet
{
    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault  page)
        {
            Action</*dynamic*/ IHTMLInput, string, Func<IHTMLLink>> apply =
                (btn, text, css) =>
                {
                    btn.disabled = true;

                    var Content = css();

                    Content.AttachToHead();


                    var AddDynamicRule = new IHTMLButton
                    {
                        innerText = "add dynamic rule to " + text
                    };

                    var DisableStyle = new IHTMLButton
                    {
                        innerText = "disable style " + text
                    };

                    AddDynamicRule.AttachTo(page.Content);
                    AddDynamicRule.onclick +=
                        delegate
                        {
                            AddDynamicRule.disabled = true;

                            var rule1 = Content.StyleSheet.AddRule("button",
                                s =>
                                {
                                    s.style.color = "green";



                                }
                            );


                            var DisableDynamicRule = new IHTMLButton
                            {
                                innerText = "disable dynamic rule from " + text
                            };

                            DisableDynamicRule.AttachTo(page.Content);


                            DisableStyle.onclick +=
                                delegate
                                {
                                    DisableDynamicRule.Orphanize();
                                };

                            DisableDynamicRule.onclick +=
                                delegate
                                {
                                    // remove the last rule? :)
                                    Content.StyleSheet.RemoveRule(Content.StyleSheet.Rules.Length - 1);

                                    AddDynamicRule.disabled = false;
                                    DisableDynamicRule.Orphanize();
                                };
                        };

                 

                    DisableStyle.AttachTo(page.Content);

                    DisableStyle.onclick +=
                        delegate
                        {
                            AddDynamicRule.Orphanize();
                            DisableStyle.Orphanize();
                            Content.Orphanize();
                            btn.disabled = false;
                        };
                };

            page.Bar.onclick +=
                delegate
                {
                    apply(page.Bar, "Boo.css", () => new WithStylesheet.Styles.BooStyle().Content);
                };


            page.Foo.onclick +=
                delegate
                {
                    apply(page.Foo, "Foo.css", () => new WithStylesheet.Styles.FooStyle().Content);
                };


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
