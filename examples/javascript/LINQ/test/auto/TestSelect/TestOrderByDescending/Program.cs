using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            orderby x.field1 descending, x.field2, x.field1 + x.field2 descending

            let gap1 = 1

            select new { x.field1 }

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
