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



     
     

        public override Type[] GetActiveTypes()
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


        public MethodBase ResolveMethod(MethodBase m)
        {
            return
                (m.DeclaringType.ToScriptAttribute() == null
                            ? ResolveImplementationMethod(m.DeclaringType, m)
                            : m);
        }

        public MethodBase ResolveMethod(Type t, MethodBase m)
        {
            var s = m.DeclaringType.ToScriptAttributeOrDefault();

            return
                (s == null
                            ? ResolveImplementationMethod(t, m)
                            : m);
        }

        public bool IsFullyQualifiedNamesRequired(Type context, Type subject)
        {
            if (context != subject && context.Name == subject.Name)
                return true;

            // there is a field with the same name as the type we would be importing
            if (context.GetField(subject.Name) != null)
                return true;

            return GetImportTypes(context).Count(i => i.Name == subject.Name) > 1;
        }
    }


}
