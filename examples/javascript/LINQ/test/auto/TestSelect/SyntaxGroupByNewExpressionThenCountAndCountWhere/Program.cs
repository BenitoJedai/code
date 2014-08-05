using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        // 20140607

        var f = (
            from x in new xTable()

            group x by new { x.field2, x.field3 } into g
            select new
            {
                g.Key,

                c = g.Count(),

                // /* 2969:0019 */    /* group */, count(*) as scalarSpecialCount
                // need to rewrite it into a scalar select or join?
                scalarSpecialCount = g.Where(x => x.field1 > 1).Count()
            }

        ).FirstOrDefault();


    }
}
