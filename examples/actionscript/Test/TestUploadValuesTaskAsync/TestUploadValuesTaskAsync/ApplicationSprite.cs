using System;
using System.Net;
using System.Text;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;

namespace TestUploadValuesTaskAsync
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            // X:\jsc.svn\examples\actionscript\Test\TestWebClient\TestWebClient\ApplicationSprite.cs

            var t = new TextField
            {
                autoSize = TextFieldAutoSize.LEFT,
                text = "...",

                multiline = true
            }.AttachTo(this);

            t.click += async delegate
            {
                var _06000010_username = "";

                var w = new WebClient();

                //w.UploadValuesCompleted += (sender, args) =>
                //{
                //    if (args.Error != null)
                //    {
                //        t.text = "UploadValuesCompleted error " + new { args.Error }.ToString();

                //        return;
                //    }

                //    // DownloadStringAsync { Length = 2822 }

                //    var data = Encoding.UTF8.GetString(args.Result);
                //    // 
                //    t.text = "UploadValuesCompleted " + new { args.Result.Length } + "\n\n" + data;
                //};

                // can we use client credentials?
                var Result = await w.UploadValuesTaskAsync(
                    //new Uri("/xml?WebMethod=06000002&n=WebMethod2", UriKind.Relative),
                    new Uri("/xml/WebMethod2", UriKind.Relative),
                       data: new System.Collections.Specialized.NameValueCollection {
                                    { "_06000002_username", _06000010_username},
                                    { "_06000002_psw", ""},

                           // the token keeps chaning!
                           { "WebMethodMetadataToken", "06000007"},
                                    { "WebMethodMetadataName", "WebMethod2"}
                                }
                );

                var data = Encoding.UTF8.GetString(Result);
                // 
                t.text = "UploadValuesCompleted " + new { Result.Length } + "\n\n" + data;
            };



        }

    }
}
