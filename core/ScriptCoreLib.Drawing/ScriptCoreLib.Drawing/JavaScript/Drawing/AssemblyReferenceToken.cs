using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.Drawing
{
    public class AssemblyReferenceToken
    {
        static IEnumerable<Type> References
        {
            get
            {
                yield return typeof(ScriptCoreLib.JavaScript.AssemblyReferenceToken);
            }
        }
    }
}
