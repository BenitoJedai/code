using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.BCLImplementation.Microsoft.CSharp;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices
{
    [Script(Implements = typeof(global::System.Runtime.CompilerServices.CallSite<>))]
    internal class __CallSite<T> : __CallSite
    {
        public T Target;



        public static __CallSite<T> Create(CallSiteBinder binder)
        {
            // T is Func<CallSite, object, IFunction, object>

            //Console.WriteLine("__CallSite.Create");

            #region SetMember
            {
                var SetMember = (object)binder as __Binder.__SetMemberBinder;
                if (SetMember != null)
                {
                    var r = new Func<__CallSite, object, object, object>(
                        (site, subject, value) =>
                        {
                            #region special rule - boundary DOM / BCL
                            if (subject == Native.Window)
                            {
                                var x = value as Delegate;

                                if (x != null)
                                {
                                    value = IFunction.OfDelegate(x);
                                }
                            }
                            #endregion

                            //Console.WriteLine("__CallSite SetMember " + new { subject, SetMember.name, value });

                            new IFunction("subject", "name", "value", "subject[name] = value;").apply(null,
                                subject,
                                SetMember.name,
                                value
                            );

                            return null;
                        }
                    );
                    return r;
                }
            }
            #endregion

            #region GetMember
            {
                var GetMember = (object)binder as __Binder.__GetMemberBinder;
                if (GetMember != null)
                {
                    var r = new Func<__CallSite, object, object>(
                        (site, subject) =>
                        {
                            //Console.WriteLine("__CallSite GetMember " + new { subject, GetMember.name });

                            var value = new IFunction("subject", "name", "return subject[name];").apply(null,
                                subject,
                                GetMember.name
                            );

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
