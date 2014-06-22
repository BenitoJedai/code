using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()
            //let yy = 6
            let xx = "piiksuland!"





            select new { x, xx, z = new { x.field1, x } }
        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
