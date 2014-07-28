using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            //let xx = 1
            group x by x.field1 into gg

            select gg.Last()

        ).FirstOrDefault();

    }
}
