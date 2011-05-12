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
        
        string Field_GetFieldTypeFullName(string TypeName, string FieldName);

        int IndexOfType(string TypeName);

        string Type_GetBaseTypeFullName(string TypeName);
        string Type_GetAssemblyFullName(string TypeName);
        string Type_GetAssemblyLocation(string TypeName);
    }

    partial class JavaArchiveReflector
    {
        public string Type_GetAssemblyLocation(string TypeName)
        {
            var i = IndexOf(TypeName);

            if (i < 0)
                return null;

            return this.Entries[i].Type.Assembly.Location;
        }

        public string Type_GetAssemblyFullName(string TypeName)
        {
            var i = IndexOf(TypeName);

            if (i < 0)
                return null;


            return this.Entries[i].Type.Assembly.FullName;
        }

        public string Type_GetBaseTypeFullName(string TypeName)
        {
            var BaseType = this.Entries[IndexOf(TypeName)].Type.BaseType;

            if (BaseType == null)
                return null;

            return BaseType.FullName;
        }

        public string Field_GetFieldTypeFullName(string TypeName, string FieldName)
        {
            return this.Entries[IndexOf(TypeName)].Type.GetField(FieldName).FieldType.FullName;
        }

        public int IndexOfType(string TypeName)
        {
            var i = -1;

            // JSC for Java does not yet support generics nor linq 
            //foreach (var SourceTypeIndex in Enumerable.Range(0, Count))

            for (int SourceTypeIndex = 0; SourceTypeIndex < Count; SourceTypeIndex++)
            {
                if (this.IsType(SourceTypeIndex))
                {
                    var SourceTypeFullName = this.GetTypeFullName(SourceTypeIndex);

                    if (TypeName == SourceTypeFullName)
                    {
                        i = SourceTypeIndex;
                        break;
                    }
                }
            }

            return i;
        }

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
