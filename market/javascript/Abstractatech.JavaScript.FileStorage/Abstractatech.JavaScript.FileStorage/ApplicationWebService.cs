using Abstractatech.JavaScript.FileStorage.Schema;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Abstractatech.JavaScript.FileStorage
{
    public delegate void AtFile(
        long ContentKey,
        string ContentValue,
        string ContentType,
        long ContentBytesLength
    );

    public interface IApplicationWebServiceX
    {
        void DeleteAsync(long Key, Action done = null);
        void UpdateAsync(long Key, string Value, Action done = null);
        void EnumerateFilesAsync(AtFile y, Action<string> done = null);
        void GetTransactionKeyAsync(Action<string> done = null);
    }

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService : IApplicationWebServiceX
    {
        //     19e0:01:01 RewriteToAssembly error: System.InvalidOperationException: Sequence contains no elements
        //at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
        //at jsc.meta.Commands.Rewrite.RewriteToJavaScriptDocument.WebServiceForJavaScript.<>c__DisplayClass274.<WriteTypeDefinition>b__269()

        //        [javac] Compiling 592 source files to V:\bin\classes
        //[javac] V:\src\Abstractatech\JavaScript\FileStorage\ApplicationWebService___c__DisplayClass2b.java:32: data_FileStorageLog has private access in Abstractatech.JavaScript.FileStorage.ApplicationWebService
        //[javac]         this.CS___8__locals2a.__4__this.data_FileStorageLog.Insert(insert0, null);
        //[javac]                                        ^
        //[javac] V:\src\Abstractatech\JavaScript\FileStorage\ApplicationWebService___c__DisplayClass2d.java:136: data_FileStorage has private access in Abstractatech.JavaScript.FileStorage.ApplicationWebService
        //[javac]             table16 = this.CS___8__locals2a.__4__this.data_FileStorage;
        //[javac]                                                      ^

        FileStorageTable data_FileStorage = new FileStorageTable();
        FileStorageLogTable data_FileStorageLog = new FileStorageLogTable();

        public void DeleteAsync(long Key, Action done = null)
        {
            data_FileStorage.Delete(
                new FileStorageQueries.Delete
                {
                    ContentKey = (int)(Key),
                }
            );

            data_FileStorageLog.Insert(
                     new FileStorageLogQueries.Insert { ContentValue = "removed " + new { Key } }
                 );

            if (done != null)
                done();
        }


        public void UpdateAsync(long Key, string Value, Action done = null)
        {
            data_FileStorage.Update(
                new FileStorageQueries.Update
                {
                    ContentKey = (int)(Key),
                    ContentValue = Value
                }
            );

            if (done != null)
                done();
        }

        public void EnumerateFilesAsync(AtFile y, Action<string> done = null)
        {
            Console.WriteLine("EnumerateFilesAsync");


            data_FileStorage.SelectAll(
                xx =>
                {
                    long
                        ContentKey = xx.ContentKey,
                        ContentBytesLength = xx.ContentBytesLength;

                    string
                        ContentValue = xx.ContentValue,
                        ContentType = xx.ContentType;

                    Console.WriteLine(new { ContentKey, ContentValue, ContentType });


                    y(
                        ContentKey: ContentKey,
                        ContentValue: ContentValue,
                        ContentType: ContentType,
                        ContentBytesLength: ContentBytesLength
                    );
                }
            );


            this.data_FileStorageLog.SelectTransaction(
                xx =>
                {
                    long
                       ContentKey = xx.ContentKey;

                    if (done != null)
                        done("" + ContentKey);
                }
            );
        }



        public void GetTransactionKeyAsync(Action<string> done = null)
        {

            this.data_FileStorageLog.SelectTransaction(
               xx =>
               {
                   long
                      ContentKey = xx.ContentKey;

                   if (done != null)
                       done("" + ContentKey);
               }
           );
        }


        public void InternalHandler(WebServiceHandler h)
        {
            //var WebServiceMethod = h.Context.Request.Headers["WebServiceMethod"];

            #region FileStorageUpload
            if (h.Context.Request.HttpMethod == "POST")
                if (h.Context.Request.Path == "/FileStorageUpload")
                {
                    // Maximum request length exceeded.

                    var Files = h.Context.Request.Files.AllKeys.Select(k => h.Context.Request.Files[k]);
                    foreach (HttpPostedFile item in Files)
                    {
                        //var bytes = item.InputStream.ReadToEnd();
                        var bytes = item.InputStream.ToBytes();
                        var FileName = item.FileName;

                        Console.WriteLine("FileStorageUpload: " +
                            new { item.ContentType, FileName, item.ContentLength, bytes.Length }
                        );

                        //                        { Message = constraint failed

                        //FileStorageTable.ContentType may not be NULL,

                        var __value = new FileStorageQueries.Insert
                        {
                            //ContentValue = item.FileName,
                            ContentValue = FileName,
                            ContentType = item.ContentType,
                            ContentBytes = bytes
                        };


                        data_FileStorage.Insert(
                            value: __value,
                            yield:
                            LastInsertRowId =>
                            {
                                data_FileStorageLog.Insert(
                                    new FileStorageLogQueries.Insert { ContentValue = "added " + new { LastInsertRowId, FileName } }
                                );

                                Console.WriteLine("FileStorageUpload: " + new { LastInsertRowId });
                                //ok.Add(new XElement("ContentKey", "" + LastInsertRowId));
                            }
                        );



                    }

                    //FileStorageUpload: { ContentType = image/jpeg, FileName = civilizationworld.jpg, ContentLength = 121059, Length = 121059 }
                    //FileStorageUpload: { LastInsertRowId = 1 }
                    //FileStorageUpload: { ContentType = image/jpeg, FileName = 6a00d83459e20169e200e54f700c828834-800wi.jpg, ContentLength = 770558, Length = 770558 }
                    //FileStorageUpload: { LastInsertRowId = 2 }


                    h.Context.Response.StatusCode = 204;
                    h.Context.Response.Write("ok");
                    h.CompleteRequest();
                    return;
                }
            #endregion



            #region /io/
            var io = "/io/";
            var path = h.Context.Request.Path;
            //Console.WriteLine("is io? " + new { path, io });
            if (path.StartsWith(io))
            {


                var filepath = path.SkipUntilIfAny(io).TakeUntilIfAny("/");


                // is this still a problem?
                filepath = filepath.Replace("%20", " ");



                Console.WriteLine("SelectBytes " + new { filepath });

                //W/CursorWindow(12675): Window is full: requested allocation 2419936 bytes, free space 2096696 bytes, window size 2097152 bytes
                //W/CursorWindow(12675): Window is full: requested allocation 2419936 bytes, free space 2096696 bytes, window size 2097152 bytes
                //E/CursorWindow(12675): Failed to read row 0, column 1 from a CursorWindow which has 0 rows, 3 columns.
                //I/System.Console(12675): TryGetMember error: { Name = ContentValue, Message = GetFieldType fault { ordinal = 1, t = 0 }, StackTrace
                // http://stackoverflow.com/questions/1407442/android-sqlite-and-huge-data-sets
                // http://stackoverflow.com/questions/11863024/android-cursor-window-is-full

                data_FileStorage.SelectBytes(
                    value: int.Parse(filepath),
                    yield: reader =>
                    {
                        dynamic r = reader;

                        long ContentBytesLength = r.ContentBytesLength;

                        string ContentValue = r.ContentValue;
                        string ContentType = r.ContentType;

                        // http://stackoverflow.com/questions/12716859/retrieve-large-blob-from-android-sqlite-database
                        // http://stackoverflow.com/questions/5406429/cursor-size-limit-in-android-sqlitedatabase

                        h.Context.Response.ContentType = ContentType;

                        // http://www.webscalingblog.com/performance/caching-http-headers-cache-control-max-age.html
                        h.Context.Response.AddHeader("Cache-Control", "max-age=2592000");
                        h.Context.Response.AddHeader("Content-Length", "" + ContentBytesLength);


                        // android makes us to chuncked loading manually!
                        for (int i = 0; i < ContentBytesLength; i += 1000000)
                        {
                            var __SelectBytesRange = new FileStorageQueries.SelectBytesRange
                                {
                                    ContentKey = int.Parse(filepath),
                                    ContentBytesRangeOffset = i,
                                    ContentBytesRangeLength = 1000000.Min((int)ContentBytesLength - i + 1)
                                };

                            // http://stackoverflow.com/questions/5406429/cursor-size-limit-in-android-sqlitedatabase
                            data_FileStorage.SelectBytesRange(
                                value: __SelectBytesRange,
                                yield: rangereader =>
                                {
                                    var ordinal = rangereader.GetOrdinal("ContentBytes");
                                    //var t = rangereader.GetFieldType(ordinal);


                                    // 'System.Data.SQLite.SQLiteDataReader.GetSQLiteType(int)' is inaccessible due to its protection level

                                    //var GetSQLiteType = rangereader.DataReader.GetType().GetMethod("GetSQLiteType", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

                                    //// ex = {"Cannot invoke a non-delegate type"}
                                    //object SQLiteType = GetSQLiteType.Invoke(rangereader.DataReader, new object[] { ordinal });

                                    //rangereader.DataReader

                                    // Get size of image data–pass null as the byte array parameter
                                    long bytesize = rangereader.GetBytes(ordinal, 0, null, 0, 0);
                                    byte[] imageData = new byte[bytesize];


                                    //                                  System.InvalidCastException was caught
                                    //HResult=-2147467262
                                    //Message=Specified cast is not valid.
                                    //Source=System.Data.SQLite
                                    //StackTrace:
                                    //     at System.Data.SQLite.SQLiteDataReader.VerifyType(Int32 i, DbType typ)
                                    //     at System.Data.SQLite.SQLiteDataReader.GetBytes(Int32 i, Int64 fieldOffset, Byte[] buffer, Int32 bufferoffset, Int32 length)
                                    //     at ScriptCoreLib.Shared.Data.DynamicDataReader.GetBytes(Int32 i, Int64 fieldOffset, Byte[] buffer, Int32 bufferoffset, Int32 length)
                                    //     at Abstractatech.JavaScript.FileStorage.ApplicationWebService.<>c__DisplayClass2d.<>c__DisplayClass32.<InternalHandler>b__27(IDataReader rangereader)
                                    //     at Abstractatech.JavaScript.FileStorage.Schema.XX.WithEachReader(SQLiteDataReader reader, Action`1 y)
                                    //     at Abstractatech.JavaScript.FileStorage.Schema.FileStorageTable.<>c__DisplayClass9.<SelectBytesRange>b__8(SQLiteConnection c)
                                    //     at Abstractatech.JavaScript.FileStorage.Schema.XX.<>c__DisplayClass1.<AsWithConnection>b__0(Action`1 y)
                                    //InnerException: 

                                    // ReadBytes will explicitly throw an exception if the affinity of the column is not "BLOB", (otherwise known as none). Something is definitely wrong either with the table definition, or the column being referenced

                                    var len = 0L;

                                    try
                                    {
                                        // this was expensive to figure out:P
                                        len = rangereader.GetBytes(ordinal, 0, imageData, 0, (int)bytesize); ;
                                    }
                                    catch
                                    {
                                        Debugger.Break();

                                    }
                                    Console.WriteLine(new { r.ContentValue, r.ContentType, i, len });
                                    h.Context.Response.OutputStream.Write(imageData, 0, (int)len);

                                }
                            );
                        }

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
