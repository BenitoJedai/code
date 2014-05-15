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
        public static __Type GetType(string typeName)
        {
            var c = default(java.lang.Class);

            try
            {
                c = java.lang.Class.forName(typeName);
            }
            catch
            {

            }

            return c;
        }

        public static implicit operator __Type(global::System.Type e)
        {
            return (__Type)(object)e;
        }

        RuntimeTypeHandle _TypeHandle;

        public virtual global::System.Type BaseType
        {
            get
            {
                return (__Type)this.InternalTypeDescription.getSuperclass();
            }
        }

        public global::System.Type[] GetInterfaces()
        {
            return __Type.Of(this.InternalTypeDescription.getInterfaces());
        }

        private static global::System.Type[] Of(java.lang.Class[] p)
        {
            var n = new global::System.Type[p.Length];

            for (int i = 0; i < p.Length; i++)
            {
                n[i] = (__Type)p[i];
            }

            return n;
        }

        public static global::System.Type GetTypeFromValue(object x)
        {
            return (__Type)global::java.lang.Object.getClass(x); ;
        }

        public static global::System.Type GetTypeFromHandle(RuntimeTypeHandle TypeHandle)
        {
            var e = new __Type
            {
                _TypeHandle = TypeHandle
            };

            return (global::System.Type)(object)e;
        }

        public string Namespace
        {
            get
            {
                var p = this.InternalTypeDescription.getPackage();

                if (p != null)
                    return p.getName();



                return "";
            }
        }

        public string FullName
        {
            get
            {
                var n = InternalFullName;



                return n;
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

        public ConstructorInfo GetConstructor(global::System.Type[] parameters)
        {
            Console.WriteLine("GetConstructor " + new { FullName, parameters });

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140111-iquery/ldtoken
            // like enum.ToString, javascript cannot do New Expressions while java might..
            // X:\jsc.svn\examples\java\Test\JVMCLRNewExpression\JVMCLRNewExpression\Program.cs


            var c = new List<java.lang.Class>();
            foreach (var item in parameters)
            {
                c.Add(ScriptCoreLibJava.Extensions.BCLImplementationExtensions.ToClass(item));
            }
            var m = default(Constructor);

            try
            {
                m = this.InternalTypeDescription.getConstructor(c.ToArray());
            }
            catch
            {

            }

            var cc = (__ConstructorInfo)m;

            Console.WriteLine("GetConstructor " + new { FullName, cc });

            return cc;
        }

        public MethodInfo GetMethod(string name, global::System.Type[] parameters)
        {
            var c = new List<java.lang.Class>();
            foreach (var item in parameters)
            {
                c.Add(ScriptCoreLibJava.Extensions.BCLImplementationExtensions.ToClass(item));
            }
            var m = default(Method);

            try
            {
                m = this.InternalTypeDescription.getMethod(name, c.ToArray());
            }
            catch
            {

            }

            return (__MethodInfo)m;
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

                //  access denied (java.lang.reflect.ReflectPermission suppressAccessChecks), StackTrace = java.security.AccessControlException: access denied (java.lang.reflect.ReflectPermission suppressAccessChecks)
                //a[i].setAccessible(true);

                n[i] = (MethodInfo)(object)new __MethodInfo { InternalMethod = a[i] };

            }

            return n;
        }

        public MethodInfo[] GetMethods(BindingFlags bindingAttr)
        {
            var a = GetMethods();

            var that = (global::System.Type)(object)this;

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

        public override global::System.Type DeclaringType
        {
            get { return (__Type)this.InternalTypeDescription.getDeclaringClass(); }
        }

        public static implicit operator global::System.Type(__Type e)
        {
            return (global::System.Type)(object)e;
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
            return InternalEquals(this, e);
        }

        private static bool InternalEquals(__Type e, __Type k)
        {
            #region null checks
            var oleft = (object)e;
            var oright = (object)k;

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

            // .net 4.0 seems to also add == operator. jsc should choose equals until then?
            if (k.InternalTypeDescription.isAssignableFrom(e.InternalTypeDescription))
                return k.FullName == e.FullName;

            return false;
        }


        public static bool operator !=(__Type left, __Type right)
        {
            return !InternalEquals(left, right);
        }

        public static bool operator ==(__Type left, __Type right)
        {


            return InternalEquals(left, right);
        }

        // Returns all the public fields of the current System.Type.
        public __FieldInfo[] GetFields()
        {

            var f = this.InternalTypeDescription.getDeclaredFields();
            var a = new java.util.ArrayList<__FieldInfo>();

            for (int i = 0; i < f.Length; i++)
            {
                var fi = f[i];

                // via https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20120-1/20120817-wordpress
                var isPublic = Modifier.isPublic(fi.getModifiers());
                var isFinal = Modifier.isFinal(fi.getModifiers());

                if (isPublic || isFinal)
                {
                    a.add(
                        new __FieldInfo { InternalField = fi }
                       );
                }
            }


            // otherwise, a new array of the same runtime type is allocated 
            return a.toArray(new __FieldInfo[0]);
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
                // java.lang.IncompatibleClassChangeError: com.sun.java.swing.plaf.nimbus.AbstractRegionPainter and com.sun.java.swing.plaf.nimbus.AbstractRegionPainter$PaintContext disagree on InnerClasses attribute
                // java.lang.Class.getDeclaringClass(Native Method)

                var c = default(java.lang.Class);

                try
                {
                    c = this.InternalTypeDescription.getDeclaringClass();
                }
                catch
                {
                }

                return c != null;
            }
        }


        public bool IsAssignableFrom(global::System.Type t)
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

        public global::System.Type GetElementType()
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

        public override string ToString()
        {
            return this.FullName;
        }
    }
}
