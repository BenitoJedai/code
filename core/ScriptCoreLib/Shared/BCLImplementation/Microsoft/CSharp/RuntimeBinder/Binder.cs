using Microsoft.CSharp.RuntimeBinder;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.Microsoft.CSharp
{
    [Script(Implements = typeof(global::Microsoft.CSharp.RuntimeBinder.Binder))]
    internal static class __Binder
    {
        #region SetMember
        [Script]
        public class __SetMemberBinder : __CallSiteBinder
        {
            public CSharpBinderFlags flags;
            public string name;
            public Type context;
            public IEnumerable<CSharpArgumentInfo> argumentInfo;
        }

        public static CallSiteBinder SetMember(
            CSharpBinderFlags flags,
            string name,
            Type context,
            IEnumerable<CSharpArgumentInfo> argumentInfo
            )
        {
            // was used before: CSharpArgumentInfo.Create

            // will be called after: __CallSite.Create
            return (CallSiteBinder)(object)new __SetMemberBinder
            {
                flags = flags,
                name = name,
                context = context,
                argumentInfo = argumentInfo
            };
        }
        #endregion

        #region Convert
        [Script]
        public class __Convert : __CallSiteBinder
        {
            public CSharpBinderFlags flags;
            public Type type;
            public Type context;
        }


        public static CallSiteBinder Convert(CSharpBinderFlags flags, Type type, Type context)
        {
            return (CallSiteBinder)(object)new __Convert
            {
                flags = flags,
                type = type,
                context = context,
            };
        }
        #endregion

        #region GetMember
        [Script]
        public class __GetMemberBinder : __CallSiteBinder
        {
            public CSharpBinderFlags flags;
            public string name;
            public Type context;
            public IEnumerable<CSharpArgumentInfo> argumentInfo;
        }

        public static CallSiteBinder GetMember(
            CSharpBinderFlags flags, 
            string name, 
            Type context, 
            IEnumerable<CSharpArgumentInfo> argumentInfo
            )
        {
            return (CallSiteBinder)(object)new __GetMemberBinder
            {
                flags = flags,
                name = name,
                context = context,
                argumentInfo = argumentInfo
            };
        }
        #endregion
    }
}
