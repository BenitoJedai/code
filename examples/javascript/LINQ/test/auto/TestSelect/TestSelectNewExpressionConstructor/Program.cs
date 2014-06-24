using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            select new xElement("hello", x.Tag) //{ field1 = "1" }

        ).FirstOrDefault();

        //var z = f.x.field1;

    }

    class xElement
    {
        public xElement(string name, string content)
        {

        }

        //public string field1;
    }
}
