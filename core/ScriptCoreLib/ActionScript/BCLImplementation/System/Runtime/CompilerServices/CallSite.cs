using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Shared.BCLImplementation.Microsoft.CSharp;
using ScriptCoreLib.Shared.BCLImplementation.System.Dynamic;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Runtime.CompilerServices
{
    [Obsolete]
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


                            DynamicContainer.SetValue(subject, SetMember.Name, value);
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

                            return DynamicContainer.GetValue(subject, GetMember.Name);
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
