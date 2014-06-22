using ScriptCoreLib.Query.Experimental;
using System;

namespace TestSelectInvocationExpression
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<string, string> ff =
                arg0 => "[" + arg0 + "]";

            var f = (
                from x in new xTable()
                //let yy = 6
                let xx = "not used"





                select ff(x.Tag)

            ).FirstOrDefault();

            //var z = f.x.field1;

        }
    }
}
