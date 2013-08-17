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

        public IDLParserToken NullableSymbol;

        public readonly IDLParserTokenPair ArraySymbols = new IDLParserTokenPair();

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is IDLTypeReference)
            {
                var x = obj as IDLTypeReference;

                return x.ToString() == this.ToString();
            }

            return false;
        }

        public override string ToString()
        {
            var w = new StringBuilder();

            if (Namespace != null)
            {
                w.Append(Namespace.Text);

                if (Operator == null)
                {
                    w.Append(" ");
                }
                else
                {
                    if (Operator.Text == "::")
                    {
                        w.Append("::");
                    }
                    else
                    {
                        w.Append(" ");
                    }
                }
            }

            w.Append(Name.Text);

            if (GenericParameters.Count > 0)
            {
                w.Append("<");

                GenericParameters.Select(k => k.Name.Text).SelectWithSeparator(",").WithEach(k => w.Append(k));

                w.Append(">");
            }

            if (this.ArraySymbols.Item2 != null)
                w.Append("[]");

            return w.ToString();
        }

        static public IDLTypeReference OfName(string Name)
        {
            return new IDLTypeReference { Name = ((IDLParserToken)Name).Next };
        }

        public IDLParserToken Terminator
        {
            get
            {
                if (NullableSymbol != null)
                    return NullableSymbol.Next;

                if (GenericParameterSymbols.Item2 != null)
                    return GenericParameterSymbols.Item2;

                if (ArraySymbols.Item2 != null)
                    return ArraySymbols.Item2;



                return Name;
            }
        }
    }
}
