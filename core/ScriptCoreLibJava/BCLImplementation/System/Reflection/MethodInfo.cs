﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;
using java.lang.reflect;
using java.lang;

namespace ScriptCoreLibJava.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(MethodInfo))]
    internal class __MethodInfo : __MethodBase
    {
        public global::java.lang.reflect.Method InternalMethod;

        public override string Name
        {
            get { return InternalMethod.getName(); }
        }

        public override global::System.Type DeclaringType
        {
            get
            {
                return (__Type)InternalMethod.getDeclaringClass();
            }
        }

        public override ParameterInfo[] GetParameters()
        {
            var a = this.InternalMethod.getParameterTypes();
            var n = new ParameterInfo[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                n[i] = new __ParameterInfo
                {
                    ParameterType = (__Type)a[i],
                    Position = i
                };
            }

            return n;
        }

        public virtual global::System.Type ReturnType
        {
            get
            {
                return (__Type)this.InternalMethod.getReturnType();
            }
        }

        public static implicit operator __MethodInfo(Method m)
        {
            if (m == null)
                return null;

            return new __MethodInfo { InternalMethod = m };
        }

        public static implicit operator MethodInfo(__MethodInfo m)
        {
            return (MethodInfo)(object)m;
        }


        public override bool InternalIsStatic()
        {
            return Modifier.isStatic(InternalMethod.getModifiers());
        }

        public override bool InternalIsPublic()
        {
            return Modifier.isPublic(InternalMethod.getModifiers());
        }

        public override bool InternalIsFamily()
        {
            return Modifier.isProtected(InternalMethod.getModifiers());
        }

        public override bool InternalIsAbstract()
        {
            return Modifier.isAbstract(InternalMethod.getModifiers());
        }

        public override object InternalInvoke(object obj, object[] parameters)
        {
            var n = default(object);

            try
            {
                n = this.InternalMethod.invoke(obj, parameters);
            }
            catch 
            {
                throw;
            }

            return n;
        }

        public static bool operator ==(__MethodInfo a, __MethodInfo b)
        {
            return a.InternalMethod == b.InternalMethod;
        }

        public static bool operator !=(__MethodInfo a, __MethodInfo b)
        {
            return a.InternalMethod != b.InternalMethod;
        }


        public override string ToString()
        {
            // System.Collections.Generic.IEnumerable`1[System.Xml.Linq.XElement] _Elements_b__1(T)

            var w = new StringBuilder();

            w.Append(this.ReturnType.FullName);
            w.Append(" ");
            w.Append(base.ToString());

            return w.ToString();
        }
    }
}
