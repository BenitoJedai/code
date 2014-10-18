using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Dynamic
{
    // http://referencesource.microsoft.com/#System.Core/Microsoft/Scripting/Actions/DynamicObject.cs

    [Script(Implements = typeof(global::System.Dynamic.DynamicObject))]
    public class __DynamicObject : __IDynamicMetaObjectProvider
    {
        //script: error JSC1000: No implementation found for this native method, please implement [System.Dynamic.DynamicObject.TrySetMember(System.Dynamic.SetMemberBinder, System.Object)]

        //public bool InternalTrySetMember(SetMemberBinder binder, object value)
        //{
        //    Console.WriteLine("__DynamicObject InternalTrySetMember");
        //    return this.TrySetMember(binder, value);
        //}

        public virtual bool TrySetMember(SetMemberBinder binder, object value)
        {
            Console.WriteLine("__DynamicObject TrySetMember");

            return default(bool);
        }

        public virtual bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            // X:\jsc.svn\examples\javascript\test\TestDynamicGetIndex\TestDynamicGetIndex\Application.cs

            result = default(object);

            return default(bool);
        }

        public virtual bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = default(object);

            return default(bool);
        }

        public virtual bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = default(object);

            return default(bool);
        }

        public DynamicMetaObject GetMetaObject(global::System.Linq.Expressions.Expression parameter)
        {
            throw new NotImplementedException();
        }
    }
}
