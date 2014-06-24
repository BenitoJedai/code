using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            group x by (
                from y in new xTable()
                where y.field1 == x.field2

                //select y
                select y.Tag
            ).FirstOrDefault() into g

            let y = g.Last()
            select new { g.Key, x = g.Last(), y }

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
