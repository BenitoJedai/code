using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace WebServiceWithStringFields
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
   //at ScriptCoreLib.Extensions.StringExtensions.TakeUntilLastOrEmpty(String e, String u)
   //at jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.CompileAndLaunch(Type PrimaryApplication) in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToUltraApplication\RewriteToUltraApplication.AsProgram.cs:line 345
   //at jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.<>c__DisplayClass22.<InternalLaunch>b__12() in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToUltraApplication\RewriteToUltraApplication.AsProgram.cs:line 77
   //at jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.<>c__DisplayClass30.<ContinueWithSplashIfAvailable>b__2b() in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToUltraApplication\RewriteToUltraApplication.AsProgram.cs:line 330
   //at ScriptCoreLib.Avalon.Desktop.JSCSolutionsNETCarouselProgram.ShowDialogSplash(Action h)
   //at jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.ContinueWithSplashIfAvailable(Action Continue) in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToUltraApplication\RewriteToUltraApplication.AsProgram.cs:line 327
   //at jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.InternalLaunch(String AssemblyDirectory, String XAssemblyDirectory) in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToUltraApplication\RewriteToUltraApplication.AsProgram.cs:line 289
   //at jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch() in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToUltraApplication\RewriteToUltraApplication.AsProgram.cs:line 54
   //at jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch(Type PrimaryApplication) in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToUltraApplication\RewriteToUltraApplication.AsProgram.cs:line 35
   //at WebServiceWithStringFields.Program.Main(String[] args) in x:\jsc.svn\examples\javascript\WebServiceWithStringFields\WebServiceWithStringFields\Program.cs:line 13

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
