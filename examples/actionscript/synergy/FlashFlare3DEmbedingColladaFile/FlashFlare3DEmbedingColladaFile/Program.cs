using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace FlashFlare3DEmbedingColladaFile
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //{"Declaration referenced in a method implementation cannot be a final method.  Type: 'flare.core.Pivot3D'.  Assembly: 'Flare3D, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.":"flare.core.Pivot3D"}


            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
