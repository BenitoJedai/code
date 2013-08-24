using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.IO;

namespace com.abstractatech.gamification.craft
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            ////"X:\jsc.svn\examples\javascript\android\com.abstractatech.gamification.craft\com.abstractatech.gamification.craft\Audio\music\intro.mid"

            //var bytes = File.ReadAllBytes(@"X:\jsc.svn\examples\javascript\android\com.abstractatech.gamification.craft\com.abstractatech.gamification.craft\Audio\music\intro.mid");
            ////var bytes = File.ReadAllBytes(@"X:\jsc.svn\examples\javascript\android\com.abstractatech.gamification.craft\com.abstractatech.gamification.craft\Audio\music\Warcraft1_TitleTheme.mid");
            //var base64 = Convert.ToBase64String(bytes);


            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
