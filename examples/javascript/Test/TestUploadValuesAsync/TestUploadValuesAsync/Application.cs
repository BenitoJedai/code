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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestUploadValuesAsync;
using TestUploadValuesAsync.Design;
using TestUploadValuesAsync.HTML.Pages;

namespace TestUploadValuesAsync
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
            // X:\jsc.svn\examples\actionscript\Test\TestWebClient\TestWebClient\ApplicationSprite.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\WebGL\Uint8ClampedArray.cs

            new IHTMLButton { "invoke" }.AttachToDocument().WhenClicked(
                button =>
                {
                    var w = new WebClient();


                    w.UploadValuesCompleted +=
                        (sender, args) =>
                        {
                            if (args.Error != null)
                            {
                                //t.text = "UploadValuesCompleted error " + new { args.Error }.ToString();

                                return;
                            }

                            // DownloadStringAsync { Length = 2822 }

                            var data = Encoding.UTF8.GetString(args.Result);
                            // 

                            new IHTMLPre { 


                            
                                "UploadValuesCompleted " + new { args.Result.Length }.ToString() + "\n\n" + data

                            }.AttachToDocument();


                        };



                    //UploadValuesCompleted { Length = 77 }
                    //<document><TaskComplete><TaskResult>13</TaskResult></TaskComplete></document>
                    // crossdomain.xml	
                    //GET
                    //404
                    // UploadValuesCompleted error { Error = Error: securityError { errorID = 2048, text = Error #2048 } }
                    w.UploadValuesAsync(
                        address: new Uri("http://my.monese.com/xml?WebMethod=06000010&n=GetUserID"),
                            data: new System.Collections.Specialized.NameValueCollection { 
                                { "_06000010_username", ""},
                                { "_06000010_psw", ""}
                            }
                    );

                    //w.UploadValuesAsync(
                    //    new Uri("/xml?WebMethod=06000001&n=WebMethod2", UriKind.Relative),
                    //       data: new System.Collections.Specialized.NameValueCollection { 
                    //            { "_06000001_username", ""},
                    //            { "_06000001_psw", ""}
                    //        }
                    //);

                    //  w.UploadValuesAsync(
                    //    new Uri("/xml?WebMethod=06000002&n=WebMethod4", UriKind.Relative),
                    //       data: new System.Collections.Specialized.NameValueCollection { 
                    //                  { "_06000002_username", ""},
                    //                  { "_06000002_psw", ""}
                    //              }
                    //);
                }
            );

            @"Hello world".ToDocumentTitle();

            this.WebMethod2(
                  "",
                  ""
              ).ContinueWithResult(
              y =>
              {
                  new IHTMLPre { 


                            
                               new {y}

                            }.AttachToDocument();
              }
            );

            // Send data from JavaScript to the server tier
            this.WebMethod4(
                "",
                ""
            );
        }

    }
}
