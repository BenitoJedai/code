using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM.HTML;

using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Delegates;
using System.Diagnostics;

namespace ScriptCoreLib.JavaScript.Extensions
{
    /// <summary>
    /// Using these extensions will inject XLinq libraries into the compilation.
    /// </summary>
    public static class INodeExtensionsWithXLinq
    {
        // used by compiler
        // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.WebServiceForJavaScript.cs
        // 436
        public static XElement InternalReplaceAll(XElement value, XElement old, string ApplicationWebServieFieldName)
        {
            // X:\jsc.svn\examples\javascript\Test\TestServiceWorker\TestServiceWorker\Application.cs

            //Console.WriteLine("InternalReplaceAll" + new { old, value, ApplicationWebServieFieldName });

            //old.Add(new XAttribute("y", "this is what we have before update"));
            //value.Add(new XAttribute("z", "this is what we have after update"));


            // where does the old come from?
            // 0:23ms ReplaceAll {{ old = <title>dynamic title</title>, value = <title>dynamic title</title> }}

            // X:\jsc.svn\examples\javascript\test\TestVisibleSynchronizedTitleElement\TestVisibleSynchronizedTitleElement\Application.vb

            if (Native.document != null)
            {
                // not in service worker..

                var FromDocument = Native.document.getElementById(ApplicationWebServieFieldName).AsXElement();
                if (FromDocument != null)
                {
                    return InternalReplaceAll(value, FromDocument);
                }
            }

            //0:31ms ReplaceAll { { old = < title id = "title" > dynamic title </ title >, value = < title > dynamic title </ title > } }

            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Xml\Linq\XElement.cs
            // keep the ids around
            return InternalReplaceAll(value, old);
        }

        public static XElement InternalReplaceAll(XElement value, XElement old)
        {
            //Console.WriteLine("ReplaceAll " + new { old, value });

            if (value == null)
                return old;

            if (old == null)
                return value;

            //0:47ms InternalReplaceAll{{ old = <div>dynamic content</div>, value = <div>dynamic content</div>, ApplicationWebServieFieldName = content }} view-source:39090
            //0:51ms ReplaceAll {{ old = <div xmlns="http://www.w3.org/1999/xhtml" id="content" x="get rid of static">static content</div>, value = <div y="this is what we have before update">dynamic content</div> }} 

            var old_id = old.Attribute("id");

            // are we able to get id if there is a namespace?
            //Console.WriteLine("ReplaceAll " + new { old, old_id });
            //0:22ms ReplaceAll { { old = < div xmlns = "http://www.w3.org/1999/xhtml" id = "content" x = "get rid of static" >static content</ div >, value = < div y = "this is what we have before update" > dynamic content </ div > } }
            //0:23ms ReplaceAll { { old = < div xmlns = "http://www.w3.org/1999/xhtml" id = "content" x = "get rid of static" >static content</ div >, old_id = content } }

            var old_id_value = default(string);

            if (old_id != null)
            {
                old_id_value = old_id.Value;
            }

            // X:\jsc.svn\examples\javascript\XElementFieldModifiedByWebService\XElementFieldModifiedByWebService\Application.cs
            // X:\jsc.svn\examples\javascript\ColorDisco\ColorDisco\Application.cs


            old.ReplaceAll(value);


            if (old_id != null)
            {
                // restore the id
                old.SetAttributeValue("id", old_id_value);
            }

            return old;
        }

        /// <summary>
        /// Converts XML to HTML and appends all created nodes to the container.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="value"></param>
        public static void Add(this INode e, XElement value)
        {
            var c = default(IHTMLDiv);

            if (e.ownerDocument != null)
            {
                c = (IHTMLDiv)e.ownerDocument.createElement("div");
            }
            else
            {
                c = new IHTMLDiv();
            }

            c.innerHTML = value.ToString();

            e.appendChild(c.firstChild);
        }



        public static void Add(this INode e, Action<XElementAction> factory)
        {
            factory(e.Add);
        }


        public static void WithContent(this IHTMLIFrame that, Action<IHTMLBody> y)
        {
            that.WhenDocumentReady(
                d =>
                {
                    d.WithContent();
                    d.WhenContentReady(y);
                }
            );
        }

        public static IHTMLDocument WithContent(this IHTMLDocument document, params object[] content)
        {
            document.open("text/html", "replace");

            var doc = new XElement("body");

            doc.Add(new XAttribute("style", "border: 0; margin: 0; padding: 0;"));

            doc.Add(content);

            document.write(doc.ToString());
            document.close();

            return document;
        }
    }
}
