using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            orderby x.field1

            select new { x.field1 }

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
