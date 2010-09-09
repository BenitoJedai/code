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

        public abstract  TResult Invoke(T func);

    }
}
