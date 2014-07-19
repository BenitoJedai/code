using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = new xTable().OrderBy(x => x.field1).FirstOrDefault();


    }
}
