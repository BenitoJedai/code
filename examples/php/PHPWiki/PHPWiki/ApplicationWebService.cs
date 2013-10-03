using PHPWiki.Schema;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Data.SQLite;
using System.Linq;
using System.Xml.Linq;

namespace PHPWiki
{
    public delegate void ystring(string e = "");

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        // Fatal error: Call to a member function _ae7686222de9fe3bad0c1ab5daa69519_6000bb3() on a non-object in B:\inc\PHPWiki.ApplicationWebService.exe\class.PHPWiki.ApplicationWebService.php on line 101

        public void CountItems(string e, Action<string> y)
        {
            pages.Count(
                count =>
                {
                    y("" + count);
                }
            );
        }


      

        public void EnumerateItems(string Key, Action<string> y)
        {
            AddItem("/EnumerateItems", "at " + Key, delegate { });

            pages.SelectByKey(Key, y);
        }


        Pages pages = new Pages();

        public void AddItem(string Key, string e, Action<string> y)
        {
            pages.InsertContent(
                new PagesQueries.InsertContent
                {
                    Content = e,
                    XKey = Key
                }
            );

            // Send it back to the caller.
            y(e);
            //Console.WriteLine("AddItem exit");
        }

        public void SaveChanges(string path, XElement c, ystring y)
        {
#if DEBUG
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(path);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(c.ToString());
            Console.ForegroundColor = ConsoleColor.Gray;
#endif
            path = path.Replace("%20", " ");

            AddItem(path,
                c.ToString(),
                delegate
                {
                    y();
                }
            );

        }

        public void Handle(WebServiceHandler h)
        {
            if (h.Context.Request.Path == "/jsc")
            {
                h.Diagnostics();
                return;
            }

            // all paths are ok
            var Other = h.Applications[0];

            h.Context.Response.ContentType = "text/html";

            var Key = h.Context.Request.Path;

            // revert ASP.NET 
            if (Key == "/default.htm")
                Key = "/";


            //         $element2->_ae7686222de9fe3bad0c1ab5daa69519_6000bb3(_54ab04ba467ed232b7f5d1fcb949e03c_6000bc9("div"))->_ae7686222de9fe3bad0c1ab5daa69519_6000bb3(_54ab04ba467ed232b7f5d1fcb949e03c_6000bc9("h1"))->_96b56dc7b8f4203e82a367dae19437d4_6000bc0($string1);
            var xml = XElement.Parse(Other.PageSource);

            //Console.WriteLine("Other.PageSource:");
            //Console.WriteLine(Other.PageSource);


            //Console.WriteLine("xml:");
            //Console.WriteLine(xml.ToString());

            xml.Element("body").Element("div").Element("h1").Value = Key;
            //xml.Element("div").Value = "Hello world";

            var Revision = "<div>Hello world, <a href='other page#foo'>other page</a></div>";

            this.EnumerateItems(Key,
                Content =>
                {


                    Revision = Content;
                    //xml.Element("div").Add(new XElement("hr"));
                    //xml.Element("div").Add(XElement.Parse(Revision));
                    //xml.Element("div").ReplaceWith(XElement.Parse(Content));
                }
            );

            //Console.WriteLine("Revision:");
            //Console.WriteLine(Revision);

            var Revision_xml = XElement.Parse(Revision);

            //Console.WriteLine("Revision_xml:");
            //Console.WriteLine(Revision_xml.ToString());

            xml.Element("body").Element("output").Add(Revision_xml);

            //xml.Element("h3").Value = h.Context.Request.UserAgent;
            //xml.Element("h3").Value = h.Context.Request.Headers["User-Agent"];

            //h.Context.Response.Write(
            //          "<script type='text/xml' class='" + Other.TypeName + "'></script>"
            //      );


            foreach (var r in Other.References)
            {
                xml.Add(
                    new XElement("script",
                        new XAttribute("src", "/" + r.AssemblyFile + ".js"),
                        " "
                    )
                );

                //h.Context.Response.Write(
                //    "<script src='" + r.AssemblyFile + ".js'></script>"
                //);
            }

            h.Context.Response.Write(xml.ToString());

            h.CompleteRequest();
        }
    }
}
