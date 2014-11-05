using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionWebApplication1
{
    //class ApplicationWebServiceHandler
    public sealed partial class ApplicationWebService
    //: ISoundCloudTracksDownload
    {
        //Y:\PromotionWebApplication1.ApplicationWebService\staging.java\web\files
        //C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe  -encoding UTF-8 -cp Y:\PromotionWebApplication1.ApplicationWebService\staging.java\web\release;C:\util\appengine-java-sdk-1.9.9\lib\impl\*;C:\util\appengine-java-sdk-1.9.9\lib\shared\* -d "Y:\PromotionWebApplication1.ApplicationWebService\staging.java\web\release" @"Y:\PromotionWebApplication1.ApplicationWebService\staging.java\web\files"


        public void Hello(string data, StringAction result)
        {
            result(data + " hello");
            result(data + " world");
        }

        public void GetTitleFromServer(StringAction result)
        {
            var r = new Random();

            var Targets = new[]
			{
				"javascript",
				"java",
				"actionscript",
				"php"
			};

            result("jsc solutions - C# to " + Targets[r.Next(0, Targets.Length)]);

            // should we add timing information if we use Thread.Sleep to the results?

        }

        //public void ThreeDWarehouse(XElementAction y)
        //{
        //    y(new ThreeDWarehouse().ToXElement());
        //}

        /*ISoundCloudTracksDownload. not supported yet ? */
        //public void SoundCloudTracksDownload(string page, Services.SoundCloudTrackFound yield)
        //{
        //    new Services.SoundCloudTracks().SoundCloudTracksDownload(page, yield);
        //}

#if !DEBUG
        public void DownloadSDK(WebServiceHandler h)
        {
            DownloadSDKFunction.DownloadSDK(h);

        }
#endif



#if Bulldog
        public void CodeGenerator(WebServiceHandler h)
        {


            const string _java = "/java/";
            const string _java_zip = "/java.zip/";

            if (h.Context.Request.Path.StartsWith(_java_zip))
            {
                var TypesList = h.Context.Request.Path.Substring(_java_zip.Length);

                DownloadJavaZip(h, TypesList);
            }

            if (h.Context.Request.Path.StartsWith(_java))
            {
                var Type = h.Context.Request.Path.Substring(_java.Length);

                var p = new global::Bulldog.Server.CodeGenerators.Java.DefinitionProvider(
                    Type,
                    new WebClient().DownloadString
                )
                {
                    Server = "www.jsc-solutions.net"
                };

                h.Context.Response.ContentType = "text/plain";
                h.Context.Response.Write(p.GetString());
                h.CompleteRequest();
            }
        }
       

        private static void DownloadJavaZip(WebServiceHandler h, string TypesList)
        {
            var Types = TypesList.Split(',');
            var zip = new ZIPFile();

            foreach (var item in Types)
            {
                var w = new StringBuilder();



                var p = new global::Bulldog.Server.CodeGenerators.Java.DefinitionProvider(
                    item,
                    new WebClient().DownloadString
                )
                {
                    Server = "www.jsc-solutions.net"
                };

                zip.Add(
                    item.Replace(".", "/") + ".cs",
                    p.GetString()
                );
            }

            // http://www.ietf.org/rfc/rfc2183.txt

            h.Context.Response.ContentType = ZIPFile.ContentType;

            h.Context.Response.AddHeader(
                "Content-Disposition",
                "attachment; filename=" + TypesList + ".zip"
            );


            var bytes = zip.ToBytes();

            h.Context.Response.OutputStream.Write(bytes, 0, bytes.Length);

            h.CompleteRequest();
        }
#endif
    }

}
