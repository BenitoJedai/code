using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            let z = x.field1 * x.field2

            orderby z

            select new { x.field1, z }

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
