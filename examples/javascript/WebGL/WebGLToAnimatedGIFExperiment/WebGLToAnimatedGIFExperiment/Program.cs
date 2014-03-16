using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace WebGLToAnimatedGIFExperiment
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {

            //Additional information: Method not found: 'Void ScriptCoreLib.JavaScript.DOM.IWindow.add_onframe(System.Action`1<Int32>)'.

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
