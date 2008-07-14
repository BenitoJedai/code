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


                // write the attributes for current field
                WriteCustomAttributes(zfn);

                WriteIdent();
                WriteTypeFieldModifier(zfn);

                Write("var ");
                WriteSafeLiteral(zfn.Name);
                Write(":");
                //WriteGenericOrDecoratedTypeName(zfn.FieldType);
                WriteDecoratedTypeNameOrImplementationTypeName(zfn.FieldType, true, true, IsFullyQualifiedNamesRequired(z, zfn.FieldType));

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

                WriteLine(";");
            }
        }


        public override void WriteTypeFieldModifier(FieldInfo zfn)
        {
            // If the field is not public and also not used then we can ditch the public keywword
            // until then we always need public because the field is used by other generated types

            Write("public ");

            /*
            if (zfn.IsPublic)
                Write("public ");
            else
            {
                if (zfn.IsFamily)
                    Write("protected ");
                else
                    Write("private ");
            }*/
            /*
            if (zfn.IsInitOnly)
                WriteKeywordFinal();
            */
            if (zfn.IsStatic)
                Write("static ");

            /*
            if (zfn.IsNotSerialized)
                Write("transient ");
             * */
        }


    }
}
