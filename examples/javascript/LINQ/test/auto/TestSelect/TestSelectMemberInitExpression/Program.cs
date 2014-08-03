using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()
            //let yy = 6
            let xx = "not used"


            // CustomXElement ?
            select new xElement(xx, xx)
            {
                field1 = x.field1
            }

        ).FirstOrDefault();

        //var z = f.x.field1;

    }

    class xElement
    {
        public xElement(string name, string content)
        {

        }

        public int field1;
    }
}
