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
    [Script(Implements = typeof(global::System.Runtime.CompilerServices.CallSite<>))]
    internal class __CallSite<T> : __CallSite
    {
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Runtime\CompilerServices\CallSite.cs

        public T Target;



        public static __CallSite<T> Create(CallSiteBinder binder)
        {
            // T is Func<CallSite, object, IFunction, object>

            //Console.WriteLine("__CallSite.Create");

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
                            if (subject == Native.Window)
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


            throw new NotImplementedException("__CallSite.Create");

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
