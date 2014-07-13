using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()



            let scalar1 = x.field3

            // could we take average of multple fields?
            select scalar1
        ).Average();

    }
}
