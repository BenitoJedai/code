using ScriptCoreLib.Query.Experimental;
using System.Collections.Specialized;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            select new StringDictionary 
            { 
                {"hello", x.Tag}
            }


        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
