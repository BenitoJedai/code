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


            // `<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.x.field3`
            select x.field3

            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxSelectLet3MemberInit\Program.cs
        ).Min();

    }
}
