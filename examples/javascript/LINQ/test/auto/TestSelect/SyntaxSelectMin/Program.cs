using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            select x.field3
        ).Min();


        // Error	5	'ScriptCoreLib.Query.Experimental.IQueryStrategy<long>' does not contain a definition for 'Min' and no extension method 'Min' accepting a first argument of type 'ScriptCoreLib.Query.Experimental.IQueryStrategy<long>' could be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectMin\Program.cs	11	11	SyntaxSelectMin
        // well, add it... 
    }
}
