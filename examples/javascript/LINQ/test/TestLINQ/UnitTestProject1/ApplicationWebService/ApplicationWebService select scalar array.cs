using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScriptCoreLib.Query.Experimental;
using System.Diagnostics;

namespace TestLINQ
{
    public partial class ApplicationWebService
    {
        [TestMethod]
        public void select_scalar_array()
        {
            var q =
               from x in new xTable()
               //select x.Key;
               //select x.field1 + x.field2;
               select new[] { 
                    new {z = x.field1 }, 
                    new {z = x.field2 }
                };

            var f = q.FirstOrDefault();

            if (Debugger.IsAttached)
                Debugger.Break();
        }
    }
}
