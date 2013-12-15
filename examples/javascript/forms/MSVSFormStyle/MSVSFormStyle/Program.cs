using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Forms.Extensions;
using System;

namespace MSVSFormStyle
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // { Message = Method 'InternalAsNode' in type 'Abstractatech.JavaScript.FormAsPopup.HTML.Images.FromAssets.Preview' from assembly 'Abstractatech.JavaScript.FormAsPopup, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' does not have an implementation. }
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
