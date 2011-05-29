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
using ScriptCoreLib.Java.Extensions;
using ScriptCoreLibJava.Extensions;

namespace ScriptCoreLib.Java
{
    public interface IJavaArchiveReflector
    {
        string FileNameString { get; }

        string[] PrimaryTypes { get; }


        int GetMethodCount(int TypeIndex);

        string GetMethodName(int TypeIndex, int MethodIndex);

        bool Method_IsStatic(int TypeIndex, int MethodIndex);
        bool Method_IsPublic(int TypeIndex, int MethodIndex);
        int Method_GetParameterCount(int TypeIndex, int MethodIndex);
        string Method_GetParameterTypeFullName(int TypeIndex, int MethodIndex, int ParameterPosition);

        string Field_GetFieldTypeFullName(string TypeName, string FieldName);

        int IndexOfType(string TypeName);

        string Type_GetBaseTypeFullName(string TypeName);
        string Type_GetAssemblyFullName(string TypeName);
        string Type_GetAssemblyLocation(string TypeName);
        bool Type_IsInterface(string TypeName);
        bool Type_IsAbstract(string TypeName);
        string[] Type_GetInterfaces(string TypeName);
        bool Type_IsArray(string TypeName);
        string Type_GetElementType(string TypeName);

        JavaArchiveReflectorFieldInfo[] Type_GetFields(string TypeName);
        JavaArchiveReflectorConstructor[] Type_GetConstructors(string TypeName);
    }

    public class JavaArchiveReflectorFieldInfo
    {
        public string FieldName;
        public string FieldType;


        public JavaArchiveReflectorFieldInfo()
        {

        }
    }

    public class JavaArchiveReflectorConstructor
    {
        public int ConstructorIndex;
        public string[] ParameterTypes;

        public JavaArchiveReflectorConstructor()
        {

        }
    }

    partial class JavaArchiveReflector
    {
        public JavaArchiveReflectorConstructor[] Type_GetConstructors(string TypeName)
        {
            var t = this.clazzLoader.GetType(TypeName);
            var f = t.GetConstructors(); // what about protected members?

            var y = new JavaArchiveReflectorConstructor[f.Length];

            for (int i = 0; i < f.Length; i++)
            {
                y[i] = new JavaArchiveReflectorConstructor
                {
                    ConstructorIndex = i,

                    ParameterTypes = f[i].GetParameterTypeFullNames()
                };
            }

            return y;
        }

        public JavaArchiveReflectorFieldInfo[] Type_GetFields(string TypeName)
        {
            var t = this.clazzLoader.GetType(TypeName);
            var f = t.GetFields(); // what about protected members?

            var y = new JavaArchiveReflectorFieldInfo[f.Length];

            for (int i = 0; i < f.Length; i++)
            {
                y[i] = new JavaArchiveReflectorFieldInfo
                {
                    FieldName = f[i].Name,
                    FieldType = f[i].FieldType.FullName
                };
            }

            return y;
        }

        public string[] PrimaryTypes
        {
            get
            {
                var a = new ArrayList();

                for (int i = 0; i < this.Count; i++)
                {
                    if (this.IsType(i))
                        a.Add(this.Entries[i].Type.FullName);
                }

                return (string[])a.ToArray(typeof(string));
            }
        }

        public bool Type_IsArray(string TypeName)
        {
            return this.clazzLoader.GetType(TypeName).IsArray;
        }

        public string Type_GetElementType(string TypeName)
        {
            return this.clazzLoader.GetType(TypeName).GetElementType().FullName;
        }

        public string[] Type_GetInterfaces(string TypeName)
        {
            var i = IndexOf(TypeName);

            if (i < 0)
                return new string[0];

            var x = this.Entries[i].Type.GetInterfaces();
            var y = new string[x.Length];

            for (int j = 0; j < x.Length; j++)
            {
                y[j] = x[j].FullName;
            }

            return y;
        }

        public bool Type_IsAbstract(string TypeName)
        {
            return this.clazzLoader.GetType(TypeName).IsAbstract;
        }

        public bool Type_IsInterface(string TypeName)
        {
            return this.clazzLoader.GetType(TypeName).IsInterface;
        }


        public string Type_GetAssemblyLocation(string TypeName)
        {
            return this.clazzLoader.GetType(TypeName).Assembly.Location;
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

        //public int GetFieldCount(int TypeIndex)
        //{
        //    return this.Entries[TypeIndex].Fields.Length;
        //}

        //public string GetFieldName(int TypeIndex, int MemberIndex)
        //{
        //    return this.Entries[TypeIndex].Fields[MemberIndex].Name;
        //}


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

        public bool Method_IsPublic(int TypeIndex, int MethodIndex)
        {
            // we should use the attributes instead?
            return this.Entries[TypeIndex].Methods[MethodIndex].IsPublic;
        }

        public int Method_GetParameterCount(int TypeIndex, int MethodIndex)
        {
            return this.Entries[TypeIndex].Methods[MethodIndex].GetParameters().Length;
        }

        public string Method_GetParameterTypeFullName(int TypeIndex, int MethodIndex, int ParameterPosition)
        {
            var m = this.Entries[TypeIndex].Methods[MethodIndex];

            if (ParameterPosition < 0)
                return m.ReturnType.FullName;

            return m.GetParameters()[ParameterPosition].ParameterType.FullName;
        }
    }
}
