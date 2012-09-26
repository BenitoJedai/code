﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.IFormatProvider))]
    internal interface __IFormatProvider
    {
        object GetFormat(Type formatType);
    }
}
