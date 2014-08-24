using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.BCLImplementation.Microsoft.CSharp;
using ScriptCoreLib.Shared.BCLImplementation.System.Dynamic;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices
{
    // see: http://msdn.microsoft.com/en-us/library/System.Runtime.CompilerServices.CallSite.aspx
    // http://referencesource.microsoft.com/#System.Core/Microsoft/Scripting/Actions/CallSite.cs
    // https://github.com/mono/mono/blob/master/mcs/class/dlr/Runtime/Microsoft.Scripting.Core/Actions/CallSite.cs
    // https://github.com/mono/mono/blob/master/mcs/tools/cil-strip/Mono.Cecil/CallSite.cs
    // https://github.com/mono/mono/blob/master/mcs/class/dlr/Runtime/Microsoft.Scripting.Core/Actions/CallSiteOps.cs

    // 
    [Script(Implements = typeof(global::System.Runtime.CompilerServices.CallSite))]
    internal class __CallSite
    {
        // tested by
        // can we move it into Shared?
        // ActionSctipt is also on board?


        public CallSiteBinder Binder { get; set; }
    }

    [Script(Implements = typeof(global::System.Runtime.CompilerServices.CallSite<>))]
    internal class __CallSite<T> : __CallSite
    {
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Runtime\CompilerServices\CallSite.cs

        public T Target;



        public static __CallSite<T> Create(CallSiteBinder binder)
        {
            // T is Func<CallSite, object, IFunction, object>

            //Console.WriteLine("__CallSite.Create");

            // see also: X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\Microsoft\CSharp\RuntimeBinder\Binder.cs

            #region __GetIndexBinder
            {
                var xGetIndexBinder = (object)binder as __GetIndexBinder;
                if (xGetIndexBinder != null)
                {
                    var r = new Func<__CallSite, object, object, object>(
                        (site, subject, key) =>
                        {
                            // X:\jsc.svn\examples\javascript\test\TestDynamicGetIndex\TestDynamicGetIndex\Application.cs

                            // tested by?
                            var x = subject as DynamicObject;
                            if (x != null)
                            {
                                //Console.WriteLine("__SetMemberBinder DynamicObject");
                                var result = default(object);

                                if (x.TryGetIndex((GetIndexBinder)(object)xGetIndexBinder, new[] { key }, out result))
                                {
                                    return result;
                                }
                            }

                            //Console.WriteLine("__CallSite __GetIndexBinder " + new { subject, key });

                            var value = ScriptCoreLib.JavaScript.Runtime.Expando.InternalGetMember(
                                subject, key
                            );

                            //var value = new IFunction("subject", "name", "return subject[name];").apply(null,
                            //    subject,
                            //    GetMember.Name
                            //);

                            return value;
                        }
                    );
                    return r;
                }
            }
            #endregion


            #region __SetIndexBinder
            {
                var __SetIndexBinder = (object)binder as __SetIndexBinder;
                if (__SetIndexBinder != null)
                {
                    //0x006e . ldsfld                     [TestDynamicSetIndexer] TestDynamicSetIndexer.Application+<.ctor>o__SiteContainer0.<>p__Site1 : CallSite`1<(CallSite, object, string, string) -> Object>
                    //0x0073 . ldfld                      [System.Core] System.Runtime.CompilerServices.CallSite`1[[System.Func`5[[System.Runtime.CompilerServices.CallSite, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]].Target : (CallSite, object, string, string) -> Object
                    //0x0078 . . ldsfld                   arg1 <- [TestDynamicSetIndexer] TestDynamicSetIndexer.Application+<.ctor>o__SiteContainer0.<>p__Site1 : CallSite`1<(CallSite, object, string, string) -> Object>
                    //0x007d . . . ldloc.2                arg2 <- loc.2 : object
                    //0x007e . . . . ldstr                str0 <- "preview:"
                    //0x0083 . . . . . ldloc.0            str1 <- loc.0 : string
                    //0x0084 . . . . call                 arg3 <- [mscorlib] System.String.Concat(str0 : string, str1 : string) : String
                    //0x0089 . . . . . ldloc.1            arg4 <- loc.1 : string
                    //0x008a . callvirt                   [mscorlib] System.Func`5.Invoke(arg1 : CallSite, arg2 : object, arg3 : string, arg4 : string) : Object
                    //0x008f pop 
                    //0x0090 . ldsfld                     [TestDynamicSetIndexer] TestDynamicSetIndexer.Application+<.ctor>o__SiteContainer0.<>p__Site2 : CallSite`1<(CallSite, Type, object) -> void>
                    //0x0095 brtrue.s 
                    //0x0095 -> 0x0097 0x00da 


                    var r = new Func<__CallSite, object, object, object, object>(
                        (site, subject, key, value) =>
                        {
                            //var x = subject as DynamicObject;
                            //if (x != null)
                            //{
                            //    //Console.WriteLine("__SetMemberBinder DynamicObject");

                            //    if (x.TrySetIndex((SetMemberBinder)(object)SetMember, value))
                            //    {
                            //        return null;
                            //    }
                            //}

                            //#region special rule - boundary DOM / BCL
                            //if (subject == Native.Window)
                            //{
                            //    var xx = value as Delegate;

                            //    if (xx != null)
                            //    {
                            //        value = IFunction.OfDelegate(xx);
                            //    }
                            //}
                            //#endregion

                            //Console.WriteLine("__CallSite SetMember " + new { subject, SetMember.name, value });

                            ScriptCoreLib.JavaScript.Runtime.Expando.InternalSetMember(
                                subject,
                                key,
                                value
                            );

                            //new IFunction("subject", "name", "value", "subject[name] = value;").apply(null,
                            //    subject,
                            //    SetMember.Name,
                            //    value
                            //);

                            return null;
                        }
                    );
                    return r;
                }
            }
            #endregion

            #region SetMember
            {
                var SetMember = (object)binder as __SetMemberBinder;
                if (SetMember != null)
                {
                    var r = new Func<__CallSite, object, object, object>(
                        (site, subject, value) =>
                        {
                            var x = subject as DynamicObject;
                            if (x != null)
                            {
                                //Console.WriteLine("__SetMemberBinder DynamicObject");

                                if (x.TrySetMember((SetMemberBinder)(object)SetMember, value))
                                {
                                    return null;
                                }
                            }

                            #region special rule - boundary DOM / BCL
                            if (subject == Native.window)
                            {
                                var xx = value as Delegate;

                                if (xx != null)
                                {
                                    value = IFunction.OfDelegate(xx);
                                }
                            }
                            #endregion

                            //Console.WriteLine("__CallSite SetMember " + new { subject, SetMember.name, value });

                            ScriptCoreLib.JavaScript.Runtime.Expando.InternalSetMember(
                                subject,
                                SetMember.Name,
                                value
                            );

                            //new IFunction("subject", "name", "value", "subject[name] = value;").apply(null,
                            //    subject,
                            //    SetMember.Name,
                            //    value
                            //);

                            return null;
                        }
                    );
                    return r;
                }
            }
            #endregion

            #region GetMember
            {
                var GetMember = (object)binder as __GetMemberBinder;
                if (GetMember != null)
                {
                    var r = new Func<__CallSite, object, object>(
                        (site, subject) =>
                        {
                            var x = subject as DynamicObject;
                            if (x != null)
                            {
                                //Console.WriteLine("__SetMemberBinder DynamicObject");
                                var result = default(object);

                                if (x.TryGetMember((GetMemberBinder)(object)GetMember, out result))
                                {
                                    return result;
                                }
                            }
                            //Console.WriteLine("__CallSite GetMember " + new { subject, GetMember.name });

                            var value = ScriptCoreLib.JavaScript.Runtime.Expando.InternalGetMember(
                                subject, GetMember.Name
                            );

                            //var value = new IFunction("subject", "name", "return subject[name];").apply(null,
                            //    subject,
                            //    GetMember.Name
                            //);

                            return value;
                        }
                    );
                    return r;
                }
            }
            #endregion

            #region Convert
            {
                var Convert = (object)binder as __Binder.__Convert;
                if (Convert != null)
                {
                    var r = new Func<__CallSite, object, object>(
                        (site, value) =>
                        {
                            //Console.WriteLine("__CallSite Convert " + new { value });

                            // should we do some reflection and conversion?
                            return value;
                        }
                    );
                    return r;
                }
            }
            #endregion


            Console.WriteLine(new { binder });

            throw new NotImplementedException("__CallSite.Create ");

        }

        public static implicit operator __CallSite<T>(Delegate Target)
        {
            // crude casting.
            // this will work in JavaScript.

            return new __CallSite<T> { Target = (T)(object)Target };
        }

    }


}
