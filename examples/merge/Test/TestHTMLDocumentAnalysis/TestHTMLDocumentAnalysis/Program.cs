using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.Extensions;

namespace TestHTMLDocumentAnalysis
{
    class Program
    {
        // we have been here before, have we not
        // first order of business. commit and then reopen on the red.
        // this test should explore what do we need to
        // collect and anlyze a single wep page application 
        // including a level of indirection.
        // would we be able to visualize it?

        // there is a parser we could try out. where is it.
        // "X:\opensource\codeplex\htmlagilitypack\HtmlAgilityPack\HtmlAgilityPack.fx.4.5.csproj"
        // https://htmlagilitypack.svn.codeplex.com/svn/trunk
        // Error: XML parsing failed: (400 Bad Request)  

        // should we find out if jsc rewrite works fine on roslyn?

        static void Main(string[] args)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();

            var origin = new Uri("https://www.youtube.com/playlist?list=PL_2cq1Gbhke8KAbNKEz-HszGqNlqtGfKl");
            // second take on it
            // we cannot trust blog bost liks.
            // we could trust page1 of a channel

            // http://www.coralcdn.org/

            // Additional information: Invalid URI: The format of the URI could not be determined.

            Console.WriteLine("thinking about looking at " + new { origin });

            //  Additional information: The underlying connection was closed: An unexpected error occurred on a send.
            // Additional information: Unable to connect to the remote server

            var c = new WebClient().DownloadString(origin);
            Console.WriteLine("about to look at " + new { c.Length });

            doc.LoadHtml(c);

            var hrefList = (
                from x in doc.DocumentNode.SelectNodes("//a")
                let href = x.GetAttributeValue("href", "not found")
                //where href.StartsWith("https://www.youtube.com/watch?v=")
                where href.StartsWith("/watch?v=")
                let u = new Uri("https://www.youtube.com/watch?v=" + href.SkipUntilOrEmpty("=").TakeUntilIfAny("&amp;"))



                // we are interested in outgoing indirections
                //where u.Host != origin.Host

                //group u by new { u.Host }
                select u
            ).Distinct().ToArray();

            Console.WriteLine("the origin has " + new { hrefList.Length } + " groups we could look at");

            //about to look at { Length = 168483 }
            //the origin has { Length = 64 }
            //groups we could look at

            foreach (var g in hrefList)
            {
                //Console.WriteLine(new { g.Key.Host });

                Console.WriteLine(new { g });

                TestYouTubeExtractor.Program.DoVideo(g.ToString());
            }

            Debugger.Break();
        }
    }
}
