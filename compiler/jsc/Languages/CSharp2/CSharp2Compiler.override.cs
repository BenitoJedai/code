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
        public override ScriptType GetScriptType()
        {
            return ScriptType.CSharp2;
        }

        public override string GetDecoratedTypeName(Type z, bool bExternalAllowed)
        {
            return GetSafeLiteral(z.Name);
        }

        public override string GetDecoratedTypeNameWithinNestedName(Type z)
        {
            return GetSafeLiteral(z.Name);
        }

        public override bool SupportsBCLTypesAreNative
        {
            get
            {
                return true;
            }
        }
    }


}
