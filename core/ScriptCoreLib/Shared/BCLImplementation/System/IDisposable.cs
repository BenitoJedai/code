using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/idisposable.cs
    [Script(Implements = typeof(global::System.IDisposable))]
    public interface __IDisposable
    {
        void Dispose();
    }
}
