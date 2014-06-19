using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScriptCoreLib.Query.Experimental;
using System.Diagnostics;

namespace TestLINQ
{
    public partial class ApplicationWebService
    {
        [TestMethod]
        public void select_xKey()
        {
            var q =
                from x in new xTable()
                select x.Key;

            var f = q.FirstOrDefault();

            if (Debugger.IsAttached)
                Debugger.Break();
        }
    }
}
