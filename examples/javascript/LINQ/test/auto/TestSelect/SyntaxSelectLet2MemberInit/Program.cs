﻿using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            let gap1 = 1
            let gap2 = 2

            select new { x.field3, x.field1 }
        ).FirstOrDefault();

    }
}
