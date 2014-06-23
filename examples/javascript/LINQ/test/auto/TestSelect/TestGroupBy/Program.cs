using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        //new xTable().GroupBy(x => x.field1).FirstOrDefault().Last().field1;
        //var z =new xTable().GroupBy(x => x.field1, x => new { x }).FirstOrDefault().Last().field1;



        var f = (
            from x in new xTable()

            let xFoo = x.field1

            let xKey = new { x.field1, x.field2 }

            group new { x, xFoo, xKey } by new
            {
                xKey,
                xFoo,
                g3 = x.field2 + 2
            } into g

            let groupExtension = 4
            let gLast = g.Last()

            select new
            {
                SelectedKey = g.Key,

                // is this causing an issue?
                SelectedKey_xFoo = g.Key.xFoo,

                // public static TElement Last<TKey, TElement>(this QueryExpressionBuilder.IQueryStrategyGrouping<TKey, TElement> source);
                LastKey = g.Last().x.field1,

                LastGroup = new { g.Last().x.field1, gLast.x.field2 }
            }

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
