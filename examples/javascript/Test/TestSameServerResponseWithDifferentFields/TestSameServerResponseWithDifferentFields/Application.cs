using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestSameServerResponseWithDifferentFields;
using TestSameServerResponseWithDifferentFields.Design;
using TestSameServerResponseWithDifferentFields.HTML.Pages;
using ScriptCoreLib.Ultra.Library.Extensions;

namespace TestSameServerResponseWithDifferentFields
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService //, IDisposable
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //  d = ((4 * _8CEABtNdQz66ZYUODttTfw(b)) / 3);

            Native.window.localStorage["body"].With(
                body =>
                {
                    var xbody = XElement.Parse(body);

                    //page.body.ReplaceWith(

                    page.body.AsXElement().ReplaceWith(xbody);
                }
            );

            AtFoo = delegate
            {
                new IHTMLPre { "AtFoo" }.AttachToDocument();

            };


            binary = Encoding.UTF8.GetBytes("hash me").ToMD5Bytes();


            var go = new IHTMLButton { "go " + binary.ToHexString() }.AttachToDocument().WhenClicked(
                async button =>
                {
                    await WebMethod2("hi");

                    new IHTMLPre { new { Counter } }.AttachToDocument();


                }
            );

            // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.InjectJavaScriptBootstrap.cs
            //Native.window.onbeforeunload +=
            //    delegate
            //    {
            //        // X:\jsc.svn\examples\javascript\test\TestSameServerResponseWithDifferentFields\TestSameServerResponseWithDifferentFields\Application.cs

            //        // this will not work for refresh. why? should it?
            //        Dispose();
            //    };


            //e = ( function () { var c$69 = d.constructor; return 'Interfaces' in c$69 ? ('f7G82WqfyzOLoZ_b8v0KVxw' in c$69.Interfaces) : false; } )();
            //f.asIDisposable = (d instanceof f7G82WqfyzOLoZ_b8v0KVxw ? d : null);



            //object x = this;

            //var isIDisposable = x is IDisposable;

            //// 0:37ms { isIDisposable = true, asIDisposable =  } 
            //var asIDisposable = x as IDisposable;

            //Console.WriteLine(new { isIDisposable, asIDisposable });

            Native.window.onbeforeunload +=
                delegate
                {
                    //if (x is IDisposable)
                    //{
                    //    ((IDisposable)x).Dispose();
                    //}


                    // dont save our button since we will re add it.
                    go.Orphanize();

                    // did we not already resolve html to xelement in another example?
                    // error on line 11 at column 79: Opening and ending tag mismatch: link line 0 and body
                    Native.window.localStorage["body"] = Native.document.body.AsXElement().ToString();
                };

        }

    }
}
