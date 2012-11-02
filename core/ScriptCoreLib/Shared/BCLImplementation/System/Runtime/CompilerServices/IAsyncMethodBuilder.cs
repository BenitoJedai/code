﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices
{
    // see: http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.iasyncstatemachine(v=vs.110).aspx
    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.IAsyncMethodBuilder")]
    internal interface __IAsyncMethodBuilder
    {
        void PreBoxInitialization();

    }

}
