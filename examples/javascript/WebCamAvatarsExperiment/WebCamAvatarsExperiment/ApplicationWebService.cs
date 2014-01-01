using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebCamAvatarsExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {

        public void Insert0(string base64)
        {
            Console.WriteLine(new { base64.Length });
            // { Length = 52398 }
        }

        public void Insert(Abstractatech.JavaScript.Avatar.Design.WebCamAvatarsSheet1Row y)
        {
            Console.WriteLine("Insert!!");

            //DateTimeConvertFromString { e = 1388579900081 }
            //DateTimeConvertFromInt64 { Kind = Utc, value = 1/1/2014 12:38:20 PM }
            //{ Length = 58370 }

            if (y.Avatar96gif != null)
                Console.WriteLine(new { y.Avatar96gif.Length });

            try
            {
                var avatars = new global::Abstractatech.JavaScript.Avatar.Design.WebCamAvatars.Sheet1();
                var key = avatars.Insert(y);

                var c = avatars.Count();

                Console.WriteLine(
                    new { c }
                    );
            }
            catch
            {
                // what the flip
                Debugger.Break();
            }

            //          about to load params for { WebMethod = { IsConstructor = false, MetadataToken = 06000002, Name = Insert, TypeFullName = WebCamAvatarsExperiment.ApplicationWebService, Parameters = 1 } }
            //enter invoke { WebMethod = { IsConstructor = false, MetadataToken = 06000002, Name = Insert, TypeFullName = WebCamAvatarsExperiment.ApplicationWebService, Parameters = 1 } }
            //enter NewGlobalInvokeMethod
            //check NewGlobalInvokeMethod { Name = Insert0 }
            //check NewGlobalInvokeMethod { Name = Insert }
            //enter NewGlobalInvokeMethod { Name = Insert }
            //before call NewGlobalInvokeMethod { Name = Insert }
            //enter { ConvertTypeName = Abstractatech.JavaScript.Avatar.ConvertToString$2$<0200001c> }
            //before xml parse { ConvertTypeName = Abstractatech.JavaScript.Avatar.ConvertToString$2$<0200001c> }
            //before ElementsToFields { ConvertTypeName = Abstractatech.JavaScript.Avatar.ConvertToString$2$<0200001c> }
            //ElementsToFields { Name = Key }
            //ElementsToFields { Name = Avatar640x480 }
            //ElementsToFields { Name = Avatar96gif }
            //ElementsToFields { Name = Avatar96frame0 }
            //ElementsToFields { Name = Avatar96frame1 }
            //ElementsToFields { Name = Avatar96frame2 }
            //ElementsToFields { Name = Avatar96frame3 }
            //ElementsToFields { Name = ExternalKey }
            //ElementsToFields { Name = Tag }
            //ElementsToFields { Name = Timestamp }
            //DateTimeConvertFromString { e = 1388586659840 }
            //DateTimeConvertFromInt64 { Kind = 0, value = 01.01.2014 16:30:59 }
            //Insert
            //java.lang.NullPointerException

            //before call NewGlobalInvokeMethod { Name = Insert }
            //enter { ConvertTypeName = Abstractatech.JavaScript.Avatar.ConvertToString$2$<0200001c> }
            //before xml parse { ConvertTypeName = Abstractatech.JavaScript.Avatar.ConvertToString$2$<0200001c> }
            //before ElementsToFields { ConvertTypeName = Abstractatech.JavaScript.Avatar.ConvertToString$2$<0200001c> }
            //ElementsToFields { Name = Key }
            //ElementsToFields { Name = Avatar640x480 }


        }
    }
}
