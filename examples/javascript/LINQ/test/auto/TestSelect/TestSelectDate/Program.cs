﻿using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            let gap1 = 1

            let z = x.Timestamp.Date

            select new { z }

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
