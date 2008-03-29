using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib
{
    /// <summary>
    /// Indicates that the integer value assigned to a parameter should be represented
    /// as an hex 
    /// </summary>
    [global::System.AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public sealed class HexAttribute : Attribute
    {

    }
}
