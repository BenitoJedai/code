using DropFileIntoSQLite.Schema;
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
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace DropFileIntoSQLite
{
    // left, top, width, height, scale

    public delegate void AtFile(
        /* Table1_ContentKey */
        Table1_ContentKey ContentKey,
        long Length,

        int Left,
        int Top,
        int Width,
        int Height
    );


    #region Table1Meta_MetaKey
    public enum Table1Meta_MetaKey { };
    #endregion

    #region Table1_ContentKey
    public enum Table1_ContentKey { };
    [Description("Client side")]
    public static class Table1AsyncExtensions
    {
        public static Task<Table1MetaQueries.InsertMeta> SetLeft(this Table1_ContentKey ContentKey, int value)
        {
            return new ApplicationWebService().Table1AsyncExtensions_SetMetaValue(
                new Table1MetaQueries.InsertMeta
                {
                    MemberName = "Left",
                    MemberValue = "" + value,
                    DeclaringType = (int)ContentKey
                }
            );
        }

        public static Task<Table1MetaQueries.InsertMeta> SetTop(this Table1_ContentKey ContentKey, int value)
        {
            return new ApplicationWebService().Table1AsyncExtensions_SetMetaValue(
                new Table1MetaQueries.InsertMeta
                {
                    MemberName = "Top",
                    MemberValue = "" + value,
                    DeclaringType = (int)ContentKey
                }
            );
        }

        public static Task<Table1MetaQueries.InsertMeta> SetWidth(this Table1_ContentKey ContentKey, int value)
        {
            return new ApplicationWebService().Table1AsyncExtensions_SetMetaValue(
              new Table1MetaQueries.InsertMeta
              {
                  MemberName = "Width",
                  MemberValue = "" + value,
                  DeclaringType = (int)ContentKey
              }
          );
        }

        public static Task<Table1MetaQueries.InsertMeta> SetHeight(this Table1_ContentKey ContentKey, int value)
        {
            return new ApplicationWebService().Table1AsyncExtensions_SetMetaValue(
                new Table1MetaQueries.InsertMeta
                {
                    MemberName = "Height",
                    MemberValue = "" + value,
                    DeclaringType = (int)ContentKey
                }
            );
        }

        public static Task<Table1_ContentKey> Delete(this Table1_ContentKey ContentKey)
        {
            return new ApplicationWebService().DeleteFileAsync(ContentKey);
        }



        public static Table1_ContentKey WithEach(this Table1_ContentKey x, AtFile y)
        {
            new ApplicationWebService().EnumerateFilesAsync(
                (ContentKey, ContentBytesLength, Left, Top, Width, Height) =>
                {
                    y(ContentKey, ContentBytesLength, Left, Top, Width, Height);
                }
            );

            return x;
        }
    }
    #endregion



    public partial class ApplicationWebService
    {
        //        0c38:02:01 007a:005a DropFileIntoSQLite.ApplicationWebService.AndroidActivity create DropFileIntoSQLite.ApplicationWebService::ScriptCoreLib.Ultra.WebService.InternalWebMethodWorker
        //0c38:02:01 LoaderStrategyImplementation CurrentDomain_AssemblyResolve: DropFileIntoSQLite, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        //0c38:02:01 RewriteToAssembly error: System.IO.FileNotFoundException: Could not load file or assembly 'DropFileIntoSQLite, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.
        //File name: 'DropFileIntoSQLite, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
        //   at System.Signature.GetSignature(Void* pCorSig, Int32 cCorSig, RuntimeFieldHandleInternal fieldHandle, IRuntimeMethodInfo methodHandle, RuntimeType declaringType)
        //   at System.Reflection.RuntimeMethodInfo.FetchNonReturnParameters()
        //   at System.Reflection.RuntimeMethodInfo.GetParameters()
        //   at ScriptCoreLib.Extensions.MethodBaseExtensions.GetParameterTypes(MethodBase e) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\MethodBaseExtensions.cs:line 28

        public Task<Table1MetaQueries.InsertMeta> Table1AsyncExtensions_SetMetaValue(


             Table1MetaQueries.InsertMeta value

            //Table1_ContentKey DeclaringType,

            //string MemberName,
            //string MemberValue
            )
        {
            return new Table1().InsertMeta(

                value

                //new Table1MetaQueries.InsertMeta
                //{
                //    MemberName = MemberName,
                //    MemberValue = MemberValue,
                //    DeclaringType = (int)DeclaringType
                //}
            );

            //Console.WriteLine(new { DeclaringType, MemberName, MemberValue });

        }


    }


    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public  partial class ApplicationWebService
    {


#if FUTURE
        public void XUpload(Blob f, Action<string> y)
        {
            // this is how a recieving of a file should look like.
        }
#endif

        //public const string DataSource = "SQLiteWithDataGridView51.sqlite";
        //public const string DataSource = "SQLiteWithDataGridView57.sqlite";

        public Task<Table1_ContentKey> DeleteFileAsync(Table1_ContentKey ContentKey)
        {
            new Table1().Delete((int)ContentKey);

            return ContentKey.ToTaskResult();
        }

        public void EnumerateFilesAsync(AtFile y)
        {
            Console.WriteLine("EnumerateFilesAsync");

            new Table1().SelectAll(
                 xx =>
                 {

                     int
                         Left = int.Parse(xx.Left),
                         Top = int.Parse(xx.Top),
                         Width = int.Parse(xx.Width),
                         Height = int.Parse(xx.Height);

                     long
                         ContentKey = xx.ContentKey,
                         ContentBytesLength = xx.ContentBytesLength;

                     Console.WriteLine(new { ContentKey, Left, Top });

                     y(
                         ContentKey: (Table1_ContentKey)ContentKey,
                         Length: ContentBytesLength,
                         Left: Left,
                         Top: Top,
                         Width: Width,
                         Height: Height
                     );


                 }
            );

        }


        public void InternalHandler(WebServiceHandler h)
        {
            if (h.IsDefaultPath)
                return;


            #region /upload
            if (h.Context.Request.Path == "/upload")
            {
                Console.WriteLine("enter upload");


                var TextContent = h.Context.Request.Form["TextContent"];

                //Console.WriteLine(TextContent);


                var ok = new XElement("ok");

                var Files = h.Context.Request.Files.AllKeys.Select(k => h.Context.Request.Files[k]);
                foreach (HttpPostedFile item in Files)
                {
                    //var bytes = item.InputStream.ReadToEnd();
                    var bytes = item.InputStream.ToBytes();
                    var FileName = item.FileName;

                    Console.WriteLine(
                        new { item.ContentType, FileName, item.ContentLength, bytes.Length }
                    );

                    // http://code.activestate.com/recipes/252531-storing-binary-data-in-sqlite/


                    var t = new Table1();

                    // jsc cannot handle method call parameter with initializer and a delegate?
                    var __value = new Table1Queries.Insert
                        {
                            //ContentValue = item.FileName,
                            ContentValue = FileName,
                            ContentBytes = bytes
                        };

                    Console.WriteLine("before insert");

                    t.Insert(
                        value: __value,
                        yield:
                        LastInsertRowId =>
                        {
                            Console.WriteLine("after insert " + new { LastInsertRowId });

                            ok.Add(new XElement("ContentKey", "" + LastInsertRowId));
                        }
                    );

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
            Console.WriteLine("is io? " + new { path, io });
            if (path.StartsWith(io))
            {


                var filepath = path.SkipUntilIfAny(io);


                // is this still a problem?
                filepath = filepath.Replace("%20", " ");


                var t = new Table1();

                Console.WriteLine("SelectBytes " + new { filepath });
                t.SelectBytes(
                    value: int.Parse(filepath),
                    yield: reader =>
                    {
                        dynamic r = reader;

                        string ContentValue = r.ContentValue;

                        #region ContentBytes
                        // Get size of image data–pass null as the byte array parameter
                        long bytesize = reader.GetBytes(reader.GetOrdinal("ContentBytes"), 0, null, 0, 0);
                        //var chunkSize = 4096 * 4;
                        var chunkSize = (int)bytesize;

                        Console.WriteLine("SelectBytes " + new { bytesize });


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

                        #endregion

                        if (ContentValue.EndsWith(".pdf"))
                        {
                            h.Context.Response.ContentType = "application/pdf";
                        }
                        else
                        {
                            h.Context.Response.ContentType = "image/jpg";
                        }

                        // http://www.webscalingblog.com/performance/caching-http-headers-cache-control-max-age.html
                        h.Context.Response.AddHeader("Cache-Control", "max-age=2592000");
                        h.Context.Response.AddHeader("Content-Length", "" + imageData.Length);

                        // send all the bytes

                        //                        W/DownloadManager(18100): Aborting request for download 578: can't know size of download, giving up
                        //V/SnapshotDownloadReceiver(14586): Starting service from intent: Intent { act=com.google.android.apps.chrome.snapshot.ACTION_DOWNLOAD_FINISHED cmp=com.chrome.beta/com.google.android.apps.chrome.snapshot.SnapshotArchiveManager (has extras) }
                        //E/SnapshotArchiveManager(14586): Failed to download file with downloadId = 578. Reason: 1004. placing job in error state.
                        //E/SnapshotArchiveManager(14586): Error setting {state = UNABLE_TO_DOWNLOAD} for {downloadId=? : [578]}: Changed rows = 0



                        h.Context.Response.OutputStream.Write(imageData, 0, imageData.Length);

                        h.CompleteRequest();

                        filepath = null;
                    }
                );

                if (filepath == null)
                    return;


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
