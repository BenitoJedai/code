using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()
            where x.field1 >= 0
            group x by new { x.field2, x.field3 } into g
            select new { g.Key }

        ).FirstOrDefault();

    }
}
