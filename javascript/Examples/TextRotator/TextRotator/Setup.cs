using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

namespace TextRotator
{

    static class Setup
    {
        static IEnumerable<Type> References
        {
            get
            {
                yield return typeof(ScriptCoreLib.Shared.IAssemblyReferenceToken);
                yield return typeof(ScriptCoreLib.Shared.Query.IAssemblyReferenceToken);
            }
        }

    }
}
