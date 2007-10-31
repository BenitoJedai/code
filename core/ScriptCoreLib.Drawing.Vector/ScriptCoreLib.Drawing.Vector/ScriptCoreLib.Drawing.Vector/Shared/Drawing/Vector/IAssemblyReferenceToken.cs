using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Drawing.Vector
{
    
    public interface IAssemblyReferenceToken :
        // ScriptCoreLib.dll
        ScriptCoreLib.Shared.IAssemblyReferenceToken, 
        // ScriptCoreLib.Query.dll
        ScriptCoreLib.Shared.Query.IAssemblyReferenceToken
    {

    }
}
