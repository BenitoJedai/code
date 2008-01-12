using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsc.Languages.ActionScript
{
    partial class ActionScriptCompiler
    {


        public override ScriptCoreLib.ScriptType GetScriptType()
        {
            throw new NotImplementedException();
        }

        public override bool CompileType(Type e)
        {
            throw new NotImplementedException();
        }

        public override void WriteTypeSignature(Type z, ScriptCoreLib.ScriptAttribute za)
        {
            throw new NotImplementedException();
        }

        public override void WriteTypeFields(Type z, ScriptCoreLib.ScriptAttribute za)
        {
            throw new NotImplementedException();
        }

        public override void WriteTypeFieldModifier(System.Reflection.FieldInfo zfn)
        {
            throw new NotImplementedException();
        }

        public override void WriteTypeInstanceMethods(Type z, ScriptCoreLib.ScriptAttribute za)
        {
            throw new NotImplementedException();
        }

        public override void WriteMethodSignature(System.Reflection.MethodBase m, bool dStatic)
        {
            throw new NotImplementedException();
        }

        public override void WriteMethodCallVerified(ILBlock.Prestatement p, ILInstruction i, System.Reflection.MethodBase m)
        {
            throw new NotImplementedException();
        }

        public override void WriteMethodParameterList(System.Reflection.MethodBase m)
        {
            throw new NotImplementedException();
        }

        public override void WriteSelf()
        {
            throw new NotImplementedException();
        }

        public override void EmitPrestatement(ILBlock.Prestatement p)
        {
            throw new NotImplementedException();
        }

        public override Type[] GetActiveTypes()
        {
            throw new NotImplementedException();
        }

        public override System.Reflection.MethodBase ResolveImplementationMethod(Type t, System.Reflection.MethodBase m)
        {
            throw new NotImplementedException();
        }

        public override System.Reflection.MethodBase ResolveImplementationMethod(Type t, System.Reflection.MethodBase m, string alias)
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
