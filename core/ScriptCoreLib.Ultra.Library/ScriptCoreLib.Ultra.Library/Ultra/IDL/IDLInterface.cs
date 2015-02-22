using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.IDL
{
    public class IDLInterface
    {
        // 20150221 
        // lets mark THREE as static to make use of the C# using static type feature.
        public IDLParserToken KeywordStatic;

        public IDLParserToken Keyword;
        public IDLParserToken Name;


        public readonly List<IDLTypeReference> GenericDefinitionParameters = new List<IDLTypeReference>();
        public readonly IDLParserTokenPair GenericDefinitionParameterSymbols = new IDLParserTokenPair();



        public IDLParserToken BaseType;

        public IDLParserToken Terminator;

        public readonly IDLParserTokenPair InterfaceBody = new IDLParserTokenPair();

        public readonly List<IDLMember> Members = new List<IDLMember>();

        public IEnumerable<IDLMemberMethod> GetMethods()
        {
            return from k in Members
                   let m = k as IDLMemberMethod
                   where m != null
                   select m;
        }

        public IEnumerable<IDLMemberAnnotation> GetConstructors()
        {
            return from k in Members
                   let m = k as IDLMemberAnnotation
                   where m != null
                   where m.Keyword.Text == "Constructor"
                   select m;
        }

        public override string ToString()
        {
            return "interface " + this.Name.Text;
        }

        public string TargetNamespace;

        public string ImplementationFile;
    }
}
