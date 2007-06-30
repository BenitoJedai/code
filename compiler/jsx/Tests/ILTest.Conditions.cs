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
        public static void Conditions()
        {
            DoSomethingWith(
                GetBoolean() && !GetBoolean() || GetBoolean() || GetBoolean() && GetBoolean() && (7 - GetInteger()) > 6
                ?
                GetValue<int>()
                :
                GetValue<int>()
                );


        }

    }

}
