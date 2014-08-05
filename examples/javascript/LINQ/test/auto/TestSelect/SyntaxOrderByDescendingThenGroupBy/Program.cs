using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
               from x in new xTable()

               orderby x.field1 descending

               group x by 1 into gg

               select new
               {
                   gg.Last().Tag
               }

           ).FirstOrDefault();

    }
}
