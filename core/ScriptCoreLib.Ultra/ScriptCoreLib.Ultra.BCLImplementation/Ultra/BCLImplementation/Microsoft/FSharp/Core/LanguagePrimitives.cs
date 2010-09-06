using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.BCLImplementation.Microsoft.FSharp.Core
{
    [Script(ImplementsViaAssemblyQualifiedName = @"Microsoft.FSharp.Core.LanguagePrimitives, FSharp.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    internal class __LanguagePrimitives
    {
        [Script(ImplementsViaAssemblyQualifiedName = @"Microsoft.FSharp.Core.LanguagePrimitives+IntrinsicFunctions, FSharp.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        internal class __IntrinsicFunctions
        {
            public static T CheckThis<T>(T x) where T : class
            {
                if (x == null)
                {
                    throw new InvalidOperationException("checkInit");
                }
                return x;
            }

        }
    }
}
