using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = new xTable().Where(x => x.field1 == 1).FirstOrDefault();


        //var f = (
        //    from x in new xTable()

        //    // did it work before?
        //    where x.field1 == 0


        //    select x.field3
        //).FirstOrDefault();

    }
}
