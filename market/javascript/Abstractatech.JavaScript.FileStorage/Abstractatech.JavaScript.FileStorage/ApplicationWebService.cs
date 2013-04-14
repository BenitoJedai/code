using Abstractatech.JavaScript.FileStorage.Schema;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Abstractatech.JavaScript.FileStorage
{
    public delegate void AtFile(
        string ContentKey,
        string ContentValue,
        string ContentType
    );

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        //[javac] S:\src\Abstractatech\JavaScript\FileStorage\ApplicationWebService___c__DisplayClass23.java:30: data_FileStorageLog has private access in Abstractatech.JavaScript.FileStorage.ApplicationWebService
        //[javac]         this.CS___8__locals22.__4__this.data_FileStorageLog.Insert(insert0, null);


        public FileStorageTable data_FileStorage = new FileStorageTable();
        public FileStorageLogTable data_FileStorageLog = new FileStorageLogTable();

        public void DeleteAsync(string Key, Action done = null)
        {
            data_FileStorage.Delete(
                new FileStorageQueries.Delete
                {
                    ContentKey = int.Parse(Key),
                }
            );

            data_FileStorageLog.Insert(
                     new FileStorageLogQueries.Insert { ContentValue = "removed " + new { Key } }
                 );

            if (done != null)
                done();
        }


        public void UpdateAsync(string Key, string Value, Action done = null)
        {
            data_FileStorage.Update(
                new FileStorageQueries.Update
                {
                    ContentKey = int.Parse(Key),
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
                        ContentKey = xx.ContentKey;

                    string
                        ContentValue = xx.ContentValue,
                        ContentType = xx.ContentType;

                    Console.WriteLine(new { ContentKey, ContentValue, ContentType });


                    y(
                        ContentKey: "" + ContentKey,
                        ContentValue: ContentValue,
                        ContentType: ContentType
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
                data_FileStorage.SelectBytes(
                    value: int.Parse(filepath),
                    yield: reader =>
                    {
                        dynamic r = reader;

                        string ContentValue = r.ContentValue;
                        string ContentType = r.ContentType;

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


                        h.Context.Response.ContentType = ContentType;

                        // http://www.webscalingblog.com/performance/caching-http-headers-cache-control-max-age.html
                        h.Context.Response.AddHeader("Cache-Control", "max-age=2592000");
                        h.Context.Response.AddHeader("Content-Length", "" + imageData.Length);

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
