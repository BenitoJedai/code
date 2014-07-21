using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140721

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
