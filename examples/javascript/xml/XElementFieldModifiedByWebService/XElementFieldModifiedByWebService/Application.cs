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
using XElementFieldModifiedByWebService;
using XElementFieldModifiedByWebService.Design;
using XElementFieldModifiedByWebService.HTML.Pages;

namespace XElementFieldModifiedByWebService
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
        public Application(IApp page)
        {

            // jsc does automatic
            //this.output = page.output;

            //this.Content = page.Content;

            // script: error JSC1000: No implementation found for this native method, please implement [System.Threading.Tasks.Task.ContinueWith(System.Action`1[[System.Threading.Tasks.Task, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]])]
            // Set-Cookie:InternalFields=field_Content=PHAgaWQ9IkNvbnRlbnQiIHN0eWxlPSJwYWRkaW5nOiAyZW07ICAgICBjb2xvcjogYmx1ZTsiPmhlbGxvIHdvcmxkPC9wPg==; path=/

            this.Modify().ContinueWithResult(
                delegate
                {
                    "content changed".ToDocumentTitle();

                    // Error	3	Cannot implicitly convert type 'System.Xml.Linq.XElement' to 'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLSpan'. An explicit conversion exists (are you missing a cast?)	X:\jsc.svn\examples\javascript\XElementFieldModifiedByWebService\XElementFieldModifiedByWebService\Application.cs	46	36	XElementFieldModifiedByWebService
                    //page.Content.AsXElement().ReplaceAll(this.Content);
                }
            );
        }

    }
}
