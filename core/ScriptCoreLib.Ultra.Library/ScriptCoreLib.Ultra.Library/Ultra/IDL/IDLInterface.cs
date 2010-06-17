using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.IDL
{
    public class IDLInterface
    {

        public IDLParserToken Keyword;
        public IDLParserToken Name;
        public IDLParserToken BaseType;

        public IDLParserToken Terminator;

        public readonly IDLParserTokenPair InterfaceBody = new IDLParserTokenPair();

        public readonly List<IDLMember> Members = new List<IDLMember>();

        public override string ToString()
        {
            return "interface " + this.Name.Text;
        }

        public string TargetNamespace;
    }
}
