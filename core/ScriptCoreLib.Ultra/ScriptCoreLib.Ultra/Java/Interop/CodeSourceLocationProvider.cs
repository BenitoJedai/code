extern alias jvm;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jvm::java.security;
using jvm::java.net;
using jvm::ScriptCoreLibJava.Extensions;
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
                // Error	40	The call is ambiguous between the following methods or properties: 'ScriptCoreLibJava.Extensions.BCLImplementationExtensions.ToClass(System.Type)' and 'ScriptCoreLibJava.Extensions.BCLImplementationExtensions.ToClass(System.Type)'	X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Java\Interop\CodeSourceLocationProvider.cs	22	27	ScriptCoreLib.Ultra

                var cls = BCLImplementationExtensions.ToClass(typeof(CodeSourceLocationProvider));

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
