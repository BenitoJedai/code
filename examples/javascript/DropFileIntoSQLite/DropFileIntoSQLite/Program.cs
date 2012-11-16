using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.Media;

namespace DropFileIntoSQLite
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //SystemSounds.Beep.Play();
            //SystemSounds.Asterisk.Play();
            //SystemSounds.Exclamation.Play();
            //SystemSounds.Hand.Play();
            //SystemSounds.Question.Play();

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
