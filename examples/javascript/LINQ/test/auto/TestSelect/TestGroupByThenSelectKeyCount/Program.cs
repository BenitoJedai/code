using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            group x by x.field1 into g


            select new { 
                g.Key,
                c = g.Count()
            }

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
