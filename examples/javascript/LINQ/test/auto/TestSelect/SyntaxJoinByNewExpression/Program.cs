using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
           from x in new xTable()
           join y in new xTable() on new { x.field1, x.field2 } equals new { y.field1, y.field2 }
           select new
           {
               x.field1,
               y.field2,
            }

       ).FirstOrDefault();

    }
}
