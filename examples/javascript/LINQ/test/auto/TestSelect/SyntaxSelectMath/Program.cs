using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            let add = x.field1 + x.field2
            let mul = add / 3

            where mul > 0

            select new
            {
                mul
            }
        ).FirstOrDefault();

    }
}
