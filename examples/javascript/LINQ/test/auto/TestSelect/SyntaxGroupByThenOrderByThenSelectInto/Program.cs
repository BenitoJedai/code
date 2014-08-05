using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()
            group x by x.field1 into gg
            orderby gg.Key descending

            select new
            {
                gg.Last().Tag
            } into z


            select z
        ).FirstOrDefault();

    }
}
