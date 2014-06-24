using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            group x by 1 into g
            select new { g.Key }

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
