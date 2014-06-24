using ScriptCoreLib.Query.Experimental;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            select new XElement("hello",
                new XAttribute("foo", "bar"),

                x.Tag,
                " + ",
                x.Tag
                )

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
