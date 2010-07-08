using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.IDL;

namespace ScriptCoreLib.Ultra.IL
{
    public class ILAssemblyExtern
    {
        public IDLParserToken Token;

        public IDLParserToken Name;

        public IDLParserTokenPair Scope;

        public IDLParserToken PublicKeyToken;
        public IDLParserToken Version;

        public override string ToString()
        {
            return new { Name, PublicKeyToken, Version }.ToString();
        }
    }
}
