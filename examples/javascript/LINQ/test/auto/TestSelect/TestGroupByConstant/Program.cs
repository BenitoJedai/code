﻿using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            group x by 1 into g

            //let gap2 = 2

            select 
            //new
            //{
                //g.Key,

                // /* 2161:0003 */   /* let */, .`Last.Tag` as `Tag`
                g.Last().Tag
            //}

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
