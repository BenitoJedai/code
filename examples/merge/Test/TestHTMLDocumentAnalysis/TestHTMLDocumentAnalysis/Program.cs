using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

            var origin = new Uri("https://zproxy.wordpress.com");
            // http://www.coralcdn.org/

            var origincc = new Uri("http://zproxy.wordpress.com.nyud.net");

            Console.WriteLine("thinking about looking at " + new { origincc });

            //  Additional information: The underlying connection was closed: An unexpected error occurred on a send.
            // Additional information: Unable to connect to the remote server

            var c = new WebClient().DownloadString(origincc);
            Console.WriteLine("about to look at " + new { c.Length });

            doc.LoadHtml(c);

            var hrefList = (
                from x in doc.DocumentNode.SelectNodes("//a")
                let u = new Uri(x.GetAttributeValue("href", "not found"))


                // we are interested in outgoing indirections
                where u.Host != origin.Host

                group u by new { u.Host }
            ).ToArray();

            Console.WriteLine("the origin has " + new { hrefList.Length } + " groups we could look at");

            //about to look at { Length = 168483 }
            //the origin has { Length = 64 }
            //groups we could look at

            foreach (var g in hrefList)
            {
                Console.WriteLine(new { g.Key.Host });

                foreach (var item in g)
                {
                    Console.WriteLine(new { item.PathAndQuery });
                }
            }

            Debugger.Break();
        }
    }
}
