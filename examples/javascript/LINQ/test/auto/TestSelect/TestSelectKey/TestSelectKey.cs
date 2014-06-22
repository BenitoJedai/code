using ScriptCoreLib.Query.Experimental;

class TestSelectKey
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()
            //let yy = 6
            let xx = "not used"





            select x.Key

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
