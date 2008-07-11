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
    partial class CSharp2Compiler : Script.CompilerCLike
    {
        public static string FileExtension = "cs";

        



        public override void WriteTypeSignature(Type z, ScriptAttribute za)
        {
            throw new NotImplementedException();
        }

        public override void WriteTypeFields(Type z, ScriptAttribute za)
        {
            throw new NotImplementedException();
        }

        public override void WriteTypeFieldModifier(FieldInfo zfn)
        {
            throw new NotImplementedException();
        }

        public override void WriteMethodSignature(MethodBase m, bool dStatic)
        {
            throw new NotImplementedException();
        }

        public override void WriteMethodCallVerified(ILBlock.Prestatement p, ILInstruction i, MethodBase m)
        {
            throw new NotImplementedException();
        }

        public override void WriteMethodParameterList(MethodBase m)
        {
            throw new NotImplementedException();
        }

        public override void WriteSelf()
        {
            throw new NotImplementedException();
        }

        public override Type[] GetActiveTypes()
        {
            throw new NotImplementedException();
        }

        public override MethodBase ResolveImplementationMethod(Type t, MethodBase m)
        {
            throw new NotImplementedException();
        }

        public override MethodBase ResolveImplementationMethod(Type t, MethodBase m, string alias)
        {
            throw new NotImplementedException();
        }

        public override void WriteTypeConstructionVerified()
        {
            throw new NotImplementedException();
        }

        public override bool EmitTryBlock(ILBlock.Prestatement p)
        {
            throw new NotImplementedException();
        }
    }


}
