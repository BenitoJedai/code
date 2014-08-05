using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
               from x in new xTable()

               orderby x.field1 ascending

               group x by 1 into gg

               select new
               {
                   gg.Last().Tag
               }

           ).FirstOrDefault();

    }
}
