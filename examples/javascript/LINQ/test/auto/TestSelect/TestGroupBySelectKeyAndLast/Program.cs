using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            group x by x.field1 into g

            let gKey = g.Key
            select new {
                
                gKey, 
                
                //last = g.Last(), 
                lastTag = g.Last().Tag
            }

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
