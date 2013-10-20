using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestServerWhereSelector
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {

        public async Task<int[]> GetData(__closure c)
        {
            var source = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };


            var value = source.Where(x => c.__Invoke(x)).ToArray();

            return value;
        }

    }



    public class __closure
    {
        public int __i;

        public bool __Invoke(int x)
        {
            return x > __i;
        }
    }

    public class Mashup
    {

        public int[] GetData(Func<int, bool> c)
        {
            var source = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };


            var value = source.Where(x => c(x)).ToArray();

            return value;
        }

    }

}
