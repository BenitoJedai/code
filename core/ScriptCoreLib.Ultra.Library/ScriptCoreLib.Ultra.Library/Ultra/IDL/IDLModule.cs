using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.IDL
{
    public class IDLModule
    {
        public IDLParserToken Keyword;
        public IDLParserToken Name;

        public readonly IDLParserTokenPair ModuleBody = new IDLParserTokenPair();

        public readonly List<IDLTypeDefinition> TypeDefinitions = new List<IDLTypeDefinition>();
        public readonly List<IDLInterface> Interfaces = new List<IDLInterface>();
        public readonly List<IDLModule> NestedModules = new List<IDLModule>();

        public static implicit operator IDLModule(string source)
        {
            return IDLParser.ToModule(source);
        }

        public override string ToString()
        {
            if (Name == null)
                return "global";

            return Name.Text;
        }

    }
}
