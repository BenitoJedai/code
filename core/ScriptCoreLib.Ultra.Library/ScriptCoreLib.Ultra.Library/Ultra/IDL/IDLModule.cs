using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.IDL
{
    public class IDLModule
    {
        public string Name;

        public readonly List<IDLTypeDefinition> TypeDefinitions = new List<IDLTypeDefinition>();
        public readonly List<IDLInterface> Interfaces = new List<IDLInterface>();

        public static implicit operator IDLModule(string source)
        {
            return IDLParser.ToModule(source);
        }
    }
}
