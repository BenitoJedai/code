using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.IDL
{
    public class IDLMemberConstructor : IDLMember
    {
        public IDLParserToken Keyword;

        public readonly List<IDLParameter> Parameters = new List<IDLParameter>();
        public readonly IDLParserTokenPair ParameterSymbols = new IDLParserTokenPair();
    }
}
