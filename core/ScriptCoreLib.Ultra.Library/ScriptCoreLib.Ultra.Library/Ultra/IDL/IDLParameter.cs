using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.IDL
{
    public class IDLParameter
    {
        public IDLParserToken KeywordIn;
        public IDLParserToken KeywordOptional;
        public IDLTypeReference ParameterType;
        public IDLParserToken Name;

        public override string ToString()
        {
            return Name.Text;
        }
    }
}
