using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from xouter in new xTable()

                // /* 1708:0007 */    /* proxy */, `<>h__TransparentIdentifier0`.`xouter.field1` as /* <>h__TransparentIdentifier1 */ `<>h__TransparentIdentifier0.xouter.field1`
                //  where (`z`.`field1` = `xouter`.`field1`)

            let y =
                from z in new xTable()

                    // xouter is available via h__TransparentIdentifier0 
                where z.field1 == xouter.field1
                select z.Key

            let c = y.Count()

            select new { c }
        ).FirstOrDefault();

    }
}
