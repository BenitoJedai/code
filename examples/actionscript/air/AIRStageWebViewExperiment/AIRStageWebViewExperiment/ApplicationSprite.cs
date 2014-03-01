using AIRStageWebViewExperiment.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.Extensions;

namespace AIRStageWebViewExperiment
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            // http://priyeshsheth.wordpress.com/2011/05/27/stagewebview-in-actionscirpt-3-flash-media-stagewebview/

            // http://www.actionscript.org/forums/showthread.php3?t=265223


            var webView = new StageWebView();
            webView.stage = this.stage;
            webView.viewPort = new Rectangle(0, 0, stage.stageWidth, stage.stageHeight);

            //var htmlString = "<!DOCTYPE HTML>" +
            //                        "<html>" +
            //                            "<body>" +
            //                                "<h1>Example</h1>" +
            //                                "<p>King Phillip cut open five green snakes.</p>" +
            //                            "</body>" +
            //                        "</html>";

            //webView.loadString(htmlString, "text/html");




            webView.loadString(AppSource.Text, "text/html");
        }

    }
}

