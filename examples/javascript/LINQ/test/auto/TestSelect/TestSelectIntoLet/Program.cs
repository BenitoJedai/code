using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()
            select new { XKey = x.Key, x.Tag } into xx

            let zoo1 = 1
            let zoo2 = 2
            let zoo3 = 3

            select new { xx.XKey, zoo1, zoo2, zoo3, zoo = new { zoo1, zoo2, zoo3 }, zooa = new[] { zoo1, zoo2, zoo3 } }

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
