using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;
using System;
using System.Net;

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
                autoSize = TextFieldAutoSize.LEFT
            }.AttachTo(this);

            var w = new WebClient();

            w.DownloadStringCompleted +=
                (sender, args) =>
                {
                    if (args.Error != null)
                    {
                        t.text = "DownloadStringAsync error " + new { args.Error }.ToString();

                        return;
                    }

                    // DownloadStringAsync { Length = 2822 }
                    t.text = "DownloadStringAsync " + new { args.Result.Length }.ToString();
                };

            t.text = "DownloadStringAsync";
            w.DownloadStringAsync(
                new Uri("/jsc", UriKind.Relative)
                );

            //await w.DownloadStringTaskAsync("/jsc"
        }

    }
}
