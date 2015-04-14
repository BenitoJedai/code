using AIRNestedIFrameWithTransform.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.Extensions;
using System.Xml.Linq;
using System.Linq;

namespace AIRNestedIFrameWithTransform
{
    public sealed class ApplicationSprite : Sprite
    {
		// X:\util\air17_sdk_sa_win

		// X:\jsc.svn\examples\actionscript\air\AIRServerSocketExperiment\AIRServerSocketExperiment\ApplicationSprite.cs

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


            // um. 3d transform fails. and the jsc studio does not load. error? AIR webkit limitation?
            var page = XElement.Parse(CSSTransformStyleExample.HTML.Pages.DefaultSource.Text);


            //Implementation not found for type import :
            //type: System.Xml.Linq.XContainer
            //method: System.Collections.Generic.IEnumerable`1[System.Xml.Linq.XElement] Descendants()
            //Did you forget to add the [Script] attribute?
            //Please double check the signature!


            //var w = new XElement("textarea", "DescendantsAndSelf:");


            //page.DescendantsAndSelf().WithEach(
            //    x =>
            //    {

            //        w.Value += "\n" + x.Name.LocalName;

            //        //.Where(x => x.Name.LocalName == "iframe")
            //    }
            //);
            //page.DescendantsAndSelf().Where(x => x.Name.LocalName == "iframe").WithEach(
            //    iframe =>
            //    {
            //        iframe.Attribute("src").Value = "http://idea-remixer.tumblr.com";
            //    }
            //);

            //page.Element("body").Add(
            //    w
            //);

            // as from a WebWorker or from the server we should be able
            // to construct HTML DOM objects yet, we may have issues doing the events for them.




            webView.loadString(page.ToString(), "text/html");
        }

    }
}

