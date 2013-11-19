using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNestedInheritedType
{
    class XQueries
    {
        // cctor

        public class Insert
        {
        }
    }

    class XTable : XQueries
    {

    }

    class Program
    {
        static void Main(string[] args)
        {

            // seems to reference XQueries as needed.
            var r = default(XTable.Insert);
        }
    }
}
