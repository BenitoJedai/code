using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
    /// <summary>
    /// Flash Player 10.1 has some support for generics.
    /// 
    /// Types like "Vector" can be used with a single generic argument.
    /// For the other generic types the generic type information is removed.
    /// </summary>
    [global::System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class GenericTypeDefinitionAttribute : Attribute
    {
    }
}
