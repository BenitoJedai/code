using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.BCLImplementation.System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.IO
{
    [Script(Implements = typeof(global::System.IO.FileInfo))]
    public class __FileInfo : __FileSystemInfo
    {
        public ScriptCoreLib.JavaScript.DOM.File InternalFile;

        public __FileInfo(string path)
        {
            this.InternalFile = __File.InternalFiles.FirstOrDefault(k => k.name == path);
        }

        public long Length
        {
            get
            {
                return (long)this.InternalFile.size;
            }
        }


        public override bool Exists
        {
            get { throw new NotImplementedException(); }
        }

        public override string Name
        {
            get { throw new NotImplementedException(); }
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }
    }

    [Script(Implements = typeof(ScriptCoreLib.Extensions.CoreFileInfoExtensions))]
    public static class __CoreFileInfoExtensions
    {
        public static void ReadAllText(this global::System.IO.FileInfo f, Action<string> yield)
        {
            var ff = (__FileInfo)(object)f;

            var r = new FileReader();

            r.onload = IFunction.OfDelegate(
                new Action(
                    delegate
                    {
                        yield((string)r.result);
                    }
                )
            );

            r.readAsText(ff.InternalFile, null);

        }
    }
}