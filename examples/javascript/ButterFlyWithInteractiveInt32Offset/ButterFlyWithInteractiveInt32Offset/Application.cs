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
using ButterFlyWithInteractiveInt32Offset.HTML.Pages;

namespace ButterFlyWithInteractiveInt32Offset
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
        public Application(IDefault page)
        {
            XInteractiveInt32Form.service.File_ReadLine = service.File_ReadLine;
            XInteractiveInt32Form.service.File_WriteLine = service.File_WriteLine;

            new ButterFlyWithInteractiveInt32Offset.Library.Butterfly(page.PageContainer);

            //Native.Document.body.onclick +=
            //    delegate
            //    {
            //        page.PageContainer.requestFullscreen();
            //    };

            //@"Hello world".ToDocumentTitle();
            //// Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);
        }

    }
}
