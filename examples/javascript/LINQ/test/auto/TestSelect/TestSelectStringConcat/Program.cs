using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            select x.Tag + "?"

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
