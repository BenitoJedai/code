using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        // 20140605
        var f = (
            from u0 in new xTable()
            join u1 in new xTable() on u0.field1 equals u1.field2
            join u2 in new xTable() on u0.field1 equals u2.field2
            group new { u0, u1, u2 } by u0.field1 into gg
            select new { gg.Key }
        ).FirstOrDefault();

    }
}
