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
            //1960:01:01 0061:0145 DropFileIntoSQLite.Application define Abstractatech.JavaScript.FormAsPopup::ScriptCoreLib.Extensions.FormAsPopupExtensionsForConsoleFormPackage

            //{ trace = X:\jsc.internal.svn\compiler\jsc\Languages\IL\ILTranslationExtensions.EmitToArguments.cs, TargetMethod = System.Windows.Forms.Form PopupInsteadOfClosing[Form](System.Windows.Forms.Form, Boolean, System.Action, Boolean, System.Action, System.Action), DeclaringType = ScriptCoreLib.Extensions.FormAsPopupExtensionsForConsoleFormPackage, Location =
            // assembly: X:\jsc.svn\examples\javascript\DropFileIntoSQLite\DropFileIntoSQLite\bin\Debug\DropFileIntoSQLite.exe
            // type: DropFileIntoSQLite.Application+<>c__DisplayClass23+<>c__DisplayClass2c, DropFileIntoSQLite, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x004f
            //  method:Void <.ctor>b__e(ScriptCoreLib.JavaScript.DOM.File, Int32), ex = System.Reflection.ReflectionTypeLoadException: Unable to load one or more of the requested types. Retrieve the LoaderExceptions property for more information.

            //SystemSounds.Beep.Play();
            //SystemSounds.Asterisk.Play();
            //SystemSounds.Exclamation.Play();
            //SystemSounds.Hand.Play();
            //SystemSounds.Question.Play();

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
