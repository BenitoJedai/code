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
using GAEMeetsNuGetSQLite.Design;
using GAEMeetsNuGetSQLite.HTML.Pages;

namespace GAEMeetsNuGetSQLite
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            page.Send.onclick +=
                  delegate
                  {
                      service.AddItem(
                        page.Key.value,
                        page.Content.value,
                        value => value.ToDocumentTitle()
                      );
                  };

            //page.Enumerate.onclick +=
            //    delegate
            //    {
            //        page.output.Clear();

            //        service.EnumerateItems(
            //            "",
            //            (Key, Content) =>
            //            {
            //                page.output.Add(
            //                    new IHTMLDiv { innerText = new { Key, Content }.ToString() }
            //                );
            //            }
            //        );
            //    };

            @"Hello world".ToDocumentTitle();
        }

    }
}
