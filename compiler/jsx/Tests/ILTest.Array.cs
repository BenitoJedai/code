using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Reflection;

namespace jsx.Tests
{

    public partial class ILTest
    {
        public void JaggedArray()
        {
            var i = new int [][] {
                new int [] { 1, 2, 3, 4},
                new int [] { 4, 5, 6, 7},
                new int [] { 4, 5, 6, 7, 54546}
            };

            DoSomethingWith(i);

            DoSomethingWith(i[2][3]);
        }
    }

}
