using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            let gap1 = x.field1

                // did it work before?
            where x.field1 == 0


            select x.field3
        ).FirstOrDefault();

    }
}
