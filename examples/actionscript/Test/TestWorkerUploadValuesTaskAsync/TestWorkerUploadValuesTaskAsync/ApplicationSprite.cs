using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;

namespace TestWorkerUploadValuesTaskAsync
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            // X:\jsc.svn\examples\actionscript\Test\TestWebClient\TestWebClient\ApplicationSprite.cs
            // X:\jsc.svn\examples\actionscript\Test\TestWorkerUploadValuesTaskAsync\TestWorkerUploadValuesTaskAsync\ApplicationSprite.cs
            // X:\jsc.svn\examples\actionscript\Test\TestUploadValuesTaskAsync\TestUploadValuesTaskAsync\ApplicationSprite.cs

            // http://achen224.blogspot.com/2013/07/as3-worker-15.html

            var t = new TextField
            {
                autoSize = TextFieldAutoSize.LEFT,
                text = "click me",

                multiline = true
            }.AttachTo(this);



            //Task
            t.click += async delegate
            {
                // can we do it on the worker?
                // X:\jsc.svn\examples\java\hybrid\JVMCLRHopToThreadPool\JVMCLRHopToThreadPool\Program.cs

                t.text = "clicked!";


                var xdata = await Task.Run(
                    // ThreadPool
                    async delegate
                {
                    var _06000010_username = "";
                    // wtf ths wont work in worker?
                    // http://stackoverflow.com/questions/13979805/adobe-air-worker-cant-load-file
                    // wont work?
                    // makes workers useless?

                    var w = new WebClient();

                    // can we use client credentials?
                    var Result = await w.UploadValuesTaskAsync(
                            //new Uri("/xml?WebMethod=06000002&n=WebMethod2", UriKind.Relative),
                            new Uri("/xml/WebMethod2", UriKind.Relative),
                               data: new System.Collections.Specialized.NameValueCollection {
                                            { "_06000002_username", _06000010_username},
                                            { "_06000002_psw", ""},

                                   // the token keeps chaning!
                                   { "WebMethodMetadataToken", "06000008"},
                                            { "WebMethodMetadataName", "WebMethod2"}
                                        }
                        );

                    var data = Encoding.UTF8.GetString(Result);

                    return data;
                }
                );

                // 
                //t.text = "UploadValuesCompleted " + new { Result.Length } + "\n\n" + data;
                t.text = "UploadValuesCompleted " + new { xdata };
            };

        }

    }
}
