using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()
            select new { x.Key, x.Tag } into xx
            let zoo = 1
            select new { xx.Key, zoo }

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
