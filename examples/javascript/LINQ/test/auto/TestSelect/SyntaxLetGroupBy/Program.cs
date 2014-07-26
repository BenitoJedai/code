using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            let gap1 = 1


            // xNewExpression = {new <>f__AnonymousType0`2(x = x, gap1 = 1)}
            group x by 1 into g

            //let gap2 = 2

            select new
            {
                g.Last().Tag,
                g.Last().field3
            }

        ).FirstOrDefault();


    }
}
