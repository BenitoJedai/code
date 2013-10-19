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
using Cloud9ACE.Design;
using Cloud9ACE.HTML.Pages;
using ScriptCoreLib.Shared.Lambda;

namespace Cloud9ACE
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
        public Application(IApp page)
        {
            #region ChromeTCPServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview().src;
                chrome.Notification.DefaultTitle = "Cloud9ACE";


                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                    AppSource.Text
                );

                return;
            }
            #endregion

            // http://ace.ajax.org/#nav=embedding
            // http://ace.ajax.org/build/kitchen-sink.html

            #region await Three.js then do InitializeContent
            new[]
            {
                new Design.ace().Content,
                new Design.theme_dreamweaver().Content,
                new Design.mode_csharp().Content,
            }.ForEach(
                (SourceScriptElement, i, MoveNext) =>
                {
                    SourceScriptElement.AttachToDocument().onload +=
                        delegate
                        {
                            MoveNext();
                        };
                }
            )(
                delegate
                {
                    new IFunction("e", @"

    var editor = ace.edit(e);

    editor.setTheme('ace/theme/dreamweaver');
    editor.getSession().setMode('ace/mode/csharp');

"

                         ).apply(null, page.editor.id);


                }
            );
            #endregion
            //@"Hello world".ToDocumentTitle();
            //// Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);
        }

    }
}
