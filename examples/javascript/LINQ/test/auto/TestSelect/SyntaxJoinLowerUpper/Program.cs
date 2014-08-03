using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        // need to test it!

        var f = (
            from x in new xTable()
            select new { x.Key, x.Tag } into uc

            join ucLower in (
                from x in new xTable()
                select new { x.Key, Tag = x.Tag.ToLower() }
            ) on uc.Key equals ucLower.Key

            join ucUpper in (
                from x in new xTable()
                select new { x.Key, Tag = x.Tag.ToUpper() }
            ) on uc.Key equals ucUpper.Key

            select new
            {
                uc.Key,
                uc.Tag,
                TagLower = ucLower.Tag,
                TagUpper = ucUpper.Tag
            }
        ).FirstOrDefault();

    }
}
