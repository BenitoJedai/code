﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{
     static partial class __Enumerable
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
