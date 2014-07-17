using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            let c = (
                 from z in new xTable()
                 where z.field1 == x.field1
                 where z.field2 == x.field2
                 select z
             )

            let cc = c.FirstOrDefault()

            select cc

        ).FirstOrDefault();

    }
}
