using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using System;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    public partial class IHTMLDocument
    {
        // any reason to use the icon for the default notification too?
        [Obsolete("experimental")]
        public IHTMLImage icon
        {
            [Script(DefineAsStatic = true)]
            set
            {
                var link = new IHTMLLink
                {
                    rel = "icon",
                    type = "image/png",
                    href = value.src
                };

                // X:\jsc.svn\examples\javascript\Test\TestDocumentIcon\TestDocumentIcon\Application.cs

                //Native.document.documentElement.insertBefore(
                //    link,
                //    Native.document.head
                //);


                link.AttachTo(
                    Native.document.head
                );

            }
        }
    }
}
