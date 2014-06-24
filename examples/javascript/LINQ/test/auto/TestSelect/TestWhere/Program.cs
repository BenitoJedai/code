using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var loc1 = 5;
        var loc0 = new { loc1 };
        var loc2 = new { loc0 };

        var f = (
            from x in new xTable()

            //where x.field1 == 5
            where x.field1 == loc2.loc0.loc1
            //where x.field2 > 5 || x.field2 < 13

            let z = 7

            select x

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
