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
        /*
         * Captains log:
         *
         * 2011/07/09:
         *  The last run on rt.jar resulted in an error being able to get index for type.
         *  We would not be able to get index for a type if its found at runtime and 
         *  was not listed in jar file.
         *  
         *  - Lets make sure our interface method is named like XTypeX_XMethodX()
         *  
         */


        string FileNameString { get; }

        string[] PrimaryTypes { get; }




        string Type_GetBaseTypeFullName(string TypeName);
        string Type_GetAssemblyFullName(string TypeName);
        string Type_GetAssemblyLocation(string TypeName);
        bool Type_IsInterface(string TypeName);
        bool Type_IsAbstract(string TypeName);
        bool Type_IsPublic(string TypeName);
        bool Type_IsNested(string TypeName);
        bool Type_IsNestedPublic(string TypeName);
        string[] Type_GetInterfaces(string TypeName);
        bool Type_IsArray(string TypeName);
        string Type_GetElementType(string TypeName);

        JavaArchiveReflectorFieldInfo[] Type_GetFields(string TypeName);
        JavaArchiveReflectorConstructor[] Type_GetConstructors(string TypeName);
        JavaArchiveReflectorMethod[] Type_GetMethods(string TypeName);
    }

    public sealed class JavaArchiveReflectorFieldInfo
    {
        public string FieldName;
        public string FieldType;


        public JavaArchiveReflectorFieldInfo()
        {

        }
    }

    public sealed class JavaArchiveReflectorConstructor
    {
        public int ConstructorIndex;
        public string[] ParameterTypes;

        public JavaArchiveReflectorConstructor()
        {

        }
    }

    public sealed class JavaArchiveReflectorMethod
    {
        public int MethodIndex;
        public string[] ParameterTypes;

        public string MethodName;
        public bool IsStatic;
        public bool IsPublic;

        public string ReturnType;

        public JavaArchiveReflectorMethod()
        {

        }


        public override string ToString()
        {
            return MethodName;
        }
    }

    partial class JavaArchiveReflector
    {
        public JavaArchiveReflectorMethod[] Type_GetMethods(string TypeName)
        {
            var t = this.clazzLoader.GetType(TypeName);
            var f = t.GetMethods(); // what about protected members?

            var y = new JavaArchiveReflectorMethod[f.Length];

            for (int i = 0; i < f.Length; i++)
            {
                y[i] = new JavaArchiveReflectorMethod
                {
                    MethodIndex = i,

                    ParameterTypes = f[i].GetParameterTypeFullNames(),
                    ReturnType = f[i].ReturnType.FullName,

                    MethodName = f[i].Name,

                    IsPublic = f[i].IsPublic,
                    IsStatic = f[i].IsStatic
                };
            }

            return y;
        }

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
            var x = this[TypeName].GetInterfaces();
            var y = new string[x.Length];

            for (int j = 0; j < x.Length; j++)
            {
                y[j] = x[j].FullName;
            }

            return y;
        }

        public bool Type_IsPublic(string TypeName)
        {
            return this[TypeName].IsPublic;
        }

        public bool Type_IsNested(string TypeName)
        {
            return this[TypeName].IsNested;
        }

        public bool Type_IsNestedPublic(string TypeName)
        {
            return this[TypeName].IsNestedPublic;
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
            return this.clazzLoader.GetType(TypeName).Assembly.FullName;
        }

        public string Type_GetBaseTypeFullName(string TypeName)
        {
            var BaseType = this.clazzLoader.GetType(TypeName).BaseType;

            if (BaseType == null)
                return null;

            return BaseType.FullName;
        }



        public bool IsType(int TypeIndex)
        {
            return !string.IsNullOrEmpty(this.Entries[TypeIndex].TypeFullName);
        }

    }
}
