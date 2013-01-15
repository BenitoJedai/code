using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.BCLImplementation.Microsoft.FSharp.Core
{
    [Script(ImplementsViaAssemblyQualifiedName = @"Microsoft.FSharp.Core.FSharpFunc`2, FSharp.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public abstract class FSharpFunc<T, TResult>
    {
        protected FSharpFunc()
        {
        }

        public abstract TResult Invoke(T func);

        public static X InvokeFast<V, W, X>(
            FSharpFunc<T, FSharpFunc<TResult, FSharpFunc<V, FSharpFunc<W, X>>>> arg0,
            T arg1,
            TResult arg2,
            V arg3,
            W arg4
        )
        {
            throw new NotImplementedException();
        }

        //       script: error JSC1000: ActionScript :
        //BCL needs another method, please define it.
        //Cannot call type without script attribute :
        //Microsoft.FSharp.Core.FSharpFunc`2[System.Double,System.Double] for Microsoft.FSharp.Core.Unit InvokeFast[Double,Canvas,Unit](Microsoft.FSharp.Core.FSharpFunc`2[System.Double,Microsoft.FSharp.Core.FSharpFunc`2[System.Double,Microsoft.FSharp.Core.FSharpFunc`2[System.Double,Microsoft.FSharp.Core.FSharpFunc`2[System.Windows.Controls.Canvas,Microsoft.FSharp.Core.Unit]]]], Double, Double, Double, System.Windows.Controls.Canvas) used at
        //<StartupCode$RotatingImage>.$ApplicationCanvas+clo@74.Invoke at offset 005f.
        //If the use of this method is intended, an implementation should be provided with the attribute [Script(Implements=typeof(...)] set. You may have mistyped it.

    }
}
