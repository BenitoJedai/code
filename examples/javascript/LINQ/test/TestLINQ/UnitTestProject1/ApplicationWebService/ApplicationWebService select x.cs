using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScriptCoreLib.Query.Experimental;
using System.Diagnostics;

namespace TestLINQ
{
    public partial class ApplicationWebService
    {
        [TestMethod]
        public void select_x()
        {
            var q =
                from x in new xTable()

                let scalar0 = (
                    from xx in new xTable()
                    //let inception = 1

                    //let inception = xx.field1
                    let inception2 = xx.field2

                    where xx.field1 == 44

                    // is this missing?
                    select new { xx.field2 }
                )

                let gap1 =
                    from xx in new xTable()
                    select new { xx.field2 }

                let gap2 = 0
                //let gap3 = 0
                let gap3 = 1
                let gap4 = 2
                let gap5 = 3
                let gap6 = 6
                let gap7 = 7

                //where gap5 > 4

                //let scalar1 = scalar0.FirstOrDefault()
                let count1 = gap1.Count()

                select new { x, gap3 };

            //select x.Key;
            //select x.field1 + x.field2;
            //let field3 = x.field1 + x.field2
            //where x.field2 > 33

            //where field3 > 33

            //select new[] { 
            //    new {z = x.field1, u = x.Tag.ToUpper(), field3 }, 
            //    new {z = x.field2, u = x.Tag.ToLower(), field3 }
            //};

            var f = q.FirstOrDefault();

            if (Debugger.IsAttached)
                Debugger.Break();
        }
    }
}
