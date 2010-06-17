using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.IDL
{
    public class IDLTypeReference
    {
        public IDLParserToken Namespace;
        public IDLParserToken Operator;
        public IDLParserToken Name;

        public readonly List<IDLTypeReference> GenericParameters = new List<IDLTypeReference>();
        public readonly IDLParserTokenPair GenericParameterSymbols = new IDLParserTokenPair();

        public override string ToString()
        {
            var w = new StringBuilder();

            if (Namespace != null)
            {
                w.Append(Namespace.Text);

                if (Operator.Text == "::")
                {
                    w.Append("::");
                }
                else
                {
                    w.Append(" ");
                }
            }

            w.Append(Name.Text);

            if (GenericParameters.Count > 0)
            {
                w.Append("<");

                GenericParameters.Select(k => k.Name.Text).SelectWithSeparator(",").WithEach(k => w.Append(k));

                w.Append(">");
            }

            return w.ToString();
        }

        
        public IDLParserToken Terminator
        {
            get
            {
                if (GenericParameterSymbols.Item2 != null)
                    return GenericParameterSymbols.Item2;

                return Name;
            }
        }
    }
}
