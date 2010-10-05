using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Designer1Forms.HTML.Pages;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Extensions.Avalon;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;

namespace Designer1Forms
{
    internal sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationControl content = new ApplicationControl();

        public Application(IDefaultPage page)
        {
            //page.Content.innerText = "hi!";

            content.AttachControlTo(page.Content);
            content.AutoSizeControlTo(page.ContentSize);

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }
    }
}
