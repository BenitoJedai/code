using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VectorExample
{

    

    public interface IAssemblyReferenceToken :
        // ScriptCoreLib.dll
        ScriptCoreLib.Shared.IAssemblyReferenceToken, 
        // ScriptCoreLib.Drawing.dll
        ScriptCoreLib.Shared.Drawing.Vector.IAssemblyReferenceToken, 
        // ScriptCoreLib.Query.dll
        ScriptCoreLib.Shared.Query.IAssemblyReferenceToken
    {

    }
}
