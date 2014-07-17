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

                 let u = (
                        from uz in new xTable()



                        select uz
                    )

                 let uu = u.FirstOrDefault()


                 select uu
             )

            let cc = c.FirstOrDefault()

            select cc

        ).FirstOrDefault();

    }
}
