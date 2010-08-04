using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

namespace jsc.Languages.CSharp2
{
    partial class CSharp2Compiler
    {
        public void WriteEnumFields(Type z, ScriptAttribute za)
        {
            FieldInfo[] zf = GetAllFields(z);

            foreach (FieldInfo zfn in zf)
            {
                if (!zfn.IsLiteral)
                    continue;

                WriteIndent();
                
                WriteSafeLiteral(zfn.Name);

                WriteAssignment();

                Write(zfn.GetRawConstantValue().ToString());

                WriteLine(",");
            }
        }

        public override void WriteTypeFields(Type z, ScriptAttribute za)
        {
            FieldInfo[] zf = GetAllFields(z);

            foreach (FieldInfo zfn in zf)
            {
                // external class cannot have static variables inside a type
                // should be defined outside as global static instead
                if ((za.HasNoPrototype || za.ImplementationType != null) && !zfn.IsStatic)
                    continue;

                if (zfn.IsLiteral)
                    continue;

                if (EventDetector.IsEvent(zfn))
                    continue;


                // write the attributes for current field
                // WriteCustomAttributes(zfn);

                WriteIndent();
                WriteTypeFieldModifier(zfn);

                WriteGenericTypeName(z, zfn.FieldType);

                WriteSpace();

                WriteSafeLiteral(zfn.Name);
                WriteLine(";");
                //WriteGenericOrDecoratedTypeName(zfn.FieldType);

                //WriteDecoratedTypeNameOrImplementationTypeName(zfn.FieldType, true, true);
                //WriteSpace();

                //WriteVariableType(zfn.FieldType, true);

                /*
                if (zfn.IsStatic && zfn.IsInitOnly)
                {
                    ConstructorInfo[] ci = z.GetConstructors(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public);

                    if (ci.Length == 1)
                    {
                        ILBlock cctor = new ILBlock(ci[0]);
                        ILBlock.Prestatement assign = cctor.GetStaticFieldFinalAssignment(zfn);

                        if (assign != null)
                        {
                            WriteAssignment();

                            EmitFirstOnStack(assign);
                        }
                    }
                }
                */

                
            }
        }


        public override void WriteTypeFieldModifier(FieldInfo zfn)
        {

            if (zfn.IsPublic)
                WriteKeywordSpace(Keywords._public);
            else
            {
                if (zfn.IsFamily)
                    WriteKeywordSpace(Keywords._protected);
                else
                    WriteKeywordSpace(Keywords._private);
            }

            if (zfn.IsInitOnly)
                WriteKeywordSpace(Keywords._readonly);

            if (zfn.IsStatic)
                WriteKeywordSpace(Keywords._static);

            /*
            if (zfn.IsNotSerialized)
                Write("transient ");
             * */
        }


    }
}
