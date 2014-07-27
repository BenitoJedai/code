using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            group x by new { a = 1 } into g
            select new { g.Key }

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}


//System.Xml.XmlException: '.', hexadecimal value 0x00, is an invalid character.Line 1, position 1.
//   at System.Xml.XmlTextReaderImpl.Throw(Exception e)
//   at System.Xml.XmlTextReaderImpl.Throw(String res, String[] args)
//   at System.Xml.XmlTextReaderImpl.ThrowInvalidChar(Char[] data, Int32 length, Int32 invCharPos)
//   at System.Xml.XmlTextReaderImpl.ParseRootLevelWhitespace()
//   at System.Xml.XmlTextReaderImpl.ParseDocumentContent()
//   at System.Xml.XmlTextReaderImpl.Read()
//   at System.Xml.XmlReader.MoveToContent()
//   at System.Xml.Linq.XElement.Load(XmlReader reader, LoadOptions options)
//   at System.Xml.Linq.XElement.Parse(String text, LoadOptions options)
//   at System.Xml.Linq.XElement.Parse(String text)
//   at AutoRefreshTestingHost.Program.<>c__DisplayClass24.<Main>b__1c()
//   at System.Threading.ThreadHelper.ThreadStart_Context(Object state)
//   at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean preserveSyncCtx)
//   at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean preserveSyncCtx)
//   at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)
//   at System.Threading.ThreadHelper.ThreadStart()