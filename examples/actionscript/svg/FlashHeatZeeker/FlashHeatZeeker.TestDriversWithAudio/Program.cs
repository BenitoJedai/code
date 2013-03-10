using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace FlashHeatZeeker.TestDriversWithAudio
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
//            InternalMain UnhandledException:
//{ FullName = System.Reflection.TargetInvocationException, InnerException = System.CannotUnloadAppDomainException: Error while unloading appdomain. (Exception from HRESULT: 0x80131015)
//   at System.AppDomain.Unload(AppDomain domain)
//   at jsc.meta.Loader.LoaderStrategy.Main(String[] args) in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Loader\LoaderStrategy.cs:line 179

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
