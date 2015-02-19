using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        // http://damieng.com/blog/2014/09/04/optimizing-sum-count-min-max-and-average-with-linq

        var f = (
            from x in new xTable()


            let c = from xx in new xTable()
                    where xx.field1 == x.field2
                    select xx.field3


            // GLSL matrix sum?
            let a = c.Sum()

            select new { a }
        ).FirstOrDefault();

    }
}
