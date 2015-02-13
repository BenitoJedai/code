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
using Abstractatech.JavaScript.TextEditor.Design;
using Abstractatech.JavaScript.TextEditor.HTML.Pages;

namespace Abstractatech.JavaScript.TextEditor
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

            var t = new ScriptCoreLib.JavaScript.Controls.TextEditor(
                page.body,

                fonts:
                    new[] {
                        new Fonts.BLOKKRegular().rule,
                        new Fonts.redacted_script_regular().rule,
                    }
            );

            Console.WriteLine("adding the button");

            // <font color="#0000fc">s</font>


            new IHTMLButton
            {
                "make svg friendly"
            }.AttachToDocument().WhenClicked(
                x =>
                {
                    Console.WriteLine(
                        new { body = t.Document.body.AsXElement() }
                    );

                //7990ms { body = <body style="height: auto; border: 0; overflow: auto; background-color:transparent;">ss<font color="#ff0000">dfsdf</font>sd</body> } view-source:35444
                //7993ms { f = <font color="#ff0000">dfsdf</font> } 

                t.Document.body.AsXElement().Elements().WithEach(
                zf =>
                {
                            Console.WriteLine(new { zf, zf.Name.LocalName });

                        // 10797ms { zf = <font color="#ff0000">dfs</font>, LocalName = FONT } 

                        if (zf.Name.LocalName.ToLower() == "div")
                            {

                                var ff = new IHTMLSpan { innerText = zf.Value };

                                var color = zf.Attribute("color").Value;

                                ff.style.color = color;

                                Console.WriteLine("ReplaceWith" + new { ff.innerText, color });

                                zf.ReplaceWith(ff.AsXElement());
                            }

                            if (zf.Name.LocalName.ToLower() == "font")
                            {

                                var ff = new IHTMLSpan { innerText = zf.Value };

                                var color = zf.Attribute("color").Value;

                                ff.style.color = color;

                                Console.WriteLine("ReplaceWith" + new { ff.innerText, color });

                                zf.ReplaceWith(ff.AsXElement());
                            }
                        }
            );

                // we inline images, should look for fonts inside svg too?
                t.Document.body.AsXElement().Elements("font").ToArray().WithEach(
                f =>
                {
                            Console.WriteLine(new { f });


                            var ff = new IHTMLSpan { innerText = f.Value };

                            ff.style.color = f.Attribute("color").Value;

                            f.ReplaceWith(ff.AsXElement());

                        //f.Nodes().att
                    }
            );
                }
            );

            new IHTMLButton
            {
                "getframe"
            }.AttachToDocument().WhenClicked(
                delegate
                {
                    var snapshot = new IHTMLDiv
                    {
                        innerHTML = t.InnerHTML
                    };

                    Console.WriteLine(t.InnerHTML);

                    IHTMLImage i = snapshot;

                    i.AttachToDocument();
                }
            );

            //@"Hello world".ToDocumentTitle();
            //// Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);
        }

    }
}
