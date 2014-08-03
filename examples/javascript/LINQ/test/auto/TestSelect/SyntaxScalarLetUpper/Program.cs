using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
           from x in new xTable()

           let c = (
                from z in new xTable()
                    //where z.field1 == x.field1
                select z.Tag
            )


           //   let <>h__TransparentIdentifier1 cc <- upper(FirstOrDefault())

           let cc = c.FirstOrDefault()

           // not correctly proxied?
           let ccu = cc.ToUpper()
           let ccl = cc.ToLower()

           select new { ccu, ccl }

       ).FirstOrDefault();

    }
}
