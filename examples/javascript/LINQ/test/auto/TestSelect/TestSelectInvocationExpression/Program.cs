using ScriptCoreLib.Query.Experimental;
using System;

namespace TestSelectInvocationExpression
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<string, string, string> ff =
                (arg0, arg1) => "[" + arg0 + "]";

            var f = (
                from x in new xTable()
                //let yy = 6
                let xx = "not used"





                //select ff(x.Tag, xx)
                select new
                {
                    c = xx,

                    a = new[] { 
                        new { g = ff(x.Tag, xx) }, 
                        new { g = ff(x.Tag, "other") } 
                    }
                }

            ).FirstOrDefault();

            //var z = f.x.field1;

        }
    }
}
