using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()


            let c = from xx in new xTable()
                        //where xx.field1 == x.field2
                    //    where xx.field1 == 7

                    //orderby xx.field3

                    //select xx.field3
                    select xx

            let a = c.Count()

            select new { a }
        ).FirstOrDefault();

    }
}
