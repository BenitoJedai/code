using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ScriptCoreLib.Ultra.IDL
{
    public class IDLNumericLiteral
    {
        public IDLParserToken Marker;
        public IDLParserToken Terminator;

        public NumberStyles Style = NumberStyles.Integer;

        public int Int32Value;

        public override string ToString()
        {
            return "" +Int32Value;
        }
    }
}
