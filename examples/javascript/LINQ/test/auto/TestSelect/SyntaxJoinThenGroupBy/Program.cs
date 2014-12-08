using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201412/20141208
        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxGroupByNewExpression\Program.cs

        var f = (
            from x in new xTable()
            join y in new xTable() on x.field1 equals y.field2
            group new { x, y } by x.field1 into gg
            select new { gg.Key }
        ).FirstOrDefault();

    }
}
