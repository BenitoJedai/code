﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

namespace HotPolygon
{
    public interface IAssemblyReferenceToken :
        ScriptCoreLib.Shared.IAssemblyReferenceToken,
        ScriptCoreLib.Shared.Query.IAssemblyReferenceToken
    {
    }

 
}
