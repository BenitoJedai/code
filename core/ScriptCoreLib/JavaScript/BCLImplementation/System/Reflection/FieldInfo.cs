using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
	// http://referencesource.microsoft.com/#mscorlib/system/reflection/fieldinfo.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Reflection/FieldInfo.cs
	// https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Reflection/FieldInfo.cs
	// https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Reflection/FieldInfo.cs

	[Script(Implements = typeof(global::System.Reflection.FieldInfo))]
    public class __FieldInfo : __MemberInfo
    {
        internal string _Name;

        public virtual FieldAttributes Attributes
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public __Type InternalDeclaringType;

        public override Type DeclaringType
        {
            get { return InternalDeclaringType; }
        }

        public virtual RuntimeFieldHandle FieldHandle
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual Type FieldType
        {
            get
            {
                //var type$InS4tkYSAj62HI0REItY4Q = InS4tkYSAj62HI0REItY4Q.prototype;
                //type$InS4tkYSAj62HI0REItY4Q.constructor = InS4tkYSAj62HI0REItY4Q;
                //type$InS4tkYSAj62HI0REItY4Q.x = null;
                //type$InS4tkYSAj62HI0REItY4Q.y = 0;



                var TargetTypeHandle = InternalDeclaringType.TypeHandle;
                var prototype = (object)TargetTypeHandle.Value;

                var defaultValueMember = Expando.Of(prototype)[_Name];

                // how could we know if it is int32 or double?
                if (defaultValueMember.IsNumber)
                    return typeof(int);

                // X:\jsc.svn\examples\javascript\test\TestFieldTypeInt32\TestFieldTypeInt32\Application.cs

                // X:\jsc.svn\examples\javascript\test\TestSpecialFieldInfo\TestSpecialFieldInfo\Application.cs
                // X:\jsc.svn\examples\javascript\test\TestSQLiteConnection\TestSQLiteConnection\Application.cs
                return typeof(string);
            }
        }

        public override string Name
        {
            // X:\jsc.svn\examples\javascript\test\TestSpecialFieldInfo\TestSpecialFieldInfo\Application.cs
            get { return _Name; }
        }


        public virtual Type ReflectedType
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public override object[] GetCustomAttributes(bool inherit)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(Type x, bool inherit)
        {
            throw new NotImplementedException();
        }



        public virtual object GetValue(object obj)
        {
            return global::ScriptCoreLib.JavaScript.Runtime.Expando.InternalGetMember(obj, _Name);

        }



        public virtual void SetValue(object obj, object value)
        {
            global::ScriptCoreLib.JavaScript.Runtime.Expando.InternalSetMember(obj, _Name, value);
        }

        public virtual void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


        public override string ToString()
        {
            return this.Name;
        }


        public static implicit operator global::System.Reflection.FieldInfo(__FieldInfo e)
        {
            return (global::System.Reflection.FieldInfo)(object)e;
        }

        public static bool operator !=(__FieldInfo left, __FieldInfo right)
        {
            return !(left == right);
        }

        public static bool operator ==(__FieldInfo left, __FieldInfo right)
        {
            if ((object)left == null)
                if ((object)right == null)
                    return true;
                else
                    return false;
            else
                if ((object)right == null)
                    return false;

            return left.Name == right.Name;
        }


    }
}
