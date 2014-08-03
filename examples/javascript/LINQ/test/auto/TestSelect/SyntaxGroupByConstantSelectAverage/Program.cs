using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

                //                /* 3163:0001 */
                //            select
                ///* 0898:0002 */   ?

            group x by 1 into gg
            select new
            {
                average1 = gg.Average(x => x.field1)
            }

        ).FirstOrDefault();

    }
}
