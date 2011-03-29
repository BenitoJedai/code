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
using WithJavaScript.HTML.Pages;

namespace WithJavaScript
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
        public Application(IDefaultPage page)
        {
            Action</*dynamic*/ IHTMLInput, string, Func<IHTMLScript>> apply =
              (btn, text, js) =>
              {
                  btn.disabled = true;
                  var Content = js();

                  #region AttatchToHead
                  var h = Native.Document.getElementsByTagName("head");

                  if (h.Length > 0)
                      h[0].appendChild(Content);
                  else
                      Content.AttachToDocument();
                  #endregion

                  var DisableScript = new IHTMLButton
                  {
                      innerText = "disable " + text
                  };


                  DisableScript.AttachTo(page.Content);

                  DisableScript.onclick +=
                      delegate
                      {
                          DisableScript.Orphanize();
                          Content.Orphanize();
                          btn.disabled = false;
                      };
              };

            page.Bar.onclick +=
              delegate
              {
                  apply(page.Bar, "Boo.js", () => new WithJavaScript.JavaScript.Bar().Content);
              };


            page.Foo.onclick +=
                delegate
                {
                    apply(page.Foo, "Foo.js", () => new WithJavaScript.JavaScript.Foo().Content);
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
