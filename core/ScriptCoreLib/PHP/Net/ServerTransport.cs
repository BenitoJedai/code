using ScriptCoreLib.PHP.IO;
using ScriptCoreLib.Shared;
using ScriptCoreLib.PHP;
using ScriptCoreLib.PHP.Runtime;

namespace ScriptCoreLib.PHP.Net
{
    [Script]
    public partial class ServerTransport<TType> : global::System.IDisposable
        where TType : class
    {

        public string Source;
        public MyTransportDescriptor<TType> Descriptor;

        public TType Data
        {
            get
            {
                if (Descriptor == null)
                    return default(TType);

                return Descriptor.Data;
            }
        }


        public bool IsFileUpload;

        public ServerTransport(bool fileupload)
        {
            IsFileUpload = fileupload;

            if (fileupload)
            {
                
                Source = Native.API.base64_decode(Native.SuperGlobals.Post[Helper.FormTemplateJSONField]);
            }
            else
            {
                Source = Native.API.file_get_contents("php://input");
            }

            Native.Log("json << stream");

            Native.Log("reading json transport data...");

            FileSystemInfo.WriteFile("json.in.log", Helper.Join("\t", Native.DateTime, Source, "\r\n"), true);

            Descriptor = JSON.Decode<MyTransportDescriptor<TType>>(Source);

            Native.Log("reading json transport data... complete! " );
            
            //Native.Log("reading json transport data... done");

            //Native.Log("dump: " + Native.DumpToString(Descriptor));
        }

        //public bool TerminateAfterStream = true;

        public void WriteToStream()
        {
            Native.Log("stream << json");

            string Source = JSONBase.Protocol + JSON.Encode(Descriptor);

            FileSystemInfo.WriteFile("json.out.log", Helper.Join("\t", Native.DateTime, Source, "\r\n"), true);

            if (this.IsFileUpload)
            {
                Native.echo("<script>this.parent['" + this.Descriptor.Callback + "']('" + Native.API.base64_encode(Source) + "')</script>");
            }
            else
            {
                Native.echo(Source);
            }

            Native.API.flush();
        }

        #region IDisposable Members

        public void Dispose()
        {
            WriteToStream();
        }

        #endregion

        /// <summary>
        /// returns true if descriptor is not null and method is post
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.Descriptor != null && Native.SuperGlobals.Server[Native.SuperGlobals.ServerVariables.REQUEST_METHOD] == "POST";
            }
        }

        public void Strict()
        {
            if (!IsValid)
            {
                Native.Log("malformed transport request", Native.LogLevelEnum.Medium);

                if (NetworkInfo.RequestMethod == "post")
                {
                    Native.API.header("HTTP/1.0 400 Bad Request");
                }
                else
                {
                    Native.Error("json encoded data expected via HTTP POST request");
                }

                Native.API.exit();
            }
        }
    }

}
