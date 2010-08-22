using ScriptCoreLib.Shared;
using System;

namespace ScriptCoreLib.Shared
{


    [Script, Obsolete]
    internal class TextWriter //: ITextWriter
    {
        string _text = "";

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public void Write(string e)
        {
            _text += e;
        }

        public void WriteLine()
        {
            WriteLine("");
        }

        public void WriteLine(string e)
        {
            Write( e + "\n");
        }


    }

}