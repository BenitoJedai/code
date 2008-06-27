using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Lambda
{
    [Script]
    public class IdentityFunction<TElement>
    {
        // Properties
        public static global::System.Func<TElement, TElement> Instance
        {
            get
            {
                return x => x;
            }
        }
    }



}
