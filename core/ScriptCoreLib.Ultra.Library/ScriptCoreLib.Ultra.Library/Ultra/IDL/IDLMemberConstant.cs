using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.IDL
{
    public class IDLMemberConstant : IDLMember
    {
        public IDLParserToken Keyword;
        public IDLTypeReference Type;
        public IDLParserToken Name;
        public IDLParserToken KeywordAssignment;
        public IDLNumericLiteral Value;
        public IDLParserToken Terminator;

        public override string ToString()
        {
            return "const " + Name.Text + " = " + Value.Int32Value + ";";
        }
    }
}
