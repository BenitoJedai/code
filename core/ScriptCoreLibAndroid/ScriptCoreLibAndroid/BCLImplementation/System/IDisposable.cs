using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System
{

    [Script(Implements = typeof(IDisposable))]
    internal interface __IDisposable
    {
        void Dispose();
    }
}
