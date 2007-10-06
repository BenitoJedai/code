using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib;

namespace ScriptCoreLib.JavaScript.Runtime
{
    
    [Script, System.Obsolete("BCL", false)]
    internal class StringWriter
    {
        public readonly IArray<string> Buffer = new IArray<string>();

        public string NewLineString = "\r\n";

        public void Write(StringWriter e)
        {
            Buffer.push(e.GetString());
        }

        public void Write()
        {
            Write("");
        }

        public void Write(object e)
        {
            var len = Buffer.length;
            if (len > 0)
                Buffer[len - 1] += e;
            else
                Buffer.push(e + "");
        }

        public void WriteLine()
        {
            Buffer.push(NewLineString);
        }

        public void WriteLine(string e)
        {
            Write(e);
            WriteLine();
        }

        public void Prefix(string p, int i)
        {
            Prefix(p, i, Buffer.length - 1);
        }

        public void Prefix(string e, int from, int to)
        {
            for (int i = from; i <= to; i++)
            {
                if (Buffer[i] != this.NewLineString)
                    Buffer[i] = e + Buffer[i];
            }
        }

        public string GetString()
        {
            return Buffer.join("");
        }

        public string GetString(string p)
        {
            return Buffer.join(p);
        }

        public void Clear()
        {
            Buffer.splice(0, Buffer.length);
        }
    }

}
