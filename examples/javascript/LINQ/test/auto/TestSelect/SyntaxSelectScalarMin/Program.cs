using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()


            let query = from xx in new xTable()
                    where xx.field1 == x.field2
                    select xx.field3

            let a = query.Min()

            select new { a }
        ).FirstOrDefault();

    }
}
