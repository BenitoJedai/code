using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            group x by new[] { x.field1, x.field2, x.field1 + x.field2 } into g

            select g.Key

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
