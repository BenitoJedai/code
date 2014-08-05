using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()
                // xlsx generated data table
                // needs to do order by
                // otherwise XSQLite and XMysql will disagree on order
            group x by x.field1 into gg

            select new
            {
                gg.Last().Tag
            }
        ).FirstOrDefault();

    }
}
