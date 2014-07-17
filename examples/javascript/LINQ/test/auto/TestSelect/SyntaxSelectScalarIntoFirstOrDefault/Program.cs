using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectInto\Program.cs

        var f = (
            from x in new xTable()

            let c = (
                 from z in new xTable()
                     //where z.field1 == x.field1

                 select new { z.Key, z.Tag } into xx

                 select xx.Key
             )


            // um. data layer would need to ask each element of the row right?
            let cc = c.FirstOrDefault()

            select cc

        ).FirstOrDefault();
    }
}
