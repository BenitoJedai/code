using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;
using ScriptCoreLib.Shared.BCLImplementation.System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
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
    }
}
