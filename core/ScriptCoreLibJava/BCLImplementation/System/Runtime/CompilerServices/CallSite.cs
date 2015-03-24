using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.Microsoft.CSharp;
using ScriptCoreLib.Shared.BCLImplementation.System.Dynamic;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Runtime.CompilerServices
{
    // see: http://msdn.microsoft.com/en-us/library/System.Runtime.CompilerServices.CallSite.aspx
    // http://referencesource.microsoft.com/#System.Core/Microsoft/Scripting/Actions/CallSite.cs
    // https://github.com/mono/mono/blob/master/mcs/class/dlr/Runtime/Microsoft.Scripting.Core/Actions/CallSite.cs
    // https://github.com/mono/mono/blob/master/mcs/tools/cil-strip/Mono.Cecil/CallSite.cs
    // https://github.com/mono/mono/blob/master/mcs/class/dlr/Runtime/Microsoft.Scripting.Core/Actions/CallSiteOps.cs

    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Runtime\CompilerServices\CallSite.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\CompilerServices\CallSite.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Runtime\CompilerServices\CallSite.cs


    [Obsolete("can we move it into Shared just yet?")]
    [Script(Implements = typeof(global::System.Runtime.CompilerServices.CallSite))]
    internal class __CallSite
    {
        public CallSiteBinder Binder { get; set; }
    }

    [Script]
    internal class __CallSite__InvokeMemberBinder
    {
        //        E/AndroidRuntime(27179): Caused by: java.lang.RuntimeException: __CallSite __InvokeMemberBinder
        //E/AndroidRuntime(27179):        at ScriptCoreLibJava.BCLImplementation.System.Runtime.CompilerServices.__CallSite__InvokeMemberBinder___c__DisplayClass8_1.___InvokeMemberBinder_b__0(__CallSite__InvokeMemberBinder___c__DisplayClass8_1.java:50)

        public static __CallSite<T> __InvokeMemberBinder<T>(__InvokeMemberBinder InvokeMember)
        {
            var r = default(Delegate);

            var argumentInfo = InvokeMember.argumentInfo;
            var argumentInfoCount = argumentInfo.Count();

            Console.WriteLine("__InvokeMemberBinder: " + new
            {
                InvokeMember.Name,
                argumentInfoCount
            });

            var IsReturnVoid = InvokeMember.flags == global::Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags.ResultDiscarded;


            #region (arg1)
            if (argumentInfoCount == 2)
            {
                if (IsReturnVoid)
                {
                    r = new Action<__CallSite, object, object>(
                     (site, subject, arg1) =>
                     {
                         object result = null;

                         #region xDynamicObject
                         var xDynamicObject = subject as DynamicObject;
                         if (xDynamicObject != null)
                         {

                             if (xDynamicObject.TryInvokeMember(
                                 (InvokeMemberBinder)(object)InvokeMember,
                                 new[] { arg1 },
                                  out result)
                             )
                                 return;
                         }
                         #endregion

                         var TargetType = subject.GetType();
                         var Candidates = TargetType.GetMethods().Where(m => m.Name == InvokeMember.Name).ToArray();


                         Console.WriteLine("__CallSite __InvokeMemberBinder arg1 " + new { subject, TargetType, InvokeMember.Name, arg1, Candidates = Candidates.Length });

                         if (Candidates.Length == 1)
                         {
                             Candidates[0].Invoke(
                                 subject,
                                 new object[] { arg1 }
                             );
                             return;
                         }


                         throw new NotImplementedException("__CallSite __InvokeMemberBinder");
                     }
                    );

                    return r;
                }

                r = new Func<__CallSite, object, object, object>(
                 (site, subject, arg1) =>
                 {
                     object result = null;

                     #region DynamicObject
                     var xDynamicObject = subject as DynamicObject;
                     if (xDynamicObject != null)
                     {

                         if (xDynamicObject.TryInvokeMember(
                             (InvokeMemberBinder)(object)InvokeMember,
                             new[] { arg1 },
                              out result)
                         )
                             return result;
                     }
                     #endregion

                     Console.WriteLine("__CallSite __InvokeMemberBinder arg1 " + new { subject, arg1 });



                     throw new NotImplementedException("__CallSite __InvokeMemberBinder");
                 }
             );

                return r;

            }
            #endregion

            #region (void)
            if (IsReturnVoid)
            {
                r = new Action<__CallSite, object>(
                 (site, subject) =>
                 {
                     object result = null;

                     var x = subject as DynamicObject;
                     if (x != null)
                     {

                         if (x.TryInvokeMember(
                             (InvokeMemberBinder)(object)InvokeMember,
                             new object[0],
                              out result)
                         )
                             return;
                     }

                     // __CallSite __InvokeMemberBinder { subject = DIDIsoDepCertificate.Activities.ApplicationActivity@2057ddf8 }


                     Console.WriteLine("__CallSite __InvokeMemberBinder " + new { subject });



                     throw new NotImplementedException("__CallSite __InvokeMemberBinder");
                 }
                );

                return r;
            }

            r = new Func<__CallSite, object, object>(
               (site, subject) =>
               {
                   object result = null;

                   #region xDynamicObject
                   var xDynamicObject = subject as DynamicObject;
                   if (xDynamicObject != null)
                   {

                       if (xDynamicObject.TryInvokeMember(
                           (InvokeMemberBinder)(object)InvokeMember,
                           new object[0],
                            out result)
                       )
                           return result;
                   }
                   #endregion

                   Console.WriteLine("__CallSite __InvokeMemberBinder " + new { subject });


                   throw new NotImplementedException("__CallSite __InvokeMemberBinder");
               }
           );

            return r;
            #endregion

        }
    }

    [Script(Implements = typeof(global::System.Runtime.CompilerServices.CallSite<>))]
    internal class __CallSite<T> : __CallSite
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\CompilerServices\CallSite.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121101/20121127

        public T Target;



        private static __CallSite<T> __Convert(__Binder.__Convert Convert)
        {
            var r = new Func<__CallSite, object, object>(
                (site, value) =>
                {
                    var equals = false;

                    var t = default(Type);

                    // its the same type. no conversion required!
                    if (value == null)
                    {
                        #region why is the value null?

                        if (Convert.type == typeof(int))
                        {
                            var value_int32 = (int)0;

                            return value_int32;
                        }

                        if (Convert.type == typeof(long))
                        {
                            var value_int64 = (long)0;

                            return value_int64;
                        }
                        #endregion
                    }
                    else
                    {
                        t = value.GetType();

                        equals = t == Convert.type;

                        if (equals)
                            return value;

                        if (value is string)
                        {
                            // getType is unavailable at API 8
                            // as will always return string

                            if (Convert.type == typeof(int))
                            {
                                return long.Parse((string)value);
                            }

                            if (Convert.type == typeof(long))
                            {
                                return long.Parse((string)value);
                            }
                        }


                        if (value is int)
                        {
                            if (Convert.type == typeof(long))
                            {
                                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121101/20121127

                                var value_int32 = (int)value;
                                var value_int64 = (long)value_int32;

                                return value_int64;
                            }
                        }
                    }

                    var message = "__CallSite Convert " + new
                    {
                        value,

                        context = Convert.context,

                        t,
                        Convert.type,
                        equals
                    };

                    throw new InvalidOperationException(message);
                }
            );
            return r;
        }

        private static __CallSite<T> __GetMember(__GetMemberBinder GetMember)
        {
            //Console.WriteLine("__CallSite GetMember prep " + new { GetMember.Name });

            var r = new Func<__CallSite, object, object>(
                (site, subject) =>
                {
                    //Console.WriteLine("__CallSite GetMember " + new { subject, GetMember.Name });


                    object result = null;

                    var x = subject as DynamicObject;
                    if (x != null)
                    {
                        //Console.WriteLine("__CallSite GetMember is DynamicObject " + new { subject, GetMember.Name });

                        if (x.TryGetMember((GetMemberBinder)(object)GetMember, out result))
                        {
                            return result;
                        }
                        else
                        {
                            //Console.WriteLine("__CallSite GetMember is DynamicObject TryGetMember false");
                        }
                    }

                    //Console.WriteLine("__CallSite GetMember not DynamicObject " + new { subject, GetMember.Name });


                    //var value = 

                    ////new IFunction("subject", "name", "return subject[name];").apply(null,
                    ////    subject,
                    ////    GetMember.name
                    ////);

                    //return value;

                    throw new NotImplementedException("__CallSite GetMember");
                }
            );
            return r;
        }



        public static __CallSite<T> Create(CallSiteBinder binder)
        {
            // DynamicObject

            #region InvokeMemberBinder
            {
                var InvokeMemberBinder = (object)binder as __InvokeMemberBinder;
                if (InvokeMemberBinder != null)
                {
                    return __CallSite__InvokeMemberBinder.__InvokeMemberBinder<T>(InvokeMemberBinder);
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


                            throw new InvalidOperationException("SetMemberBinder");
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
                    return __GetMember(GetMember);
                }
            }
            #endregion

            #region Convert
            {
                var Convert = (object)binder as __Binder.__Convert;
                if (Convert != null)
                {
                    return __Convert(Convert);
                }
            }
            #endregion

            throw new NotImplementedException("__CallSite.Create " + new { binder = binder.GetType().FullName });
        }

        public static implicit operator __CallSite<T>(Delegate Target)
        {
            if (Target == null)
                throw new InvalidOperationException("CallSite Target not initialized!");

            // crude casting.
            // this will work in JavaScript.

            return new __CallSite<T> { Target = (T)(object)Target };
        }

    }


}
