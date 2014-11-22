﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/object.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Object.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Object.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Object.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Object.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Object.cs

    [Script(Implements = typeof(object))]
    internal class __Object
    {
        [Script(OptimizedCode = "return a === b;")]
        public static bool ReferenceEquals(object a, object b)
        {
            return default(bool);
        }

        public new virtual string ToString()
        {
            return default(string);
        }

        [Script(DefineAsStatic = true)]
        new public Type GetType()
        {
            return __Type.GetTypeFromValue(this);
        }
    }
}
