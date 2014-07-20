using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            let gap1 = 1
            let gap2 = 2
            let gap3 = 3

            group x by 1 into g


            select new
            {
                g.Last().Tag,
                g.Last().field3
            }

        ).FirstOrDefault();


    }
}
