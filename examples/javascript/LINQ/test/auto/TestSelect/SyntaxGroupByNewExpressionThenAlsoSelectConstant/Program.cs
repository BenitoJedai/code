using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        //20140610

        var LoginCount = 33;

        // * 2278:0004 */   /* let */, .`LoginCount` as `LoginCount`

        var f = (
            from x in new xTable()

            group x by new { x.field2, x.field3 } into g
            select new { g.Key, LoginCount }

        ).FirstOrDefault();

    }
}
