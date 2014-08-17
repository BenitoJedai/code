using Microsoft.CSharp.RuntimeBinder;
using ScriptCoreLib.Shared.BCLImplementation.System.Dynamic;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.Microsoft.CSharp
{
    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140714
    // X:\opensource\github\WootzJs\WootzJs.Runtime\Microsoft\CSharp\RuntimeBinder\Binder.cs
    // http://referencesource.microsoft.com/#Microsoft.CSharp/Microsoft/CSharp/RuntimeBinder/Binder.cs
    // https://github.com/mono/mono/blob/a31c107f59298053e4ff17fd09b2fa617b75c1ba/mcs/class/Microsoft.CSharp/Microsoft.CSharp.RuntimeBinder/Binder.cs

    [Script(Implements = typeof(global::Microsoft.CSharp.RuntimeBinder.Binder))]
    public static partial class __Binder
    {
        // how does this 

        public static CallSiteBinder GetIndex(CSharpBinderFlags flags, Type context, IEnumerable<CSharpArgumentInfo> argumentInfo)
        {
            // X:\jsc.svn\examples\javascript\test\TestDynamicGetIndex\TestDynamicGetIndex\Application.cs


            return (CallSiteBinder)(object)new __GetIndexBinder
            {

                flags = flags,
                context = context,
                argumentInfo = argumentInfo
            };
        }

        public static CallSiteBinder SetIndex(
            CSharpBinderFlags flags,
            Type context,
            IEnumerable<CSharpArgumentInfo> argumentInfo
            )
        {
            // tested by X:\jsc.svn\examples\javascript\Test\TestDynamicSetIndexer\TestDynamicSetIndexer\Application.cs

         
            //0x002c . ldc.i4.0                   flags <- (CSharpBinderFlags) None = 0 (0x00000000)
            
            //0x002d . . ldtoken                  handle <- [TestDynamicSetIndexer] TestDynamicSetIndexer.Application
            //0x0032 . . call                     context <- [mscorlib] System.Type.GetTypeFromHandle(handle : struct RuntimeTypeHandle) : Type

            //0x0037 . . . ldc.i4.3               3 (0x00000003)
            //0x0038 . . . newarr                 [Microsoft.CSharp] Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo
            //0x003d . . stloc.3                  loc.3 : CSharpArgumentInfo[]

            //0x003e . . . ldloc.3                loc.3 : CSharpArgumentInfo[]
            //0x003f . . . . ldc.i4.0             0 (0x00000000)
            //0x0040 . . . . . ldc.i4.0           flags <- (CSharpArgumentInfoFlags) None = 0 (0x00000000)
            //0x0041 . . . . . . ldnull           name <- null
            //0x0042 . . . . . call               [Microsoft.CSharp] Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo.Create(flags : CSharpArgumentInfoFlags : int, name : string) : CSharpArgumentInfo
            //0x0047 . . stelem.ref 

            //0x0048 . . . ldloc.3                loc.3 : CSharpArgumentInfo[]
            //0x0049 . . . . ldc.i4.1             1 (0x00000001)
            //0x004a . . . . . ldc.i4.1           flags <- (CSharpArgumentInfoFlags) UseCompileTimeType = 1 (0x00000001)
            //0x004b . . . . . . ldnull           name <- null
            //0x004c . . . . . call               [Microsoft.CSharp] Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo.Create(flags : CSharpArgumentInfoFlags : int, name : string) : CSharpArgumentInfo
            //0x0051 . . stelem.ref 

            //0x0052 . . . ldloc.3                loc.3 : CSharpArgumentInfo[]
            //0x0053 . . . . ldc.i4.2             2 (0x00000002)
            //0x0054 . . . . . ldc.i4.1           flags <- (CSharpArgumentInfoFlags) UseCompileTimeType = 1 (0x00000001)
            //0x0055 . . . . . . ldnull           name <- null
            //0x0056 . . . . . call               [Microsoft.CSharp] Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo.Create(flags : CSharpArgumentInfoFlags : int, name : string) : CSharpArgumentInfo
            //0x005b . . stelem.ref 
            
            //0x005c . . . ldloc.3                argumentInfo <- loc.3 : CSharpArgumentInfo[]

            //0x005d . call                       binder <- [Microsoft.CSharp] Microsoft.CSharp.RuntimeBinder.Binder.SetIndex(flags : CSharpBinderFlags : int, context : Type, argumentInfo : IEnumerable`1<CSharpArgumentInfo>) : CallSiteBinder

            //0x0062 . call                       [System.Core] System.Runtime.CompilerServices.CallSite`1<(CallSite, object, string, string) -> Object>.Create(binder : CallSiteBinder) : CallSite`1
            //0x0067 stsfld                       [TestDynamicSetIndexer] TestDynamicSetIndexer.Application+<.ctor>o__SiteContainer0.<>p__Site1 : CallSite`1<(CallSite, object, string, string) -> Object>
            //0x006c br.s 
            //0x002a 0x006c -> 0x006e
            //0x002a 0x006c -> 0x006e ldsfld 

            // global::Microsoft.CSharp.RuntimeBinder.CSharpSetIndexBinder

            // see also: X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\CompilerServices\CallSite.cs

            return (CallSiteBinder)(object)new __SetIndexBinder
            {

                flags = flags,
                context = context,
                argumentInfo = argumentInfo
            };
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
                Name = name,

                flags = flags,
                context = context,
                argumentInfo = argumentInfo
            };
        }


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



        public static CallSiteBinder GetMember(
            CSharpBinderFlags flags,
            string name,
            Type context,
            IEnumerable<CSharpArgumentInfo> argumentInfo
            )
        {
            return (CallSiteBinder)(object)new __GetMemberBinder
            {
                Name = name,

                flags = flags,
                context = context,
                argumentInfo = argumentInfo
            };
        }

        public static CallSiteBinder InvokeMember(
            CSharpBinderFlags flags,
            string name,
            IEnumerable<Type> typeArguments,
            Type context,
            IEnumerable<CSharpArgumentInfo> argumentInfo
            )
        {
            return (CallSiteBinder)(object)new __InvokeMemberBinder
            {
                Name = name,

                flags = flags,
                typeArguments = typeArguments,

                context = context,
                argumentInfo = argumentInfo
            };
        }
    }
}
