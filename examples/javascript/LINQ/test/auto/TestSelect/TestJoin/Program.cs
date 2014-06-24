using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            join y in new xTable() on x.field1 equals y.field2
            //select x.Key
            select new { x.field1, y.field2, z = new { x.field1, y.field2 } }

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
