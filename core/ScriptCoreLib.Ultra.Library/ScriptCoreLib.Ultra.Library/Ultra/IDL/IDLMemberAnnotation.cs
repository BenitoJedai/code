using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.IDL
{
    public class IDLMemberAnnotation : IDLMember
    {
        public IDLParserToken Keyword;

        public readonly List<IDLParameter> Parameters = new List<IDLParameter>();
        public readonly IDLParserTokenPair ParameterSymbols = new IDLParserTokenPair();



        public IDLParserToken Terminator
        {
            get
            {
                if (ParameterSymbols.Item2 != null)
                    return ParameterSymbols.Item2;

                return Keyword;
            }
        }
    }

    public class IDLMemberAnnotationArray 
    {
        public readonly IDLParserTokenPair Symbols = new IDLParserTokenPair();

        public readonly List<IDLMemberAnnotation> Items = new List<IDLMemberAnnotation>();
    } 
}
