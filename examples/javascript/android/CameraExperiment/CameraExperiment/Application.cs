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
using CameraExperiment.Design;
using CameraExperiment.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;

namespace CameraExperiment
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
            page.TakePicture.style.color = "blue";



            page.TakePicture.onclick +=
                delegate
                {
                    //Unknown chromium error: -324

                    service.TakePicture("",
                        path =>
                        {
                            Console.WriteLine(new { path });

                            //new IHTMLDiv { innerText = new { e }.ToString() }.AttachToDocument();

                            new IHTMLImage { }.AttachToDocument().With(
                                img =>
                                {
                                    // portrait mode only!

                                    //div.style.color = JSColor.Red;
                                    img.src = "/thumb/" + path;

                                    #region onload +=
                                    img.InvokeOnComplete(
                                        delegate
                                        {
                                            //div.style.color = JSColor.Green;

                                            IHTMLPre p = null;

                                            img.onclick += delegate
                                            {
                                                if (p == null)
                                                {
                                                    img.src = "/io/" + path;
                                                    img.style.width = "100%";
                                                    //div.style.display = IStyle.DisplayEnum.block;

                                                    p = new IHTMLPre { }.AttachToDocument();
                                                    service.GetEXIF("/io/" + path,
                                                        x =>
                                                        {
                                                            p.innerText = x;
                                                        }
                                                    );


                                                }
                                                else
                                                {

                                                    p.Orphanize();
                                                    p = null;
                                                    img.src = "/thumb/" + path;
                                                    img.style.width = "";
                                                }

                                            };
                                        }
                                    );
                                    #endregion



                                }
                            );
                        }
                    );

                };
        }

    }
}
