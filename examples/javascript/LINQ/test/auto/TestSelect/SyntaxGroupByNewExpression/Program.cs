﻿using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\SyntaxJoinThenGroupBy\Program.cs

        var f = (
            from x in new xTable()

            group x by new { x.field2, x.field3 } into g
            select new { g.Key }

        ).FirstOrDefault();

    }
}
