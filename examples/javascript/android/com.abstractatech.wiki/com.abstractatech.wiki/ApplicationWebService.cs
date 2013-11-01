using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Data.SQLite;
using System.Linq;
using System.Xml.Linq;

namespace com.abstractatech.wiki
{
    public delegate void ystring(string e = "");

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

        const string DataSource = "MY_DATABASE2.sqlite";

        public void CountItems(string Key, Action<string> y)
        {
            //Console.WriteLine("CountItems enter");
            using (var c = OpenReadOnlyConnection())
            {
                c.Open();

                using (var reader = new SQLiteCommand(
                    "select count(*) from MY_TABLEXX where XKey ='" + Key.Replace("'", "\\'") + "'", c).ExecuteReader()
                    )
                {

                    if (reader.Read())
                    {
                        var Content = (int)reader.GetInt32(0);

                        y("" + Content);

                    }
                }

                c.Close();

            }
            //Console.WriteLine("CountItems exit");
        }


        SQLiteConnection OpenReadOnlyConnection()
        {
            return new SQLiteConnection(

           new SQLiteConnectionStringBuilder
           {
               DataSource = DataSource,
               Version = 3,
               ReadOnly = true,

           }.ConnectionString
           );
        }

        public void EnumerateItems(string Key, Action<string> y)
        {
            AddItem("/EnumerateItems", "at " + Key, delegate { });

            // Unable to open the database file
            //Console.WriteLine("EnumerateItems enter");
            using (var c = OpenReadOnlyConnection())
            {
                c.Open();

                var cmd = new SQLiteCommand("select Content from MY_TABLEXX where XKey ='" + Key.Replace("'", "\\'") + "'", c);

                using (var reader = cmd.ExecuteReader())
                {
                    // why is reader null for PHP?
                    while (reader.Read())
                    {
                        var Content = (string)reader["Content"];

                        y(Content);

                    }
                }

                c.Close();

            }
            //Console.WriteLine("EnumerateItems exit");
        }

        public void AddItem(string Key, string e, Action<string> y)
        {
            //Console.WriteLine("AddItem enter");
            using (var c = new SQLiteConnection(

             new SQLiteConnectionStringBuilder
             {
                 DataSource = DataSource,
                 Version = 3
             }.ConnectionString

             ))
            {
                c.Open();

                using (var cmd = new SQLiteCommand("create table if not exists MY_TABLEXX (XKey text not null, Content text not null)", c))
                {
                    cmd.ExecuteNonQuery();
                }

                //new SQLiteCommand("delete from MY_TABLE", c).ExecuteNonQuery();

                // The database file is locked
                // http://stackoverflow.com/questions/4348860/the-database-file-is-locked-with-system-data-sqlite

                new SQLiteCommand("insert into MY_TABLEXX (XKey, Content) values ('" + Key.Replace("'", "\\'") + "', '" + e.Replace("'", "\\'") + "')", c).ExecuteNonQuery();
                //new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 2')", c).ExecuteNonQuery();
                //new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 3')", c).ExecuteNonQuery();
                //new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 4')", c).ExecuteNonQuery();
                //new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 5')", c).ExecuteNonQuery();



                c.Close();
            }

            // Send it back to the caller.
            y(e);
            //Console.WriteLine("AddItem exit");
        }

        public void SaveChanges(string path, XElement c, ystring y)
        {
            //Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(path);
            //Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(c.ToString());
            //Console.ForegroundColor = ConsoleColor.Gray;

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
            // all paths are ok
            var Other = h.Applications[0];

            h.Context.Response.ContentType = "text/html";

            var Key = h.Context.Request.Path;

            //Console.WriteLine(Key);


            // revert ASP.NET 
            if (Key == "/default.htm")
                Key = "/";

            var xml = XElement.Parse(Other.PageSource);



            xml.Element("div").Element("div").Element("h1").Value = Key;
            //xml.Element("div").Value = "Hello world";

            var Revision = @"
<div style='padding: 2em;'>
    Hello world, you are about to create a new page 
    for <b>" + Key + @"</b>
    <br />
    <a href='#edit'>Click here to edit.</a>
    <br />
    <a href='other'>or click here to create another page.</a>
    <br />
    <a href='other#edit'>or click here to create another page and start editing it.</a>
</div>";

            var RevisionNumber = 0;

            this.EnumerateItems(Key,
                Content =>
                {
                    RevisionNumber++;
                    Revision = Content;
                    //xml.Element("div").Add(new XElement("hr"));
                    //xml.Element("div").Add(XElement.Parse(Revision));
                    //xml.Element("div").ReplaceWith(XElement.Parse(Content));
                }
            );
            xml.Element("div").Element("div").Element("h4").Value = "Revision " + RevisionNumber;

            xml.Element("div").Element("output").Add(XElement.Parse(Revision));

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
