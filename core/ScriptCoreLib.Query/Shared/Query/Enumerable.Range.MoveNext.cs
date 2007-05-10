using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using ScriptException = global::ScriptCoreLib.JavaScript.System.ScriptException;

namespace ScriptCoreLib.Shared.Query
{

    public static partial class Enumerable
    {



        partial class _RangeIterator_d__91
        {

            public bool MoveNext()
            {
                if (this.__1__state == 0)
                {
                    this.__1__state = -1;
                    this._i_5__92 = 0;
                }
                else if (this.__1__state == 1)
                {
                    this.__1__state = -1;
                    this._i_5__92++;
                }
                else
                {
                    return false;
                }

                if (this._i_5__92 < this.count)
                {
                    this.__2__current = this.start + this._i_5__92;
                    this.__1__state = 1;
                    return true;
                }

                return false;

            }

        }






    }
}
