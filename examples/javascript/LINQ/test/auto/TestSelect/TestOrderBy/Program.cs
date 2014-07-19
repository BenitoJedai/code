using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            //let foo = x.field1

            orderby x.field1
            //orderby foo

            select new { x.field1 }

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
