using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.Windows.Forms
{
    public class AssemblyReferenceToken
    {
        static IEnumerable<Type> References
        {
            get
            {
                yield return typeof(ScriptCoreLib.JavaScript.Drawing.AssemblyReferenceToken);
            }
        }
    }
}
