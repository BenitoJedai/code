using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.IDL
{
    public class IDLMemberMethod : IDLMember
    {
        public IDLParserToken KeywordGetter;
        public IDLParserToken KeywordSetter;
        public IDLTypeReference Type;
        public IDLParserToken Name;

        public readonly IDLParserTokenPair ParameterSymbols = new IDLParserTokenPair();
        public readonly List<IDLParameter> Parameters = new List<IDLParameter>();

        public IDLParserToken KeywordRaises;


        public readonly IDLParserTokenPair RaisesSymbols = new IDLParserTokenPair();
        public IDLTypeReference RaisesType;


        public IDLParserToken Terminator;


        public override string ToString()
        {
            return "... " + Name.Text + "(...);";
        }
    }
}
