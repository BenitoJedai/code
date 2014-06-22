using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()
            //let yy = 6
            let xx = "not used"



            select new xRow
            {
                field1 = x.field1
            }

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
