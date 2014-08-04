using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
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
