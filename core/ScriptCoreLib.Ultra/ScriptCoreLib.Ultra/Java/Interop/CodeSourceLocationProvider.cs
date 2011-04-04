using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.security;
using java.net;
using ScriptCoreLibJava.Extensions;
using System.IO;

namespace ScriptCoreLib.Java.Interop
{
    public static class CodeSourceLocationProvider
    {
        public static string GetCodeSourceLocation()
        {
            var f = default(FileInfo);

            try
            {
                var cls = typeof(CodeSourceLocationProvider).ToClass();

                ProtectionDomain pDomain = cls.getProtectionDomain();
                CodeSource cSource = pDomain.getCodeSource();
                URL loc = cSource.getLocation();


                var ff = loc.getFile();
                var prefix = "file:/";

                if (prefix == ff.Substring(0, prefix.Length))
                    ff = ff.Substring(prefix.Length);

                f = new FileInfo(ff);
            }
            catch
            {
                throw new NotSupportedException();
            }

            return f.FullName;
        }
    }
}
