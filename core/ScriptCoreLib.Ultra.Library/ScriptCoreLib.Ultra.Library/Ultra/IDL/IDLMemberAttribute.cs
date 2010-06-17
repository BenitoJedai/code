using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.IDL
{
    public class IDLMemberAttribute : IDLMember
    {
        public IDLParserToken KeywordReadOnly;

        public IDLParserToken Keyword;

        public IDLTypeReference Type;
        public IDLParserToken Name;

        public IDLParserToken Terminator;

        public override string ToString()
        {
            return "attribute ... " + Name.Text + ";";
        }
    }
}
