using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()
            where x.field1 == 4
            where x.field2 == 8

            orderby x.field1

            select x.field3
        ).Average();

    }
}
