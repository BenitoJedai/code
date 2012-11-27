using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Dynamic
{
    [Script(Implements = typeof(global::System.Dynamic.DynamicObject))]
    internal class __DynamicObject : __IDynamicMetaObjectProvider
    {
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
    }
}
