using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.IDL
{
    public class IDLMemberAttribute : IDLMember
    {
        public IDLParserToken KeywordEvent;
        public IDLParserToken KeywordStatic;
        public IDLParserToken KeywordReadOnly;

        public IDLParserToken Keyword;

        public IDLTypeReference Type;
        public IDLParserToken Name;

        public IDLParserToken Terminator;


        public IDLMemberAnnotationArray Annotations;

        public override string ToString()
        {
            return "attribute ... " + Name.Text + ";";
        }
    }
}
