using DropFileIntoSQLite.Definitions;
using DropFileIntoSQLite.Library.Synergy;
using DropFileIntoSQLite.Queries;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace DropFileIntoSQLite
{
    // left, top, width, height, scale

    #region Table1Meta_MetaKey
    public enum Table1Meta_MetaKey { };
    public static partial class XX
    {
        public static void CreateTable(this Table1Meta_MetaKey e, SQLiteConnection c)
        {
            {
                using (var reader = new SQLiteCommand(
                    CreateTable1MetaQuery.GetSource()
                    , c).ExecuteReader())
                {

                }
            }
        }

        public static void CreateTable(this Table1_ContentKey e, SQLiteConnection c)
        {
            {
                using (var reader = new SQLiteCommand(
                    CreateTable1MetaQuery.GetSource()
                    , c).ExecuteReader())
                {

                }
            }
        }
    }
    #endregion

    #region Table1_ContentKey
    public enum Table1_ContentKey { };
    [Description("Client side")]
    public static class Table1AsyncExtensions
    {
        public static Table1_ContentKey SetLeft(this Table1_ContentKey ContentKey, int value)
        {
            new ApplicationWebService().Table1AsyncExtensions_SetMetaValue(
                "" + ContentKey,
                "Left",
                "" + value,
                delegate
                {

                }
            );

            return ContentKey;
        }

        public static Table1_ContentKey SetTop(this Table1_ContentKey ContentKey, int value)
        {
            new ApplicationWebService().Table1AsyncExtensions_SetMetaValue(
                "" + ContentKey,
                "Top",
                "" + value,
               delegate
               {

               }
           );

            return ContentKey;
        }

        public static Table1_ContentKey SetWidth(this Table1_ContentKey ContentKey, int value)
        {
            new ApplicationWebService().Table1AsyncExtensions_SetMetaValue(
                "" + ContentKey,
                "Width",
                "" + value,
               delegate
               {

               }
           );

            return ContentKey;
        }

        public static Table1_ContentKey SetHeight(this Table1_ContentKey ContentKey, int value)
        {
            new ApplicationWebService().Table1AsyncExtensions_SetMetaValue(
                "" + ContentKey,
                "Height",
                "" + value,
               delegate
               {

               }
           );

            return ContentKey;
        }

        public static Table1_ContentKey Delete(this Table1_ContentKey ContentKey)
        {
            new ApplicationWebService().DeleteFileAsync(
                "" + ContentKey,
                delegate
                {

                }
            );

            return ContentKey;
        }

        public delegate void AtFile(
            Table1_ContentKey ContentKey,
            long Length,

             string Left,
             string Top,
             string Width,
             string Height
         );

        public static Table1_ContentKey WithEach(this Table1_ContentKey x, AtFile y)
        {
            new ApplicationWebService().EnumerateFilesAsync("",
                   (ContentKey, ContentBytesLength, Left, Top, Width, Height) =>
                   {
                       var __ContentKey = (Table1_ContentKey)int.Parse(ContentKey);

                       y(__ContentKey, int.Parse(ContentBytesLength), Left, Top, Width, Height);
                   }
            );

            return x;
        }
    }
    #endregion



    public partial class ApplicationWebService
    {
        public void Table1AsyncExtensions_SetMetaValue(
            string DeclaringType,
            string MemberName,
            string MemberValue,
            Action<string> yield)
        {
            using (var c = DataSource.ToConnection())
            {
                c.Open();

                default(Table1Meta_MetaKey).CreateTable(c);

                //xmd.ExecuteAsync

                var cmd = new SQLiteCommand(
                    Queries.SetMetaValueQuery.GetSource(),
                    c
                );

                cmd.Parameters.AddWithValue(
                    new SetMetaValueQuery
                    {
                        MemberName = MemberName,
                        MemberValue = MemberValue,
                        DeclaringType = long.Parse(DeclaringType)
                    }
                );

                cmd.ExecuteReader().Dispose();


                Console.WriteLine(new { DeclaringType, MemberName, MemberValue });

            }

            yield("");
        }


    }

    public static partial class XX
    {
        public static SQLiteConnection ToConnection(this string DataSource)
        {
            var csb = new SQLiteConnectionStringBuilder
            {
                DataSource = DataSource,
                Version = 3
            };

            var c = new SQLiteConnection(csb.ConnectionString);

            return c;
        }

        public static void ExecuteReaderForEach(this SQLiteCommand cmd, Action<dynamic> y)
        {
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    y(new DynamicDataReader(reader));
                }
            }
        }
    }

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService
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
                    using (var reader = new SQLiteCommand(
                        CreateTable1Query.GetSource()
                        , c).ExecuteReader())
                    {
                    }
                }

                {
                    //var sql = "delete from Table1 where ContentKey = ?";
                    var cmd = new SQLiteCommand(
                        DeleteContentBytesQuery.GetSource()
                        , c);
                    cmd.Parameters.AddWithValue(
                        new DeleteContentBytesQuery { ContentKey = ContentKey }
                    );


                    using (var reader = cmd.ExecuteReader())
                    {

                    }
                }
            }
            #endregion
        }

        public void EnumerateFilesAsync(string e, AtFile y)
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
                    using (var reader = new SQLiteCommand(
                        CreateTable1Query.GetSource(), c).ExecuteReader())
                    {
                    }
                }

                default(Table1Meta_MetaKey).CreateTable(c);


                {
                    //var sql = "select ContentKey, ContentBytes";

                    //sql += ", coalesce((select MemberValue from Table1Meta where MemberName = 'Left' and DeclaringType = ContentKey order by MetaKey desc), '0') as Left";
                    //sql += ", coalesce((select MemberValue from Table1Meta where MemberName = 'Top' and DeclaringType = ContentKey order by MetaKey desc), '0') as Top";
                    //sql += ", coalesce((select MemberValue from Table1Meta where MemberName = 'Width' and DeclaringType = ContentKey order by MetaKey desc), '0') as Width";
                    //sql += ", coalesce((select MemberValue from Table1Meta where MemberName = 'Height' and DeclaringType = ContentKey order by MetaKey desc), '0') as Height";

                    //sql += " from Table1";

                    new SQLiteCommand(
                        FromTable1SelectQuery.GetSource(), c
                    ).ExecuteReaderForEach(
                        xx =>
                        {
                            //var reader = xx as IDataReader;

                            string
                                Left = xx.Left,
                                Top = xx.Top,
                                Width = xx.Width,
                                Height = xx.Height;

                            long
                                ContentKey = xx.ContentKey,
                                ContentBytesLength = xx.ContentBytesLength;

                            //var Left = reader.GetString(reader.GetOrdinal("Left"));
                            //var Top = reader.GetString(reader.GetOrdinal("Top"));
                            //var Width = reader.GetString(reader.GetOrdinal("Width"));
                            //var Height = reader.GetString(reader.GetOrdinal("Height"));

                            //var ContentKey = reader.GetInt64(reader.GetOrdinal("ContentKey"));

                            Console.WriteLine(new { ContentKey, Left, Top });

                
                            y(
                                ContentKey: "" + ContentKey,
                                Length: "" + ContentBytesLength,
                                Left: Left,
                                Top: Top,
                                Width: Width,
                                Height: Height
                            );

                   
                        }
                    );
                }
            }
            #endregion
        }

        public delegate void AtFile(
            /* Table1_ContentKey */ string ContentKey,
            /* long */ string Length,

            string Left,
            string Top,
            string Width,
            string Height
        );

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

                    using (var c = DataSource.ToConnection())
                    {
                        c.Open();

                        default(Table1_ContentKey).CreateTable(c);



                        {
                            //var sql = "insert into Table1 (ContentValue, ContentBytes) values (?, ?)";
                            var cmd = new SQLiteCommand(
                                UploadContentBytesQuery.GetSource()
                                , c);

                            cmd.Parameters.AddWithValue(
                                new UploadContentBytesQuery
                                {
                                    ContentValue = item.FileName,
                                    ContentBytes = bytes
                                }
                            );


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

                    //var sql = "select ContentBytes from Table1 where ContentKey = ?";
                    var cmd = new SQLiteCommand(GetContentBytesQuery.GetSource(), c);
                    cmd.Parameters.AddWithValue(
                        new GetContentBytesQuery { ContentKey = filepath }
                    );

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

                                // can we stream it to the client instead?
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
