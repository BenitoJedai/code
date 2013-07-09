using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace com.abstractatech.gamification.jwe
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // {"Could not load type 'ScriptCoreLib.ActionScript.mx.core.ILayoutDirectionElement' from assembly 'ScriptCoreLib, Version=4.5.0.0, Culture=neutral, PublicKeyToken=null'.":"ScriptCoreLib.ActionScript.mx.core.ILayoutDirectionElement"}
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
