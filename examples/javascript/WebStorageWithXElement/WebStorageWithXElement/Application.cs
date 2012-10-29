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
using WebStorageWithXElement.Design;
using WebStorageWithXElement.HTML.Pages;

namespace WebStorageWithXElement
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
            var foo = Native.Window.localStorage["foo"];
            var xml = new XElement("foo",
                        new XAttribute("title", "What is the title?"),
                        new XElement("content", "What is the content?")
                    );

            if (foo != null)
                xml = XElement.Parse(foo);

            page.ViewXMLSource.innerText = xml.ToString();

            xml.Attribute("title").With(Title => page.TextArea1.value = Title.Value);
            xml.Element("content").With(Content => page.TextArea2.value = Content.Value);

            page.Button1.onclick +=
                delegate
                {
                    var n = new XElement("foo",
                        new XAttribute("title", page.TextArea1.value),
                        new XElement("content", page.TextArea2.value)
                    );

                    page.ViewXMLSource.innerText = n.ToString();
                    Native.Window.localStorage["foo"] = n.ToString();
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
