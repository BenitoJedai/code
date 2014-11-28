using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;
using java.lang.reflect;

namespace ScriptCoreLibJava.BCLImplementation.System.Reflection
{
    // http://referencesource.microsoft.com/#mscorlib/system/reflection/fieldinfo.cs

    [Script(Implements = typeof(FieldInfo))]
    public class __FieldInfo : __MemberInfo
    {
        public global::java.lang.reflect.Field InternalField;

        public override string Name
        {
            get { return InternalField.getName(); }
        }

        public override global::System.Type DeclaringType
        {
            get
            {
                return (__Type)InternalField.getDeclaringClass();
            }
        }

        public global::System.Type FieldType
        {
            get
            {
                return (__Type)InternalField.getType();
            }
        }

        public object GetValue(object obj)
        {
            var n = default(object);
            try
            {
                n = InternalField.get(obj);
            }
            catch
            {
                throw;
            }
            return n;
        }

        public void SetValue(object obj, object value)
        {
            try
            {
                InternalField.set(obj, value);
            }
            catch
            {
                throw;
            }
        }




        public bool IsStatic
        {
            get
            {
                return Modifier.isStatic(this.InternalField.getModifiers());
            }
        }

        public bool IsPublic
        {
            get
            {
                return Modifier.isPublic(this.InternalField.getModifiers());
            }
        }

        public bool IsPrivate
        {
            get
            {
                if (IsFamily)
                    return false;

                if (IsPublic)
                    return false;

                return true;
            }
        }


        public bool IsFamily
        {
            get
            {
                return Modifier.isProtected(this.InternalField.getModifiers());
            }
        }

        public bool IsLiteral
        {
            get
            {
                return Modifier.isFinal(this.InternalField.getModifiers());
            }
        }

        public object GetRawConstantValue()
        {
            // X:\jsc.svn\examples\java\Test\TestJavaFinalIntegerField\TestJavaFinalIntegerField\Program.cs
            var value = default(object);

            try
            {
                value = this.InternalField.get(null);
            }
            catch
            {
            }
            return value;
        }

        public static bool operator ==(__FieldInfo a, __FieldInfo b)
        {
            return InternalIsEqual(a, b);
        }

        private static bool InternalIsEqual(__FieldInfo a, __FieldInfo b)
        {
            if ((object)a == null)
                if ((object)b == null)
                    return true;

            if ((object)a != null)
                if ((object)b == null)
                    return false;

            if ((object)a == null)
                if ((object)b != null)
                    return false;

            return a.InternalField == b.InternalField;
        }

        public static bool operator !=(__FieldInfo a, __FieldInfo b)
        {
            return !InternalIsEqual(a, b);
        }

        public override string ToString()
        {
            return this.FieldType.FullName + " " + this.Name;
        }
    }
}
