extern alias jvm;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Desktop.JVM
{
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
            // called by?

            if (SourceType == null)
                return false;

            // see java.sql.Connection

            if (SourceType.IsInterface && SourceType.GetFields(
                System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.DeclaredOnly
                | System.Reflection.BindingFlags.Static).Any())
            {
                // oldschool enums for java :)
                return SourceType.GetMethods().Length < 4;
            }

            if (SourceType.BaseType != null
                && SourceType.BaseType.FullName == typeof(jvm::java.lang.Enum).FullName)
            {
                return SourceType.GetMethods().Length < 4;
            }

            return false;
        }
    }
}
