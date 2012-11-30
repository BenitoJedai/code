using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;

namespace ScriptCoreLib.GLSL
{
    using genType = Int32;
    using thisType = ivec3;

    [StructLayout(LayoutKind.Explicit)]
    public struct ivec3
    {
        // reference implementation:
        // X:\opensource\github\SLSharp\IIS.SLSharp\Shaders\ShaderDefinition.Int32Vector.cs

        [FieldOffset(0)]
        genType _x;

        [Obfuscation(Feature = "alias:r")]
        [Obfuscation(Feature = "alias:s")]
        public genType x { get { return _x; } set { _x = value; } }

    }
}
