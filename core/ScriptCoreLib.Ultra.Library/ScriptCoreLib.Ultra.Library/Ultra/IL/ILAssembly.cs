using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.IDL;

namespace ScriptCoreLib.Ultra.IL
{
    public class ILAssembly
    {
        public readonly List<ILAssemblyExtern> AssemblyExternList = new List<ILAssemblyExtern>();

        public readonly List<ILAssemblyMethod> Methods = new List<ILAssemblyMethod>();
    }

    public class ILAssemblyMethod
    {
        public IDLParserToken Token;

        public bool IsStatic;

        // X:\jsc.svn\examples\c\Test\TestConsoleWriteLine\TestConsoleWriteLine\Program.cs
        public bool IsUnmanagedExport;

        public IDLParserToken NameToken;
        public IDLParserToken ParameterStartToken;
        public IDLParserToken BodyStartToken;
    }
}
