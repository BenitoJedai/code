﻿using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()
            let gap1 = 1

            orderby x.field1

            select x.field3
        ).FirstOrDefault();

    }
}
