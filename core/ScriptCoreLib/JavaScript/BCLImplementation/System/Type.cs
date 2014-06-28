using System;
using System.Collections.Generic;
using System.Text;
//using Reflection;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using System.Reflection;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{


    [Script(Implements = typeof(global::System.Type))]
    public class __Type : __MemberInfo
    {
        // X:\jsc.svn\examples\javascript\test\TestTypeHandle\TestTypeHandle\Application.cs
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Type.cs



        //Implementation not found for type import :
        //type: System.Type
        //method: Boolean get_IsEnum()
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!
        public virtual bool IsEnum
        {
            get
            {
                // jsc erases enum typeinfo for jvm.

                return false;
            }
        }


        public override string ToString()
        {
            if (IsNative)
                return "[native] " + this.Name;

            return this.FullName;
        }

        [Script]
        internal sealed class __AttributeReflection
        {
            public IFunction Type;
            public object Value;
        }

        [Script]
        internal sealed class __TypeReflection
        {
            public IFunction GetAttributes;
        }

        public __Assembly Assembly
        {
            get
            {
                return new __Assembly
                {

                    __Value = (__AssemblyValue)Expando.InternalGetMember(AsExpando().constructor, "Assembly")
                };
            }
        }

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression
        public bool IsNative
        {
            get
            {
                return (bool)Expando.InternalIsMember(AsExpando().constructor, "IsNative");
            }
        }


        // where is this used?


        public static RuntimeTypeHandle GetTypeHandle(object o)
        {
            // X:\jsc.svn\examples\javascript\test\TestTaskStartToString\TestTaskStartToString\Application.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.ctor.cs

            return o.GetType().TypeHandle;
        }

        public RuntimeTypeHandle TypeHandle
        {
            get;
            set;
        }




        #region GetFields
        public __FieldInfo GetField(string name)
        {
            __FieldInfo r = null;

            if (this.IsNative)
            {
                // we do not have the type information. behave as if dynamic
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression
                return r = new __FieldInfo { _Name = name, InternalDeclaringType = this };
            }

            foreach (var m in global::ScriptCoreLib.JavaScript.Runtime.Expando.Of(this.TypeHandle.Value).GetFields())
            {
                if (m.Name == name)
                {
                    r = new __FieldInfo { _Name = m.Name, InternalDeclaringType = this };

                    break;
                }

            }

            return r;
        }



        //public abstract FieldInfo[] GetFields(BindingFlags bindingAttr)
        public virtual FieldInfo[] GetFields(BindingFlags bindingAttr)
        {
            // X:\jsc.svn\examples\javascript\async\Test\TestDelegateObjectScopeInspection\TestDelegateObjectScopeInspection\Application.cs
            // do know how to treat binding attr?

            return GetFields();
        }

        public FieldInfo[] GetFields()
        {
            var a = new List<FieldInfo>();

            foreach (var m in AsExpando().GetFields())
            {
                a.Add(new __FieldInfo { _Name = m.Name, InternalDeclaringType = this });

            }


            return a.ToArray();
        }
        #endregion




        public Expando AsExpando()
        {
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Activator.cs

            return global::ScriptCoreLib.JavaScript.Runtime.Expando.Of(this.TypeHandle.Value);
        }

        public static Type GetTypeFromHandle(RuntimeTypeHandle TypeHandle)
        {
            return new __Type { TypeHandle = TypeHandle };
        }

        public static implicit operator Type(__Type e)
        {
            return (Type)(object)e;
        }
        public static implicit operator __Type(Type e)
        {
            return (__Type)(object)e;
        }


        public override string Name
        {
            get
            {
                // X:\jsc.svn\examples\javascript\forms\test\TestTypeActivatorRef\TestTypeActivatorRef\Class1.cs

                // http://msdn.microsoft.com/en-us/library/dd264736.aspx
                //dynamic constructor = AsExpando().constructor;

                //return constructor.TypeName;
                return (string)Expando.InternalGetMember(
                    AsExpando().constructor, "TypeName");
            }
        }

        public string Namespace
        {
            get
            {
                // jsc does not yet emit namespace info
                return "<Namespace>";
            }
        }

        public string FullName
        {
            get
            {
                return Namespace + "." + Name;
            }
        }

        __TypeReflection Reflection
        {
            get
            {
                return ((__TypeReflection)(object)AsExpando().constructor);
            }
        }



        public Type[] GetInterfaces()
        {
            // does jsc keep interface type info yet?
            // X:\jsc.svn\examples\javascript\Test\TestRoslynYieldReturn\TestRoslynYieldReturn\Application.cs

            // wo do dont we, how else the as is works?
            return new Type[0];
        }

        public global::System.Reflection.ConstructorInfo GetConstructor(Type[] t)
        {
            // X:\jsc.svn\examples\javascript\linq\visualized\VisualizeWhere\VisualizeWhere\Application.cs

            return null;
        }





        #region GetCustomAttributes
        public override object[] GetCustomAttributes(bool inherit)
        {
            return GetCustomAttributes(null, false);
        }


        public override object[] GetCustomAttributes(Type x, bool inherit)
        {
            if (inherit)
                throw new NotSupportedException();

            if (Reflection.GetAttributes == null)
                return new object[0];

            var i = new List<object>();

            foreach (var v in (__AttributeReflection[])Reflection.GetAttributes.apply(Reflection))
            {
                var c = true;

                if (x != null)
                    if (!object.ReferenceEquals(v.Type.prototype, x.TypeHandle.Value))
                        c = false;

                // todo: rebuild to known type
                if (c)
                    i.Add(v.Value);
            }

            return i.ToArray();
        }
        #endregion





        #region InternalEquals
        public bool Equals(Type o)
        {
            return InternalEquals(this, (__Type)(object)o);
        }

        public static bool operator !=(__Type left, __Type right)
        {
            return !InternalEquals(left, right);
        }

        public static bool operator ==(__Type left, __Type right)
        {
            return InternalEquals(left, right);
        }

        public bool Equals(__Type e)
        {
            return InternalEquals(this, e);
        }

        private static bool InternalEquals(__Type x, __Type e)
        {
            // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\DataGridView\DataGridView.DataSource.cs

            object xx = x;
            object ee = e;

            if (xx == null)
            {
                if (ee == null)
                    return true;

                return false;
            }

            if (ee == null)
            {
                return false;
            }

            object a = x.TypeHandle.Value;
            object b = e.TypeHandle.Value;

            return a == b;
        }
        #endregion


        public override Type DeclaringType
        {
            get { throw new NotImplementedException(); }
        }
    }
}
