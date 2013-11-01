using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Forms.Extensions;
using System;

namespace com.abstractatech.multiscreen.formsexample
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
			DesktopFormsExtensions.Launch(
				() => new ApplicationControl()
			);
#else
            /*
    at System.IO.PathHelper.GetFullPathName()
   at System.IO.Path.NormalizePath(String path, Boolean fullCheck, Int32 maxPathLength)
   at System.IO.Path.GetFullPathInternal(String path)
   at System.IO.FileInfo.Init(String fileName, Boolean checkHost)
   at System.IO.FileInfo..ctor(String fileName)
   at jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.<Launch>b__13(String k)
   at System.Linq.Enumerable.WhereSelectArrayIterator`2.MoveNext()
   at System.Linq.Enumerable.FirstOrDefault[TSource](IEnumerable`1 source)
   at jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch()
   at jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch(Type PrimaryApplication)
   at com.abstractatech.multiscreen.formsexample.Program.Main(String[] args) in y:\jsc.internal.svn\examples\javascript\forms\com.abstractatech.multiscreen.formsexample\com.abstractatech.multiscreen.formsexample\Program.cs:line 19
             * 
 */
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
#endif
        }

    }
}
