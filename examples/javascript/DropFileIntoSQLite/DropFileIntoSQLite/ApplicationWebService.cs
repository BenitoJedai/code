using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace DropFileIntoSQLite
{
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

#if FUTURE
        public void XUpload(Blob f, Action<string> y)
        {
            // this is how a recieving of a file should look like.
        }
#endif

        public const string DataSource = "SQLiteWithDataGridView51.sqlite";

        public void DeleteFileAsync(string ContentKey, Action<string, string> y)
        {
            #region delete
            var csb = new SQLiteConnectionStringBuilder
            {
                DataSource = DataSource,
                Version = 3
            };

            using (var c = new SQLiteConnection(csb.ConnectionString))
            {
                c.Open();

                {
                    var sql = "create table if not exists Table1 (ContentKey INTEGER PRIMARY KEY AUTOINCREMENT, ContentValue text not null, ContentBytes blob)";
                    using (var reader = new SQLiteCommand(sql, c).ExecuteReader())
                    {
                    }
                }

                {
                    var sql = "delete from Table1 where ContentKey = ?";
                    var cmd = new SQLiteCommand(sql, c);
                    cmd.Parameters.AddWithValue("", ContentKey);


                    using (var reader = cmd.ExecuteReader())
                    {
                       
                    }
                }
            }
            #endregion
        }

        public void EnumerateFilesAsync(string e, Action<string, string> y)
        {
            #region read
            var csb = new SQLiteConnectionStringBuilder
            {
                DataSource = DataSource,
                Version = 3
            };

            using (var c = new SQLiteConnection(csb.ConnectionString))
            {
                c.Open();

                {
                    var sql = "create table if not exists Table1 (ContentKey INTEGER PRIMARY KEY AUTOINCREMENT, ContentValue text not null, ContentBytes blob)";
                    using (var reader = new SQLiteCommand(sql, c).ExecuteReader())
                    {
                    }
                }

                {
                    var sql = "select ContentKey, ContentBytes from Table1";
                    var cmd = new SQLiteCommand(sql, c);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ContentKey = reader.GetInt64(reader.GetOrdinal("ContentKey"));

                            // Get size of image data–pass null as the byte array parameter
                            long bytesize = reader.GetBytes(reader.GetOrdinal("ContentBytes"), 0, null, 0, 0);
                            // Allocate byte array to hold image data
                            ////byte[] imageData = new byte[bytesize];
                            ////long bytesread = 0;
                            ////int curpos = 0;
                            ////while (bytesread < bytesize)
                            ////{
                            ////    // chunkSize is an arbitrary application defined value 
                            ////    bytesread += reader.GetBytes(0, curpos, imageData, curpos, chunkSize);
                            ////    curpos += chunkSize;
                            ////}

                            y("" + ContentKey, "" + bytesize);

                            //dataGridView1.Rows.Add(
                            //    "",
                            //    ContentValue,
                            //    "" + bytesize
                            //);
                        }
                    }
                }
            }
            #endregion
        }

        public void InternalHandler(WebServiceHandler h)
        {
            if (h.IsDefaultPath)
                return;


            #region /upload
            if (h.Context.Request.Path == "/upload")
            {


                var TextContent = h.Context.Request.Form["TextContent"];

                //Console.WriteLine(TextContent);


                var ok = new XElement("ok");

                foreach (HttpPostedFile item in h.Context.Request.Files.AllKeys.Select(k => h.Context.Request.Files[k]))
                {
                    //var bytes = item.InputStream.ReadToEnd();
                    var bytes = item.InputStream.ToBytes();

                    Console.WriteLine(
                        new { item.ContentType, item.FileName, item.ContentLength, bytes.Length }
                    );

                    #region add to db
                    // http://code.activestate.com/recipes/252531-storing-binary-data-in-sqlite/


                    var csb = new SQLiteConnectionStringBuilder
                    {
                        DataSource = DataSource,
                        Version = 3
                    };

                    using (var c = new SQLiteConnection(csb.ConnectionString))
                    {
                        c.Open();

                        {
                            var sql = "create table if not exists Table1 (ContentKey INTEGER PRIMARY KEY AUTOINCREMENT, ContentValue text not null, ContentBytes blob)";
                            using (var reader = new SQLiteCommand(sql, c).ExecuteReader())
                            {
                            }
                        }

                        {
                            var sql = "insert into Table1 (ContentValue, ContentBytes) values (?, ?)";
                            var cmd = new SQLiteCommand(sql, c);
                            cmd.Parameters.AddWithValue("", item.FileName);
                            cmd.Parameters.AddWithValue("", bytes);

                            using (var reader = cmd.ExecuteReader())
                            {
                            }

                            var LastInsertRowId = c.LastInsertRowId;

                            ok.Add(new XElement("ContentKey", "" + LastInsertRowId));
                        }
                    }
                    #endregion
                }


                h.Context.Response.ContentType = "text/xml";

                h.Context.Response.Write(ok);

                // close
                h.CompleteRequest();
                return;
            }
            #endregion

            #region /io/
            var io = "/io/";
            var path = h.Context.Request.Path;
            if (path.StartsWith(io))
            {


                var filepath = path.SkipUntilIfAny(io);


                // is this still a problem?
                filepath = filepath.Replace("%20", " ");

                var csb = new SQLiteConnectionStringBuilder
           {
               DataSource = DataSource,
               Version = 3
           };

                using (var c = new SQLiteConnection(csb.ConnectionString))
                {
                    c.Open();

                    var sql = "select ContentBytes from Table1 where ContentKey = ?";
                    var cmd = new SQLiteCommand(sql, c);
                    cmd.Parameters.AddWithValue("", filepath);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            var chunkSize = 4096;

                            // Get size of image data–pass null as the byte array parameter
                            long bytesize = reader.GetBytes(reader.GetOrdinal("ContentBytes"), 0, null, 0, 0);
                            // Allocate byte array to hold image data
                            byte[] imageData = new byte[bytesize];
                            long bytesread = 0;
                            int curpos = 0;
                            while (bytesread < bytesize)
                            {
                                // chunkSize is an arbitrary application defined value 
                                bytesread += reader.GetBytes(reader.GetOrdinal("ContentBytes"), curpos, imageData, curpos, chunkSize);
                                curpos += chunkSize;
                            }

                            h.Context.Response.ContentType = "image/jpg";

                            // http://www.webscalingblog.com/performance/caching-http-headers-cache-control-max-age.html
                            h.Context.Response.AddHeader("Cache-Control", "max-age=2592000");

                            // send all the bytes

                            h.Context.Response.OutputStream.Write(imageData, 0, imageData.Length);



                            h.CompleteRequest();
                            return;
                        }
                    }
                }

                h.Context.Response.ContentType = "text/html";
                h.Context.Response.Write("what ya lookin for?");
                h.Context.Response.Write(new XElement("pre", filepath).ToString());
                h.CompleteRequest();
                return;
            }
            #endregion

        }
    }
}
