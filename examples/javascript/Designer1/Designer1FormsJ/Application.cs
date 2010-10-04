using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Extensions.Avalon;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using Designer1FormsJ.HTML.Pages;

namespace Designer1FormsJ
{
    internal sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationApplet content = new ApplicationApplet();

        public Application(IDefaultPage page)
        {
            //page.Content.innerText = "hi!";

            content.AttachAppletTo(page.Content);
            content.AutoSizeAppletTo(page.ContentSize);

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }
    }
}
