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
            //            0204:01:01 0049:002b MSVSFormStyle.Application create Abstractatech.JavaScript.Forms.AeroStyler::Abstractatech.JavaScript.Forms.AeroStyler.HTML.Images.FromAssets.s_bg
            //0204:01:01 RewriteToAssembly error: System.InvalidOperationException: Renew any references. TargetField can not be null. { Location =

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
