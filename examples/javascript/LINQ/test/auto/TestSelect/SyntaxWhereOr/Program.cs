using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
                from x in new xTable()

                where x.field1 == 1 || x.field2 == 2

                select x.field3
            ).FirstOrDefault();
    }
}
