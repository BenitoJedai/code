extern alias jvm;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Desktop.JVM
{
    using Enum = jvm::java.lang.Enum;

    public static class JVMTypeExtensions
    {
        /// <summary>
        /// While inspecting a library based on android, javacard or jvm the enum 
        /// can display its members inline.
        /// </summary>
        /// <param name="SoureType"></param>
        /// <returns></returns>
        public static bool IsJVMEnum(this Type SourceType)
        {
            if (SourceType == null)
                return false;

            if (SourceType.IsInterface && SourceType.GetFields(
                System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.DeclaredOnly
                | System.Reflection.BindingFlags.Static).Any())
            {
                // oldschool enums for java :)
                return true;
            }

            return SourceType.BaseType != null && SourceType.BaseType.FullName == typeof(Enum).FullName;
        }
    }
}
