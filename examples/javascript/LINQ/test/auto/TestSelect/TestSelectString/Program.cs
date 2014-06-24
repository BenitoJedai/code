using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            select (x.Tag  + "a").ToUpper()
            //string.Concat(
            ////new[]
            ////{
            //    55,
            //    x.Tag.ToUpper(),
            //    " + ",
            //    x.Tag.ToLower()
            ////}
            //)

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
