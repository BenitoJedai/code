using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

namespace jsc.Languages.ActionScript
{
    partial class ActionScriptCompiler
    {

        public  class TypeInstanceConstructorInfo
        {
            public ILBlock code;
            public ConstructorInfo ctor;
            public ConstructorInfo target;
            public ILFlow.StackItem[] args;
        }



        protected override void WriteTypeInstanceConstructors(Type z)
        {
            WriteTypeInstanceConstructorsAndGetPrimary(z);
        }

        public class ConstructorMergeInfo
        {
            public ConstructorInfo Primary;

            public ILFlow.StackItem[] Values;

            public Action CustomVariableInitialization;
        }

		public void WriteTypeFieldInitialization(Type z)
		{
			foreach (var CandidateField in z.GetFields(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (CandidateField.FieldType == typeof(double))
				{
					// we need to initialize Number fields for actionscript
					WriteIdent();
					WriteKeyword(Keywords._this);
					Write(".");
					WriteSafeLiteral(CandidateField.Name);
					WriteAssignment();
					Write("0");
					Write(";");
					WriteLine();
				}
			}
		}

        protected ConstructorMergeInfo WriteTypeInstanceConstructorsAndGetPrimary(Type z)
        {
            var r = new ConstructorMergeInfo();

            var zci = GetAllInstanceConstructors(z);

		

            if (zci.Length > 1)
            {
                // visual basic can have optional parameters on its own, its c# that needs some help
                // as3 does not support method overloading but does support default parameters
                // we need to figure out which ctor is real and which are just sattelites

                // default values should be extended to allow instance values
                // a workaround could be:
                // if (param == null) { param = UIComponent; } 

                

                var query =
                    from c in zci
                    let code = new ILBlock(c)
                    let b = code.Prestatements.PrestatementCommands.Where(
						p => p.Instruction != null && !p.Instruction.IsAnyOpCodeOf(OpCodes.Initobj, OpCodes.Ret, OpCodes.Nop)
					).ToArray()
                    where (b.Length == 1 && b[0].Instruction == OpCodes.Call)
                    let i = b[0].Instruction
                    let t = i.TargetConstructor
                    where t != null && zci.Contains(t)
                    // skip the ldarg0/this op
                    select new TypeInstanceConstructorInfo { code = code, ctor = c, /*b,*/ target = t, args = i.StackBeforeStrict.Skip(1).ToArray() };

                var cache = query.ToArray();
                var targets = zci.Except(cache.Select(i => i.ctor)).ToArray();


                if (targets.Length != 1)
                    Break("Unable to transform overloaded constructors to a single constructor via optional parameters for " + z.FullName);

                var target = targets.Single();


                // step 1
                var ctor = cache.Single(i => i.target == target);
                var args = ctor.args;

                Action CustomVariableInitialization = delegate { };

                while (true)
                {
                    ctor = cache.SingleOrDefault(i => i.target == ctor.ctor);

                    if (ctor == null)
                        break;



                    args = args.Select(
                        (s, i) =>
                        {
                            if (s.SingleStackInstruction.TargetParameter != null)
                                return ctor.args[i];

                            return s;
                        }
                    ).ToArray();
                }

                // now we should have one ctor and others that point to them

                args = args.Select(
                    a =>
                    {
                        if (a.SingleStackInstruction.IsLoadLocal)
                        {
                            // probably default(T), but we do not know for sure with this implementation
                            return null;
                        }

                        return a;
                    }
                ).ToArray();

                Action CustomVariableInitializationForBody = delegate 
				{
					WriteTypeFieldInitialization(z);

					CustomVariableInitialization(); 
				};

                WriteMethodSignature(target, false, WriteMethodSignatureMode.Delcaring, args, i => CustomVariableInitializationForBody += i, null);
                WriteMethodBody(target, this.MethodBodyFilter, CustomVariableInitializationForBody);

                r.Primary = target;
                r.Values = args;
				r.CustomVariableInitialization = CustomVariableInitializationForBody;


            }
            else
            {
				Action CustomVariableInitializationForBody = delegate
				{
					WriteTypeFieldInitialization(z);

				};

                foreach (var zc in zci)
                {
                    r.Primary = zc;

                    WriteMethodSignature(zc, false);
					WriteMethodBody(zc, this.MethodBodyFilter, CustomVariableInitializationForBody);

                }

				r.CustomVariableInitialization = CustomVariableInitializationForBody;

            }

            WriteLine();

            return r;
        }

    }
}
