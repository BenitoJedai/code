using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.BCLImplementation.Microsoft.FSharp.Core
{
    [Script(ImplementsViaAssemblyQualifiedName = @"Microsoft.FSharp.Core.OptimizedClosures, FSharp.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    internal class __OptimizedClosures
    {
        // tested by X:\jsc.svn\examples\actionscript\RotatingImage\RotatingImage\ApplicationCanvas.fs

        [Script(ImplementsViaAssemblyQualifiedName = 
@"Microsoft.FSharp.Core.OptimizedClosures+FSharpFunc`5, FSharp.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
)]

        public abstract class __FSharpFunc<T1, T2, T3, T4, TResult> :
            FSharpFunc<T1,
                FSharpFunc<T2,
                    FSharpFunc<T3,
                        FSharpFunc<T4, TResult>
                    >
                >
            >
        {
            public abstract TResult Invoke(T1 t1, T2 t2, T3 t3, T4 t4);

        }
    }
}
