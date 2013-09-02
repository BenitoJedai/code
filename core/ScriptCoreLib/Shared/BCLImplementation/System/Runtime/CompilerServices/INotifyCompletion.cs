using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices
{
    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.INotifyCompletion")]
    public interface __INotifyCompletion
    {
        void OnCompleted(Action continuation);
    }
}
