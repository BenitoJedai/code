using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()
            group x by 1 into gg
            select new
            {
                count1 = gg.Count(),

                gg.Key
            }

        ).FirstOrDefault();

    }
}
