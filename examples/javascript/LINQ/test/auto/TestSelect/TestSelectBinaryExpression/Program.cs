using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()
            //let yy = 6
            //let xx = "not used"

            //let netoPALK = x.field1

            //let brutoPALK = netoPALK * 1.8

            select (x.field1 + 7) / 55
            //select brutoPALK
            // ctrlS
        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
