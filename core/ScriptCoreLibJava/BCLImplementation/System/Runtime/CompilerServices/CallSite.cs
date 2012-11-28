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
    [Script]
    internal class __CallSite__InvokeMemberBinder
    {
        public static __CallSite<T> __InvokeMemberBinder<T>(__InvokeMemberBinder InvokeMember)
        {
            var r = default(Delegate);

            var argumentInfo = InvokeMember.argumentInfo;
            var argumentInfoCount = argumentInfo.Count();

            //Console.WriteLine("__InvokeMemberBinder: " + new
            //{
            //    InvokeMember.Name,
            //    argumentInfoCount
            //});

            if (InvokeMember.flags == global::Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags.ResultDiscarded)
            {
                if (argumentInfoCount == 2)
                {
                    r = new Action<__CallSite, object, object>(
                     (site, subject, arg1) =>
                     {
                         object result = null;

                         var x = subject as DynamicObject;
                         if (x != null)
                         {

                             if (x.TryInvokeMember(
                                 (InvokeMemberBinder)(object)InvokeMember,
                                 new[] { arg1 },
                                  out result)
                             )
                                 return;
                         }

                         Console.WriteLine("__CallSite __InvokeMemberBinder " + new { subject });



                         throw new NotImplementedException("__CallSite __InvokeMemberBinder");
                     }
                    );
                }
                else
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

                         Console.WriteLine("__CallSite __InvokeMemberBinder " + new { subject });



                         throw new NotImplementedException("__CallSite __InvokeMemberBinder");
                     }
                    );
                }
            }
            else
            {
                if (argumentInfoCount == 2)
                {
                    r = new Func<__CallSite, object, object, object>(
                     (site, subject, arg1) =>
                     {
                         object result = null;

                         var x = subject as DynamicObject;
                         if (x != null)
                         {

                             if (x.TryInvokeMember(
                                 (InvokeMemberBinder)(object)InvokeMember,
                                 new[] { arg1 },
                                  out result)
                             )
                                 return result;
                         }

                         Console.WriteLine("__CallSite __InvokeMemberBinder " + new { subject });



                         throw new NotImplementedException("__CallSite __InvokeMemberBinder");
                     }
                 );
                }
                else
                {
                    r = new Func<__CallSite, object, object>(
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
                                   return result;
                           }

                           Console.WriteLine("__CallSite __InvokeMemberBinder " + new { subject });



                           throw new NotImplementedException("__CallSite __InvokeMemberBinder");
                       }
                   );
                }
            }

            return r;
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
                    var t = default(Type);

                    // its the same type. no conversion required!
                    if (value == null)
                    {

                    }
                    else
                    {
                        t = value.GetType();
                        if (t == Convert.type)
                            return value;

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

                    Console.WriteLine("__CallSite Convert " + new
                    {
                        value,

                        context = Convert.context,

                        t,
                        Convert.type
                    });

                    // should we do some reflection and conversion?
                    return value;
                }
            );
            return r;
        }

        private static __CallSite<T> __GetMember(__GetMemberBinder GetMember)
        {
            var r = new Func<__CallSite, object, object>(
                (site, subject) =>
                {
                    object result = null;

                    var x = subject as DynamicObject;
                    if (x != null)
                    {
                        if (x.TryGetMember((GetMemberBinder)(object)GetMember, out result))
                            return result;
                    }

                    Console.WriteLine("__CallSite GetMember " + new { subject, GetMember.Name });


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
            // crude casting.
            // this will work in JavaScript.

            return new __CallSite<T> { Target = (T)(object)Target };
        }

    }

    [Script(Implements = typeof(global::System.Runtime.CompilerServices.CallSite))]
    internal class __CallSite
    {
        public CallSiteBinder Binder { get; set; }
    }
}
