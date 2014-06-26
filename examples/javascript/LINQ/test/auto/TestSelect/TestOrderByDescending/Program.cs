using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            let xBinary = x.field1 + x.field2 

            orderby x.field1 descending, x.field2, xBinary descending

            let gap1 = 1

            select new { x.field1 }

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
