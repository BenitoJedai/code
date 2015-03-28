using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;
using ScriptCoreLib.Shared.BCLImplementation.System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/IO/FileInfo.cs

    // 
    [Script(Implements = typeof(global::System.IO.FileInfo))]
    internal class __FileInfo : __FileSystemInfo
    {
        readonly string InternalPath;

        public __FileInfo(string path)
        {
            InternalPath = path;
        }
        public override bool Exists
        {
            get { return __File.Exists(FullName); }
        }


        public override string Name
        {
            get { return Path.GetFileName(InternalPath); }
        }

        public override string FullName
        {
            get
            {
                return __Directory.__GetFullPath(InternalPath);
            }
        }



        public long Length
        {
            get
            {
                // X:\jsc.svn\examples\javascript\appengine\Test\TestAppEngineFileLength\TestAppEngineFileLength\ApplicationWebService.cs

                return
                    new java.io.File(this.InternalPath).length();
            }
        }


        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override DateTime LastWriteTimeUtc
        {
            get
            {
                // http://stackoverflow.com/questions/5525854/how-to-convert-the-file-last-modified-timestamp-to-a-date
                // C:\Users\Arvo\AppData\Local\Temp\MetadataAsSource\4caec971e3d94594a5b57af72d2cd51b\0263743930004db4bd0fa4f184052414\FileSystemInfo.cs

                var x = new java.io.File(this.InternalPath).lastModified();

                var v = new __DateTime();

                v.InternalValue = global::java.util.Calendar.getInstance();
                v.InternalValue.setTimeInMillis(x);

                return v.ToUniversalTime();
            }
            set
            {
            }
        }
    }
}
