using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;
using System;
using System.Net;
using System.Text;

namespace TestWebClient
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            // DownloadStringAsync error { Error = Error: securityError }
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140109-webclient
            // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Net\WebClient.cs

            var t = new TextField
            {
                autoSize = TextFieldAutoSize.LEFT,
                text = "...",

                multiline = true
            }.AttachTo(this);

            this.root.loaderInfo.uncaughtErrorEvents.uncaughtError +=
                e =>
                {
                    // TypeError: Error #1034
                    t.text = "error: " + new { e.errorID, e.text, e.error } + "\n\n"
                        + ((Error)e.error).getStackTrace() + "";

                };

            var w = new WebClient();

            //w.DownloadStringCompleted +=
            //    (sender, args) =>
            //    {
            //        if (args.Error != null)
            //        {
            //            t.text = "DownloadStringAsync error " + new { args.Error }.ToString();

            //            return;
            //        }

            //        // DownloadStringAsync { Length = 2822 }
            //        t.text = "DownloadStringAsync " + new { args.Result.Length }.ToString();
            //    };

            //w.DownloadStringAsync(
            //    new Uri("/jsc", UriKind.Relative)
            //    );


            w.UploadValuesCompleted +=
                (sender, args) =>
                {
                    if (args.Error != null)
                    {
                        t.text = "UploadValuesCompleted error " + new { args.Error }.ToString();

                        return;
                    }

                    // DownloadStringAsync { Length = 2822 }

                    var data = Encoding.UTF8.GetString(args.Result);
                    // 
                    t.text = "UploadValuesCompleted " + new { args.Result.Length }.ToString() + "\n\n" + data;
                };

            var _06000010_username = "";

            //UploadValuesCompleted { Length = 77 }
            //<document><TaskComplete><TaskResult>13</TaskResult></TaskComplete></document>
            // crossdomain.xml	
            //GET
            //404
            // UploadValuesCompleted error { Error = Error: securityError { errorID = 2048, text = Error #2048 } }
            w.UploadValuesAsync(
                address: new Uri("http://my.monese.com/xml?WebMethod=06000010&n=GetUserID"),
                    data: new System.Collections.Specialized.NameValueCollection { 
                                { "_06000010_username", _06000010_username},
                                { "_06000010_psw", ""}
                            }
            );

            //w.UploadValuesAsync(
            //    new Uri("/xml?WebMethod=06000002&n=WebMethod2", UriKind.Relative),
            //       data: new System.Collections.Specialized.NameValueCollection { 
            //            { "_06000002_username", _06000010_username},
            //            { "_06000002_psw", ""}
            //        }
            //);

            //await w.DownloadStringTaskAsync("/jsc"
        }

    }
}
