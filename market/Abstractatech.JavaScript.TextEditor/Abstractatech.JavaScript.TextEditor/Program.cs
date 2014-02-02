using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace Abstractatech.JavaScript.TextEditor
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //---------------------------
            //Microsoft Visual Studio Express 2012 for Windows Desktop
            //---------------------------
            //Visual Studio cannot start debugging because the debug target 'X:\jsc.svn\market\Abstractatech.JavaScript.TextEditor\Abstractatech.JavaScript.TextEditor\bin\Debug\Abstractatech.JavaScript.TextEditor.exe' is missing. Please build the project and retry, or set the OutputPath and AssemblyName properties appropriately to point at the correct location for the target assembly.
            //---------------------------
            //OK   
            //---------------------------


            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
