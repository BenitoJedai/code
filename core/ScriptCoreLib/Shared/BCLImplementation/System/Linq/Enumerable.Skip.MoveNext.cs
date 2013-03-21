using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{

     static partial class __Enumerable
    {


        partial class _SkipIterator_d__40<TSource>
        {

            public bool MoveNext()
            {



                if ((this.__1__state == 0) || (this.__1__state == 2))
                {
                    if (this.__1__state == 0)
                    {
                        this.__1__state = -1;

                        this._e_5__4e = this.source.AsEnumerable().GetEnumerator();


                        while (this.count > 0 && this._e_5__4e.MoveNext())
                        {
                            this.count--;
                        }

                        if ((this.count > 0))
                        {
                            this.__m__Finally43();
                            return false;
                        }

                    }
                    else if (this.__1__state == 2)
                    {
                        this.__1__state = 1;

                    }

                    if (this._e_5__4e.MoveNext())
                    {
                        this._element_5__41 = this._e_5__4e.Current;
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
                if (this._e_5__4e != null)
                {
                    this._e_5__4e.Dispose();
                }
            }

        }






    }
}
