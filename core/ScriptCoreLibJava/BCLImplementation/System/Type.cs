using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.Reflection;
using System.Reflection;
using java.lang.reflect;
using System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Type))]
    internal class __Type : __MemberInfo
    {
        public static implicit operator __Type(Type e)
        {
            return (__Type)(object)e;
        }

        RuntimeTypeHandle _TypeHandle;

        public virtual Type BaseType
        {
            get
            {
                return (__Type)this.InternalTypeDescription.getSuperclass();
            }
        }

        public Type[] GetInterfaces()
        {
            return __Type.Of(this.InternalTypeDescription.getInterfaces());
        }

        private static Type[] Of(java.lang.Class[] p)
        {
            var n = new Type[p.Length];

            for (int i = 0; i < p.Length; i++)
            {
                n[i] = (__Type)p[i];
            }

            return n;
        }

        public static Type GetTypeFromValue(object x)
        {
            return (__Type)global::java.lang.Object.getClass(x); ;
        }

        public static Type GetTypeFromHandle(RuntimeTypeHandle TypeHandle)
        {
            var e = new __Type
            {
                _TypeHandle = TypeHandle
            };

            return (Type)(object)e;
        }

        public string FullName
        {
            get
            {
                // fixme: should return .net styled names
                return InternalFullName;
            }
        }

        public global::java.lang.Class InternalTypeDescription
        {
            get
            {
                var a = this._TypeHandle.Value;
                var b = (object)a;
                var c = (__IntPtr)b;

                return c.ClassToken;
            }
        }

        public string InternalFullName
        {
            get
            {
                return this.InternalTypeDescription.getName();
            }
        }

        public override string Name
        {
            get
            {
                var t = this.InternalTypeDescription;
                var p = t.getPackage();
                var n = t.getName();

                if (p != null)
                {
                    var x = p.getName();

                    if (n.StartsWith(x))
                        return n.Substring(x.Length + 1);

                    return x;
                }

                return n;
            }
        }


        public ConstructorInfo[] GetConstructors()
        {
            var a = this.InternalTypeDescription.getConstructors();
            var n = new ConstructorInfo[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                n[i] = (__ConstructorInfo)a[i];
            }

            return n;
        }

        public MethodInfo[] GetMethods()
        {
            // http://www.onjava.com/pub/a/onjava/2007/03/15/reflections-on-java-reflection.html?page=3

            var a = this.InternalTypeDescription.getDeclaredMethods();
            var n = new MethodInfo[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                // suppressing default Java language access control checks
                // - does it actually do that?
                a[i].setAccessible(true);
                n[i] = (MethodInfo)(object)new __MethodInfo { InternalMethod = a[i] };

            }

            return n;
        }

        public MethodInfo[] GetMethods(BindingFlags bindingAttr)
        {
            var a = GetMethods();

            var that = (Type)(object)this;

            for (int i = 0; i < a.Length; i++)
            {
                var IsPublic = (bindingAttr & BindingFlags.Public) == BindingFlags.Public;
                var IsNonPublic = (bindingAttr & BindingFlags.NonPublic) == BindingFlags.NonPublic;
                var IsStatic = (bindingAttr & BindingFlags.Static) == BindingFlags.Static;
                var IsInstance = (bindingAttr & BindingFlags.Instance) == BindingFlags.Instance;
                var DeclaredOnly = (bindingAttr & BindingFlags.DeclaredOnly) == BindingFlags.DeclaredOnly;

                if (!IsInstance)
                    if (IsStatic)
                        if (!a[i].IsStatic)
                            a[i] = null;

                if (!IsStatic)
                    if (IsInstance)
                        if (a[i].IsStatic)
                            a[i] = null;

                if (IsPublic)
                    if (!IsNonPublic)
                        if (a[i] != null)
                            if (!a[i].IsPublic)
                                a[i] = null;

                if (!IsPublic)
                    if (IsNonPublic)
                        if (a[i] != null)
                            if (a[i].IsPublic)
                                a[i] = null;

                if (DeclaredOnly)
                {
                    if (a[i] != null)
                        if (!a[i].DeclaringType.Equals(that))
                        {
                            a[i] = null;
                        }
                }
            }

            var c = 0;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != null)
                    c++;
            }

            var m = new MethodInfo[c];

            c = 0;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != null)
                {
                    m[c] = a[i];
                    c++;
                }
            }


            return m;
        }

        public override Type DeclaringType
        {
            get { return (__Type)this.InternalTypeDescription.getDeclaringClass(); }
        }

        public static implicit operator Type(__Type e)
        {
            return (Type)(object)e;
        }


        public static implicit operator __Type(global::java.lang.Class e)
        {
            if (e == null)
                return null;

            object i = (__RuntimeTypeHandle)e;

            return GetTypeFromHandle((RuntimeTypeHandle)i);
        }

        public bool Equals(__Type e)
        {
            return this == e;
        }

        private static bool InternalEquals(__Type e, __Type k)
        {
            // .net 4.0 seems to also add == operator. jsc should choose equals until then?
            if (k.InternalTypeDescription.isAssignableFrom(e.InternalTypeDescription))
                return k.FullName == e.FullName;

            return false;
        }


        public static bool operator !=(__Type left, __Type right)
        {
            return !(left == right);
        }

        public static bool operator ==(__Type left, __Type right)
        {
            #region null checks
            var oleft = (object)left;
            var oright = (object)right;

            if (oleft == null)
            {
                if (oright == null)
                    return true;

                return false;
            }
            else
            {
                if (oright == null)
                    return false;
            }
            #endregion

            return InternalEquals(left, right);
        }

        public __FieldInfo[] GetFields()
        {
            var f = this.InternalTypeDescription.getFields();
            var a = new __FieldInfo[f.Length];

            for (int i = 0; i < f.Length; i++)
            {
                a[i] = new __FieldInfo { InternalField = f[i] };
            }


            return a;
        }

        public __FieldInfo GetField(string n)
        {
            var f = default(__FieldInfo);

            foreach (var k in GetFields())
            {
                if (k.Name == n)
                {
                    f = k;
                    break;
                }
            }

            return f;
        }

        // http://msdn.microsoft.com/en-us/library/system.type.isnested.aspx
        public bool IsNested
        {
            get
            {
                return this.InternalTypeDescription.getDeclaringClass() != null;
            }
        }


        public bool IsAssignableFrom(Type t)
        {
            return ((__Type)t).InternalTypeDescription.isAssignableFrom(this.InternalTypeDescription);
        }

        public bool IsAbstract
        {
            get
            {
                return Modifier.isAbstract(this.InternalTypeDescription.getModifiers());
            }
        }

        public bool IsInterface
        {
            get
            {
                return Modifier.isInterface(this.InternalTypeDescription.getModifiers());
            }
        }

        // http://msdn.microsoft.com/en-us/library/system.type.isnestedpublic.aspx
        public bool IsNestedPublic
        {
            get
            {
                // true if the class is nested and declared public; otherwise, false.

                if (!IsNested)
                    return false;

                return Modifier.isPublic(this.InternalTypeDescription.getModifiers());
            }
        }

        // http://msdn.microsoft.com/en-us/library/system.type.ispublic.aspx
        public bool IsPublic
        {
            get
            {
                // true if the Type is declared public and is not a nested type; otherwise, false.

                if (IsNested)
                    return false;

                return Modifier.isPublic(this.InternalTypeDescription.getModifiers());
            }
        }

        public bool IsSealed
        {
            get
            {
                return Modifier.isFinal(this.InternalTypeDescription.getModifiers());
            }
        }

        public bool IsClass
        {
            get
            {

                return IsAssignableFrom(typeof(object));
            }
        }

        public bool IsArray
        {
            get
            {
                return this.InternalTypeDescription.isArray();
            }
        }

        public Type GetElementType()
        {
            return (__Type)this.InternalTypeDescription.getComponentType();
        }

        public __Assembly InternalAssembly;

        public Assembly Assembly
        {
            get
            {
                if (InternalAssembly == null)
                    InternalAssembly = new __Assembly { InternalReference = this };

                return InternalAssembly;
            }
        }

        public string AssemblyQualifiedName
        {
            get
            {
                // http://msdn.microsoft.com/en-us/library/system.type.assemblyqualifiedname.aspx

                return this.FullName + ", " + this.Assembly.FullName;
            }
        }
    }
}
