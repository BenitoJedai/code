using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

[assembly: ScriptResources("assets/WebCalculator")]

namespace FormsExample
{
    interface IAssemblyReferenceToken :
        ScriptCoreLib.Shared.Windows.Forms.IAssemblyReferenceToken,
        ScriptCoreLib.Shared.Drawing.IAssemblyReferenceToken,
        ScriptCoreLib.Shared.Query.IAssemblyReferenceToken,
        ScriptCoreLib.Shared.IAssemblyReferenceToken

    {

    }

}
