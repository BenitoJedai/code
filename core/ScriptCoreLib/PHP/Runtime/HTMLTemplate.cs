using ScriptCoreLib;
using System;

namespace ScriptCoreLib.PHP.Runtime
{
    [Script, Obsolete]
    internal class HTMLTemplate
    {
        public string Text;

        public HTMLTemplate(string filename)
        {
            Text = Native.API.file_get_contents(filename);
        }

        public string this[string e]
        {
            set
            {
                Text = Text.Replace("<!-- " + e + " -->", value);
            }
        }



        public void Write()
        {
            Native.echo(Text);
        }

        public void Spawn(string p)
        {
            this["{" + p + "}"] = "<div class='" + p + "' />";

        }
    }
}
