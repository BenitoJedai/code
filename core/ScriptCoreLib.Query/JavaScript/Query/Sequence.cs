using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using ScriptException = global::ScriptCoreLib.JavaScript.System.ScriptException;

namespace ScriptCoreLib.JavaScript.Query
{
    [Script]
    internal static class Sequence
    {
        static Sequence()
        {
            // this is just a hack, future version of jsc should fix array vs enumerable issues

            Shared.Query.Sequence.InternalAsEnumerableImplementation =
                delegate(IEnumerable e)
                {
                    var u = ScriptCoreLib.JavaScript.Runtime.Expando.Of(e);

                    if (!u.IsArray)
                    {
                        if (u.prototype == null)
                        {
                            if (ScriptCoreLib.JavaScript.Runtime.Expando.InternalIsMember(u, "length"))
                            {
                                // DOM list ?
                            }
                            else return e;
                        }
                        else return e;
                    }

                    return (ScriptCoreLib.Shared.Query.SZArrayEnumerator<object>)u.To<object[]>();
                };
        }
    }
}
