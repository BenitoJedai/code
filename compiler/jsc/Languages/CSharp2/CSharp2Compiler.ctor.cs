using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using ScriptCoreLib;
using System.Reflection.Emit;
using jsc.Script;
using System.Runtime.InteropServices;

namespace jsc.Languages.CSharp2
{
    partial class CSharp2Compiler
    {
        public readonly AssamblyTypeInfo MySession;

        public CSharp2Compiler(TextWriter xw, AssamblyTypeInfo xs)
            : base(xw)
        {
            MySession = xs;

            CreateInstructionHandlers();
        }
    }


}
