using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        // 20140610

        var f = (
          from x in new xTable()
          where x.field1 >= 0
          where x.field1 < 55
          group x by new { x.field2, x.field3 } into g
          select new { g.Key }

      ).FirstOrDefault();

    }
}
