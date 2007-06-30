using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using System.Reflection;

namespace jsx.Tests
{
    public partial class ILTest
    {
        IEnumerator<int> wrap1;

         
        public void Goto1()
        {
            if (GetValue<bool>())
            {
                goto skip_outer;
            }

            #region foreach (var v in GetValues())
            wrap1 = GetValues().GetEnumerator();
            goto loop1;
        loop1_continue:
            var v = wrap1.Current;
            #endregion


            DoSomethingWith(v);

        skip_outer: ;

            foreach (var z in GetValues())
            {
                DoSomethingWith(z);

            }


            #region end foreach
        loop1: if (wrap1.MoveNext()) goto loop1_continue;
            #endregion

        }
    }

}
