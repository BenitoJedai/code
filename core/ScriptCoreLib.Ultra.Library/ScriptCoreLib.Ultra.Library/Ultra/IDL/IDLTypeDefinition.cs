using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.IDL
{
    public class IDLTypeDefinition
    {
        public IDLModule DeclaringModule;

        public IDLParserToken Keyword;
        public IDLTypeReference Type;
        public IDLParserToken Name;

        public IDLParserToken Terminator;

        public override string ToString()
        {
            return "typedef "  + this.Type.ToString() + " " + this.Name.Text + ";";
        }
    }
}
