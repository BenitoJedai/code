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

        // whatif its double?
        public int Int32Value;
        public double DoubleValue;

        public override string ToString()
        {
            return "" +Int32Value;
        }
    }
}
