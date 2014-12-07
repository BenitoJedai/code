using AIRStageWebViewExperiment.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.Extensions;
using System.Xml.Linq;

namespace AIRStageWebViewExperiment
{
    public sealed class ApplicationSprite : Sprite
    {
        // X:\jsc.svn\examples\actionscript\air\AIRServerSocketExperiment\AIRServerSocketExperiment\ApplicationSprite.cs

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201412/20141207

        public ApplicationSprite()
        {
            // http://priyeshsheth.wordpress.com/2011/05/27/stagewebview-in-actionscirpt-3-flash-media-stagewebview/

            // http://www.actionscript.org/forums/showthread.php3?t=265223



            if (Capabilities.playerType != "Desktop")
                // dont crash in browser
                return;

            // is this a project template?
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20130302
            // do we have AIR apis available?
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


            var page = XElement.Parse(AppSource.Text);

            page.Element("body").Add(
                new XElement("button", "click me")
            );

            // as from a WebWorker or from the server we should be able
            // to construct HTML DOM objects yet, we may have issues doing the events for them.

            page.Element("body").Add(
                new XElement("style", "button { color: blue; }")
            );


            webView.loadString(page.ToString(), "text/html");

            //webView.
        }

    }
}

