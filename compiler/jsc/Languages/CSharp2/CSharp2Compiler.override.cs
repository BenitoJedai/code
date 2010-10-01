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


        public override string GetTypeNameForFilename(Type z)
        {

            return z.DeclaringTypesToStack(true).Aggregate("",
                (v, p) =>
                {
                    if (!string.IsNullOrEmpty(v))
                        v += ".";

                    return v + GetSafeLiteral(p.Name);
                }
            ); ;
        }

        public override bool SupportsInlineThisReference
        {
            get
            {
                return true;
            }
        }

        public override bool SupportsAbstractMethods
        {
            get
            {
                return true;
            }
        }

        public override bool SupportsBCLTypesAreNative
        {
            get
            {
                return true;
            }
        }

        public override bool SupportsInlineAssigments
        {
            get
            {
                // fixme: seems like OpCodes.ret does not honor if this is set to false
                return true;
            }
        }

		public override Type ResolveImplementation(Type t)
		{
			return MySession.ResolveImplementation(t);
		}

        public override System.Reflection.MethodBase ResolveImplementationMethod(Type t, System.Reflection.MethodBase m)
        {
            return MySession.ResolveImplementation(t, m);
        }

        public override System.Reflection.MethodBase ResolveImplementationMethod(Type t, System.Reflection.MethodBase m, string alias)
        {
            return MySession.ResolveMethod(m, t, alias);
        }

        public override void WriteSelf()
        {
            Write("this");
        }


        public override void WriteLocalVariableDefinition(LocalVariableInfo v, MethodBase u)
        {
            WriteIndent();

            WriteKeywordSpace(Keywords._var);
            WriteVariableName(u.DeclaringType, u, v);
            WriteAssignment();
            WriteKeyword(Keywords._default);
            Write("(");
            WriteGenericTypeName(u.DeclaringType, v.LocalType);
            Write(")");

            WriteLine(";");
        }

        public override MethodInfo[] GetAllInstanceMethods(Type z)
        {
            if (z == null)
                return null;

            return Enumerable.ToArray(
                from m in base.GetAllInstanceMethods(z)
                let p = new PropertyDetector(m)
                where p.GetProperty == null && p.SetProperty == null
                select m
            );
        }

        protected override bool IsTypeCastRequired(System.Type e, ILFlowStackItem s)
        {
            if (e.IsEnum)
            {
                //if (s.SingleStackInstruction.TargetParameter != null && s.SingleStackInstruction.TargetParameter.ParameterType.Equals(e))
                //    return true;

                // int to enum
                if (s.SingleStackInstruction.TargetParameter != null && s.SingleStackInstruction.TargetParameter.ParameterType.Equals(typeof(int)))
                    return true;

                if (s.SingleStackInstruction.TargetInteger != null)
                    return true;
            }

            if (e.Equals(typeof(int)))
            {
                if (s.SingleStackInstruction.TargetMethod != null &&
                    !s.SingleStackInstruction.TargetMethod.ReturnParameter.Equals(e))
                    return true;

                if (s.SingleStackInstruction.TargetField != null)
                    return !s.SingleStackInstruction.TargetField.FieldType.Equals(e);

                if (s.SingleStackInstruction.TargetParameter != null)
                    return !s.SingleStackInstruction.TargetParameter.ParameterType.Equals(e);

                if (s.SingleStackInstruction.TargetVariable != null)
                    return !s.SingleStackInstruction.TargetVariable.LocalType.Equals(e);
            }

            if (e.Equals(typeof(uint)))
            {
                // signed to unsigned
                if (s.SingleStackInstruction.TargetInteger != null)
                    return true;

                if (s.SingleStackInstruction.TargetVariable != null && s.SingleStackInstruction.TargetVariable.LocalType.Equals(typeof(int)))
                    return true;
            }

            return base.IsTypeCastRequired(e, s);
        }


        public override Predicate<ILBlock.Prestatement> MethodBodyFilter
        {
            get
            {
                return
                 delegate(ILBlock.Prestatement p)
                 {
                     // note that instance constructor returns pointer to instance

                     #region remove redundant returns
                     if (p.Instruction != null)
                         if (p.Instruction == OpCodes.Ret)
                             if (p.Instruction.Next == null)
                                 if (p.Instruction.StackBeforeStrict.Length == 0)
                                 {
                                     return true;
                                 }
                     #endregion

                     // base calls should be part of signature
                     if (p.IsBaseConstructorCall())
                         return true;

                     return false;
                 };
            }
        }

        public override bool WriteTypeInstanceMethodsFilter(MethodInfo m)
        {
			if (PropertyDetector.IsProperty(m))
				return true;

            return EventDetector.IsEvent(m);
        }


    }


}
