using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using java.io;
using java.net;
using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;

namespace ScriptCoreLib.Java
{
    public interface IJavaArchiveReflector
    {
        string FileNameString { get; }

        int Count { get; }

        bool IsType(int TypeIndex);

        string GetTypeFullName(int index);

        int GetFieldCount(int TypeIndex);

        string GetFieldName(int TypeIndex, int MemberIndex);

        int GetMethodCount(int TypeIndex);

        string GetMethodName(int TypeIndex, int MethodIndex);

        bool Method_IsStatic(int TypeIndex, int MethodIndex);

        int Method_GetParameterCount(int TypeIndex, int MethodIndex);

        string Method_GetParameterTypeFullName(int TypeIndex, int MethodIndex, int ParameterPosition);
    }

    partial class JavaArchiveReflector
    {
        public bool IsType(int TypeIndex)
        {
            return !string.IsNullOrEmpty(this.Entries[TypeIndex].TypeFullName); 
        }

        public int GetFieldCount(int TypeIndex)
        {
            return this.Entries[TypeIndex].Fields.Length;
        }

        public string GetFieldName(int TypeIndex, int MemberIndex)
        {
            return this.Entries[TypeIndex].Fields[MemberIndex].Name;
        }


        public int GetMethodCount(int TypeIndex)
        {
            return this.Entries[TypeIndex].Methods.Length;
        }

        public string GetMethodName(int TypeIndex, int MethodIndex)
        {
            return this.Entries[TypeIndex].Methods[MethodIndex].Name;
        }

        public bool Method_IsStatic(int TypeIndex, int MethodIndex)
        {
            // we should use the attributes instead?
            return this.Entries[TypeIndex].Methods[MethodIndex].IsStatic;
        }

        public int Method_GetParameterCount(int TypeIndex, int MethodIndex)
        {
            return this.Entries[TypeIndex].Methods[MethodIndex].GetParameters().Length;
        }

        public string Method_GetParameterTypeFullName(int TypeIndex, int MethodIndex, int ParameterPosition)
        {
            return this.Entries[TypeIndex].Methods[MethodIndex].GetParameters()[ParameterPosition].ParameterType.FullName;
        }
    }
}
