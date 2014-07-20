using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            let gap1 = 1
            let gap2 = 2
            let gap3 = 3


            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectLet3Min\Program.cs
            select new { x.field3, x.field1, gap1, gap2, gap3 }
        ).FirstOrDefault();

    }
}
