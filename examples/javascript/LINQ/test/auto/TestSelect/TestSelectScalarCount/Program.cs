using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\Experimental\QueryExpressionBuilder.cs
        // 410


        var f = (
            from x in new xTable()

            //let y = 
            //    from z in new xTable()
            //    where z.field1 == x.field1
            //    select z

            //let c = y.Count()

            let c = (
                from z in new xTable()
                where z.field1 == x.field1
                select z
            ).Count()


            select c

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
