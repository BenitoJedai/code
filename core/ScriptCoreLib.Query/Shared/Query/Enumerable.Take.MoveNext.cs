using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;

namespace ScriptCoreLib.Shared.Query
{

    internal static partial class __Enumerable
    {



        partial class _TakeIterator_d__40<TSource>
        {

            public bool MoveNext()
            {
                if (this.__1__state == 0 || this.__1__state == 2)
                {
                    if (this.__1__state == 0)
                    {
                        this.__1__state = -1;
                        if (this.count <= 0)
                        {
                            this.__m__Finally43();
                            return false;
                        }
                        this.__7__wrap42 = this.source.AsEnumerable().GetEnumerator();


                    }
                    else if (this.__1__state == 2)
                    {
                        this.__1__state = 1;
						this.count--;
                        if (this.count == 0)
                        {
                            this.__m__Finally43();
                            return false;
                        }
                    }

                    if (this.__7__wrap42.MoveNext())
                    {
                        this._element_5__41 = this.__7__wrap42.Current;
                        this.__2__current = this._element_5__41;
                        this.__1__state = 2;
                        return true;
                    }
                }

                return false;
            }

            private void __m__Finally43()
            {
                this.__1__state = -1;
                if (this.__7__wrap42 != null)
                {
                    this.__7__wrap42.Dispose();
                }
            }

        }






    }
}
