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

        public class TypeInstanceConstructorInfo
        {
            public ILBlock ConstructorCode;
            public ConstructorInfo Constructor;
            public ConstructorInfo TargetConstructor;
            public ILFlowStackItem[] TargetConstructorArguments;

            public override string ToString()
            {
                return this.Constructor.MetadataToken.ToString("x8") + " -> " + this.TargetConstructor.MetadataToken.ToString("x8");
            }
        }



        protected override void WriteTypeInstanceConstructors(Type z)
        {
            WriteTypeInstanceConstructorsAndGetPrimary(z);
        }

        public class ConstructorMergeInfo
        {
            public ConstructorInfo Primary;

            public ILFlowStackItem[] Values;

            public Action CustomVariableInitialization;
        }

        public void WriteTypeFieldInitialization(Type z)
        {
            foreach (var CandidateField in z.GetFields(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (CandidateField.FieldType == typeof(double))
                {
                    // we need to initialize Number fields for actionscript
                    WriteIndent();
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

        public class ConstructorInlineInfo
        {
            public readonly Type z;
            public ConstructorInfo[] Constructors { get; private set; }
            public TypeInstanceConstructorInfo[] SatelliteConstructors { get; private set; }
            public ConstructorInfo PrimaryConstructor { get; private set; }

            public ConstructorInlineInfo(Type z)
            {
                this.Constructors = GetAllInstanceConstructors(z);

                if (!(this.Constructors.Length > 1))
                    return;

                this.SatelliteConstructors = Enumerable.ToArray(
                    from Constructor in Constructors

                    let ConstructorCode = new ILBlock(Constructor)

                    let CallInstructions = ConstructorCode.Prestatements.PrestatementCommands.Where(
                        p => p.Instruction != null && !p.Instruction.IsAnyOpCodeOf(OpCodes.Initobj, OpCodes.Ret, OpCodes.Nop)
                    ).ToArray()
                    where (CallInstructions.Length == 1 && CallInstructions[0].Instruction == OpCodes.Call)

                    let i = CallInstructions[0].Instruction

                    let TargetConstructor = i.TargetConstructor

                    // let's make sure our satellite is calling a constructor within same type
                    where TargetConstructor != null && Constructors.Contains(TargetConstructor)

                    let TargetConstructorArguments = i.StackBeforeStrict.Skip(1).ToArray()
                    // skip the ldarg0/this op
                    select new TypeInstanceConstructorInfo
                    {
                        ConstructorCode = ConstructorCode,
                        Constructor = Constructor, /*b,*/
                        TargetConstructor = TargetConstructor,
                        TargetConstructorArguments = TargetConstructorArguments
                    }
                );


                #region PrimaryConstructor
                var PrimaryConstructors = Constructors.Except(SatelliteConstructors.Select(i => i.Constructor)).ToArray();

                if (PrimaryConstructors.Length != 1)
                    throw new NotSupportedException("Unable to transform overloaded constructors to a single constructor via optional parameters for " + z.FullName);

                this.PrimaryConstructor = PrimaryConstructors.Single();
                #endregion
            }

            public ILFlowStackItem[] InsertDefaults(ILFlowStackItem[] s, ConstructorInfo TargetMethod, int offset)
            {
                var SatteliteConstructor = this.SatelliteConstructors.Single(k => k.Constructor == TargetMethod);

                return s.Take(offset).Concat(
                    SatteliteConstructor.TargetConstructorArguments.Select(
                    a =>
                    {
                        var i = a.SingleStackInstruction;

                        if (i.IsLoadLocal)
                        {
                            // http://maohao.wordpress.com/2009/02/26/actionscript-101-null-vs-undefined/

                            // default(T) yay
                            return new ILFlowStackItem(
                                new ILInstruction(OpCodes.Ldnull),
                                0
                            );
                        }

                        var p = i.TargetParameter;

                        if (p == null)
                            return a;

                        // s[0] is this
                        return s[p.Position + offset];
                    }
                )).ToArray();
            }
        }

        protected ConstructorMergeInfo WriteTypeInstanceConstructorsAndGetPrimary(Type z)
        {
            var r = new ConstructorMergeInfo();

            var i = new ConstructorInlineInfo(z);

            var Constructors = i.Constructors;



            if (Constructors.Length > 1)
            {
                // as3 does not support method overloading but does support default parameters
                // we need to figure out which ctor is real and which are just sattelites

                // default values should be extended to allow instance values
                // a workaround could be:
                // if (param == null) { param = UIComponent; } 







                Action CustomVariableInitialization = delegate { };

                Action CustomVariableInitializationForBody = delegate
                {
                    WriteTypeFieldInitialization(z);

                    CustomVariableInitialization();
                };

                WriteMethodSignature(i.PrimaryConstructor, false, WriteMethodSignatureMode.Declaring,
                    null
                    , ii => CustomVariableInitializationForBody += ii, null);


                WriteMethodBody(i.PrimaryConstructor, this.MethodBodyFilter, CustomVariableInitializationForBody);

                r.Primary = i.PrimaryConstructor;
                r.Values = null;
                r.CustomVariableInitialization = CustomVariableInitializationForBody;


            }
            else
            {
                Action CustomVariableInitializationForBody = delegate
                {
                    WriteTypeFieldInitialization(z);

                };

                foreach (var zc in Constructors)
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
