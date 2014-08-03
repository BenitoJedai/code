using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            //  where Contains()
            where x.Tag.ToLower().Contains("xx")

            select x.field3
        ).Average();

    }
}
