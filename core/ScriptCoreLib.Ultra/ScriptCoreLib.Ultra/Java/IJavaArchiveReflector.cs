extern alias jvm;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;
using jvm::java.io;
using jvm::java.net;
using jvm::java.lang;
using jvm::java.util.zip;
using jvm::ScriptCoreLibJava.Extensions;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Java.Extensions;

// why is this here not in scriptCoreLib?
namespace java.lang.reflect
{
    [Script(IsNative = true)]
    internal sealed class Method // : AccessibleObject, AnnotatedElement, GenericDeclaration, Member
    {
        public Class[] getExceptionTypes()
        {
            return null;
        }
    }
}

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

        void LoadFile(string jar);

        string FileNameString { get; }

        string[] PrimaryTypes { get; }




        string Type_GetBaseTypeFullName(string TypeName);
        string Type_GetAssemblyFullName(string TypeName);
        string Type_GetAssemblyLocation(string TypeName);

        bool Type_IsAssignableFrom(string e, string TypeName);
        bool Type_IsInterface(string TypeName);
        bool Type_IsAbstract(string TypeName);
        bool Type_IsPublic(string TypeName);
        bool Type_IsNested(string TypeName);
        bool Type_IsNestedPublic(string TypeName);
        bool Type_IsArray(string TypeName);

        string Type_GetElementType(string TypeName);


        string[] Type_GetInterfaces(string TypeName);

        JavaArchiveReflectorTypeInfo Type_Describe(string TypeName);

        JavaArchiveReflectorFieldInfo[] Type_GetFields(string TypeName);
        JavaArchiveReflectorConstructor[] Type_GetConstructors(string TypeName);
        JavaArchiveReflectorMethod[] Type_GetMethods(string TypeName);
    }

    public sealed class JavaArchiveReflectorTypeInfo
    {
        public bool IsInterface;
        public bool IsAbstract;
        public bool IsPublic;
        public bool IsNested;
        public bool IsNestedPublic;
        public bool IsArray;
        public bool IsSealed;

        public JavaArchiveReflectorTypeInfo()
        {

        }
    }

    [DebuggerDisplay(".field {FieldType} {FieldName};")]
    public sealed class JavaArchiveReflectorFieldInfo
    {
        public string FieldName;
        public string FieldType;

        public bool IsStatic;
        public bool IsFamily;
        public bool IsPrivate;
        public bool IsPublic;

        public bool IsLiteral;

        public long LiteralInt64;

        //public sbyte LiteralInt8;
        public byte LiteralInt8;

        public short LiteralInt16;
        public int LiteralInt32;
        public string LiteralString;

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
        public bool IsFamily;
        public bool IsAbstract;

        public string ReturnType;

        public string[] MethodThrows;

        public JavaArchiveReflectorMethod()
        {

        }


        public override string ToString()
        {
            var w = MethodName;

            w += "(";

            for (int i = 0; i < ParameterTypes.Length; i++)
            {
                if (i > 0)
                    w += ", ";

                w += ParameterTypes[i];

            }

            w += ")";

            return w + " : " + ReturnType;
        }
    }

    public static class JavaArchiveReflectorExtensions
    {
        public static bool SignatureEquals(this JavaArchiveReflectorConstructor a, JavaArchiveReflectorConstructor b)
        {

            if (a.ParameterTypes.Length != b.ParameterTypes.Length)
                return false;

            var value = true;
            for (int i = 0; i < a.ParameterTypes.Length; i++)
            {
                if (a.ParameterTypes[i] != b.ParameterTypes[i])
                {
                    value = false;
                    break;
                }
            }

            return value;
        }

        public static bool SignatureEquals(this JavaArchiveReflectorMethod a, JavaArchiveReflectorMethod b, bool CheckReturnType = true)
        {
            if (a.MethodName != b.MethodName)
                return false;

            if (CheckReturnType)
                if (a.ReturnType != b.ReturnType)
                    return false;

            if (a.ParameterTypes.Length != b.ParameterTypes.Length)
                return false;

            var value = true;
            for (int i = 0; i < a.ParameterTypes.Length; i++)
            {
                if (a.ParameterTypes[i] != b.ParameterTypes[i])
                {
                    value = false;
                    break;
                }
            }

            return value;
        }

        // can this be used from c gen?
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20141204
        // no it would need to live in the ScriptCoreLib.Ultra.Library to be visible
        public static bool SignatureEquals(this MethodInfo a, string MethodName, Type ReturnType, Type[] ParameterTypes)
        {
            return SignatureEquals(
                a.Name, a.ReturnType, a.GetParameterTypes(),
                MethodName, ReturnType, ParameterTypes
                    );

        }

        public static bool SignatureEquals(
            string aMethodName, Type aReturnType, Type[] aParameterTypes,
            string MethodName, Type ReturnType, Type[] ParameterTypes)
        {

            if (aMethodName != MethodName)
                return false;

            // comparing void?

            if (aReturnType != ReturnType)
                return false;


            if (aParameterTypes.Length != ParameterTypes.Length)
                return false;

            var value = true;
            for (int i = 0; i < aParameterTypes.Length; i++)
            {
                if (aParameterTypes[i] != ParameterTypes[i])
                {
                    value = false;
                    break;
                }
            }

            return value;
        }
    }


    partial class JavaArchiveReflector
    {
        public JavaArchiveReflectorTypeInfo Type_Describe(string TypeName)
        {
            var t = this.clazzLoader.GetType(TypeName);


            return new JavaArchiveReflectorTypeInfo
            {
                IsAbstract = t.IsAbstract,
                IsArray = t.IsArray,
                IsInterface = t.IsInterface,
                IsNested = t.IsNested,
                IsNestedPublic = t.IsNestedPublic,
                IsPublic = t.IsPublic,
                IsSealed = t.IsSealed
            };
        }

        public JavaArchiveReflectorMethod[] Type_GetMethods(string TypeName)
        {
            var y = default(JavaArchiveReflectorMethod[]);
            var f = default(System.Reflection.MethodInfo[]);

            var t = this.clazzLoader.GetType(TypeName);

            try
            {
                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150402/scriptcorelibandroid-natives

                //f = t.GetMethods(); // what about protected members?
                f = t.GetMethods(
                    BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public 
                ); // what about protected members?
            }
            catch
            {
                System.Console.WriteLine("JavaArchiveReflector.Type_GetMethods error, TypeName: " + TypeName);

                // we did not find a type. skip constructors..
                y = new JavaArchiveReflectorMethod[0];
            }

            if (y == null)
            {
                y = new JavaArchiveReflectorMethod[f.Length];

                for (int i = 0; i < f.Length; i++)
                {
                    var fi = f[i];

                    //Caused by: java.lang.ClassCastException: [Ljava.lang.Object; cannot be cast to [Ljava.lang.String;
                    //        at jsc.jvmi__i__d.Internal.Java.JavaArchiveReflector.Type_GetMethods(JavaArchiveReflector.java:277)
                    //        at jsc.jvmi._ToDelegates________02000048_.Type_GetMethods(_ToDelegates________02000048_.java:345)

                    //MethodThrows = fi.ToMethod().getExceptionTypes().Select(k => k.ToType().FullName).ToArray()

                    var MethodThrows = GetExceptionTypes(fi);

                    y[i] = new JavaArchiveReflectorMethod
                    {
                        MethodIndex = i,

                        ParameterTypes = f[i].GetParameterTypeFullNames(),
                        ReturnType = f[i].ReturnType.FullName,

                        MethodName = f[i].Name,

                        IsPublic = f[i].IsPublic,
                        IsFamily = f[i].IsFamily,
                        IsStatic = f[i].IsStatic,
                        IsAbstract = f[i].IsAbstract,

                        // can we use linq now?
                        MethodThrows = MethodThrows
                    };
                }
            }

            return y;
        }

        private static string[] GetExceptionTypes(MethodInfo fi)
        {
            var MethodThrowsList = new List<string>();


            //Error	268	The call is ambiguous between the following methods or properties: 'java.lang.reflect.Method.getExceptionTypes()' and 'java.lang.reflect.Method.getExceptionTypes()'	X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Java\IJavaArchiveReflector.cs	329	13	ScriptCoreLib.Ultra

            var Method = (java.lang.reflect.Method)(object)jvm::ScriptCoreLibJava.Extensions.BCLImplementationExtensions.ToMethod(fi);

            //public Class[] getExceptionTypes();
            //        public Class[] getExceptionTypes();

            Method.getExceptionTypes().WithEach(
                (jvm::java.lang.Class e) =>
                {
                    //Error	276	Argument 1: cannot convert from 'java.lang.Class [c:\util\jsc\bin\ScriptCoreLibJava.dll]' to 'java.lang.Class [C:\util\jsc\bin\ScriptCoreLibAndroid.dll]'	X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Java\IJavaArchiveReflector.cs	353	29	ScriptCoreLib.Ultra
                    //Error	2	The best overloaded method match for 'ScriptCoreLibJava.Extensions.BCLImplementationExtensions.ToType(java.lang.Class)' has some invalid arguments	X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Java\IJavaArchiveReflector.cs	362	29	ScriptCoreLib.Ultra

                    var t = jvm::ScriptCoreLibJava.Extensions.BCLImplementationExtensions.ToType(

                        // Error	3	Argument 1: cannot convert from 'java.lang.Class [c:\util\jsc\bin\ScriptCoreLibJava.dll]' to 'java.lang.Class [C:\util\jsc\bin\ScriptCoreLibAndroid.dll]'	X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Java\IJavaArchiveReflector.cs	365	26	ScriptCoreLib.Ultra
                        c: (jvm::java.lang.Class)e
                    );

                    MethodThrowsList.Add(
                        t.FullName
                    );
                }
            );

            var MethodThrows = MethodThrowsList.ToArray();
            return MethodThrows;
        }

        public JavaArchiveReflectorConstructor[] Type_GetConstructors(string TypeName)
        {
            var y = default(JavaArchiveReflectorConstructor[]);
            var f = default(System.Reflection.ConstructorInfo[]);

            var t = this.clazzLoader.GetType(TypeName);

            try
            {
                f = t.GetConstructors(); // what about protected members?
            }
            catch
            {
                System.Console.WriteLine("JavaArchiveReflector.Type_GetConstructors error, TypeName: " + TypeName);

                // we did not find a type. skip constructors..
                y = new JavaArchiveReflectorConstructor[0];
            }

            if (y == null)
            {
                y = new JavaArchiveReflectorConstructor[f.Length];

                for (int i = 0; i < f.Length; i++)
                {
                    y[i] = new JavaArchiveReflectorConstructor
                    {
                        ConstructorIndex = i,

                        ParameterTypes = f[i].GetParameterTypeFullNames()
                    };
                }
            }

            return y;
        }

        public JavaArchiveReflectorFieldInfo[] Type_GetFields(string TypeName)
        {
            var y = default(JavaArchiveReflectorFieldInfo[]);
            var f = default(System.Reflection.FieldInfo[]);

            var t = this.clazzLoader.GetType(TypeName);

            try
            {
                f = t.GetFields(); // what about protected members?
            }
            catch
            {
                System.Console.WriteLine("JavaArchiveReflector.Type_GetFields error, TypeName: " + TypeName);

                // we did not find a type. skip constructors..
                y = new JavaArchiveReflectorFieldInfo[0];
            }

            if (y == null)
            {
                y = new JavaArchiveReflectorFieldInfo[f.Length];

                for (int i = 0; i < f.Length; i++)
                {
                    var fi = f[i];

                    var yi = new JavaArchiveReflectorFieldInfo
                    {
                        FieldName = fi.Name,
                        FieldType = fi.FieldType.FullName,

                        IsPublic = fi.IsPublic,
                        IsPrivate = fi.IsPrivate,
                        IsStatic = fi.IsStatic,
                        IsFamily = fi.IsFamily,
                        IsLiteral = fi.IsLiteral,


                    };

                    if (fi.IsLiteral)
                    {
                        InternalSetConstant(fi, yi);
                    }

                    y[i] = yi;
                }
            }

            return y;
        }

        private static void InternalSetConstant(FieldInfo fi, JavaArchiveReflectorFieldInfo yi)
        {
            // X:\jsc.svn\examples\java\Test\TestJavaFinalIntegerField\TestJavaFinalIntegerField\Program.cs
            var RawConstantValue = fi.GetRawConstantValue();
            if (RawConstantValue == null)
            {
                // wtf?

                //001b ScriptCoreLibAndroid.Natives android.os.Parcelable
                //System.InvalidOperationException: SetConstant { FieldName = EMPTY_STATE, FieldType = android.view.AbsSavedState }

                yi.IsLiteral = false;

                // enum?
                return;
            }

            var RawConstantValueType = RawConstantValue.GetType();

            if (RawConstantValueType == typeof(int))
                yi.LiteralInt32 = (int)RawConstantValue;
            else if (RawConstantValueType == typeof(short))
                yi.LiteralInt16 = (short)RawConstantValue;
            else if (RawConstantValueType == typeof(long))
                yi.LiteralInt64 = (long)RawConstantValue;
            else if (RawConstantValueType == typeof(sbyte))
                yi.LiteralInt8 = (byte)(sbyte)RawConstantValue;
            else if (fi.FieldType == typeof(string))
                yi.LiteralString = (string)RawConstantValue;
            else
            {

                //Console.WriteLine(

                //"InternalSetConstant " + new { fi.DeclaringType.FullName, fi.Name, RawConstantValueType, fi.FieldType }

                //    );

                yi.IsLiteral = false;
            }
            //throw new InvalidOperationException(
            //);
        }

        public string[] PrimaryTypes
        {
            get
            {
                //System.Console.WriteLine("JavaArchiveReflector PrimaryTypes " + this.Entries.Length);

                var a = new List<string>();

                for (int i = 0; i < this.Entries.Length; i++)
                {
                    if (this.IsType(i))
                    {
                        var Type = this.Entries[i].Type;

                        if (Type != null)
                        {
                            a.Add(Type.FullName);
                        }
                        else
                        {
                            //System.Console.WriteLine("JavaArchiveReflector PrimaryTypes null type: " + i);
                        }
                    }
                    else
                    {
                        //System.Console.WriteLine("JavaArchiveReflector PrimaryTypes not a type: " + i);
                    }
                }

                return a.ToArray();
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

        public bool Type_IsAssignableFrom(string e, string TypeName)
        {
            var _e = this.clazzLoader.GetType(e);
            var _TypeName = this.clazzLoader.GetType(TypeName);

            if (_e == null)
                return false;

            if (_TypeName == null)
                return false;

            return _e.IsAssignableFrom(_TypeName);
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
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150402/scriptcorelibandroid-natives
            //Console.WriteLine("enter Type_GetBaseTypeFullName" + new { TypeName });

            var BaseType = this.clazzLoader.GetType(TypeName).BaseType;

            if (BaseType == null)
                return null;

            var value = BaseType.FullName;

            //Console.WriteLine("exit Type_GetBaseTypeFullName" + new { TypeName, value });
            return value;
        }



        public bool IsType(int TypeIndex)
        {
            return !string.IsNullOrEmpty(this.Entries[TypeIndex].TypeFullName);
        }

    }
}
