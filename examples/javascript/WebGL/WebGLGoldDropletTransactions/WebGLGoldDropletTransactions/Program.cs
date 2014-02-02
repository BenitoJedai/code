using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace WebGLGoldDropletTransactions
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //stack rewrite for 00000053
            //281c:01:01 RewriteToAssembly error: System.InvalidOperationException: Renew any references. TargetField can not be null. { Location =
            // assembly: X:\jsc.svn\examples\javascript\WebGL\WebGLGoldDropletTransactions\WebGLGoldDropletTransactions\bin\Debug\WebGLOBJExperiment.dll
            // type: WebGLOBJExperiment.THREE_OBJAsset+<>c__DisplayClass6+<>c__DisplayClass8, WebGLOBJExperiment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x001d
            //  method:Void <.ctor>b__4(System.Object) }
            //   at jsc.Languages.IL.ILTranslationExtensions.EmitToArguments.<.ctor>b__47(ILInstruction )
            //   at jsc.Languages.IL.ILTranslationExtensions.EmitToArguments. ? . ? .    (ILRewriteContext )
            //   at jsc.Languages.IL.ILTranslationExtensions.EmitToArguments.?? .    (ILRewriteContext )
            //   at jsc.Languages.IL.ILTranslationExtensions.EmitTo(MethodBase , ILGenerator , EmitToArguments , TypeBuilder )

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
