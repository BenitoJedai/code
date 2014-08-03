using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

                //  where Contains()
                // not supported yet. 
            where string.IsNullOrEmpty(x.Tag)

            select x.field3
        ).Average();

    }
}
