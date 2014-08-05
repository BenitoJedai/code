using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from u0 in new xTable()
            join u1 in new xTable() on u0.field1 equals u1.field2
            join u2 in new xTable() on u1.field1 equals u2.field2
            join u3 in new xTable() on u2.field1 equals u3.field2
            join u4 in new xTable() on u3.field1 equals u4.field2
            join u5 in new xTable() on u4.field1 equals u5.field2
            select new { u0, u1, u2, u3, u4, u5 }

        ).FirstOrDefault();

    }
}
