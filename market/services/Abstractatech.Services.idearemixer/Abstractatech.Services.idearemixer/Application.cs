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
using Abstractatech.Services.idearemixer.Design;
using Abstractatech.Services.idearemixer.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;

namespace Abstractatech.Services.idearemixer
{
    public class ApplicationLoader
    {
        // view-source:http://idea-remixer.tumblr.com/

        //<base href='http://create.jsc-solutions.net/'></base>
        //<script src='/idea-remixer.tumblr.com/view-source'></script>

        public IHTMLBase x = new IHTMLBase { href = "http://create.jsc-solutions.net/" };

        public IHTMLScript s = new IHTMLScript { src = "/idea-remixer.tumblr.com/view-source" };


        public ApplicationLoader()
        {
            // remember current base
            //page.a.href = page.audio.src;

            //   <link rel="alternate" type="application/rss+xml" href="http://idea-remixer.tumblr.com/rss">


            Action<string, Action<string, FeedContainer>> gfeeds =
                (rss, yield) =>
                {

                    IFunction.OfDelegate(yield).Export("gfeeds_yield");

                    var src = "http://www.google.com/uds/Gfeeds?callback=gfeeds_yield&scoring=h&context=0&num=250&hl=en&output=json&v=1.0&nocache=0&q=" + rss;

                    new IHTMLScript { src = src }.AttachToHead();
                };

            Action<FeedContainer> ondata =
                data =>
                {

                    //var entries =
                    //    from e in data.feed.entries
                    //    from g in e.mediaGroups
                    //    from c in g.contents
                    //    select new { c.url, e.title };



                    data.feed.entries.WithEach(
                        e =>
                        {
                            //{"responseData": {"feed":
                            // {"feedUrl":"http://idea-remixer.tumblr.com/rss","title":"idea-remixer","link":"http://idea-remixer.tumblr.com/","author":"","description":"","type":"rss20",
                            // "entries":[
                            // {"mediaGroups":[{"contents":[{"url":"http://a.tumblr.com/tumblr_mgrts0AGjU1rs64dko1.mp3","fileSize":"2628334","type":"audio/mpeg"}]}],

                            e.mediaGroups.WithEach(
                                g =>
                                {
                                    g.contents.WithEach(
                                        c =>
                                        {
                                            var a = new IHTMLAnchor { href = c.url, innerText = e.title }.AttachToHead();

                                            a.style.display = IStyle.DisplayEnum.block;

                                            Console.WriteLine(new { e.title, c.url });
                                        }
                                    );
                                }
                            );
                        }
                    );

                    // switch base

                    Console.WriteLine("data ready. will load code.");



                    x.AttachToHead();
                    s.AttachToHead();

                };

            gfeeds("http://idea-remixer.tumblr.com/rss",
                (tag, data) =>
                {
                    Console.WriteLine("got data!");
                    Console.WriteLine(new { data });

                    ondata(data);
                }
            );



        }
    }

    public sealed class FeedContainer
    {
        public Feed feed;
    }

    public sealed class Feed
    {
        public string title;
        public string link;
        public string author;
        public string description;
        public string type;
        public FeedEntry[] entries;
    }

    public sealed class FeedEntry
    {
        public string title;
        public string link;
        public string author;
        public string publishedDate;
        public string contentSnippet;
        public string content;
        public string[] categories;

        public FeedMediaGroup[] mediaGroups;
    }

    public sealed class FeedMediaGroup
    {
        public FeedMediaGroupContent[] contents;
    }

    public sealed class FeedMediaGroupContent
    {
        public string url;
    }

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

            var x = new ApplicationLoader();




        }

    }
}
