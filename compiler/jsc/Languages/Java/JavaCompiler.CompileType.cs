
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;
using ScriptCoreLib;
using ScriptCoreLib.Shared;

namespace jsc.Languages.Java
{

    partial class JavaCompiler
    {
        public Action CompileType_WriteAdditionalMembers;

        public override bool CompileType(Type z)
        {
            if (IsNativeType(z))
                return false;

            if (z.Name.Contains("<PrivateImplementationDetails>") || (z.DeclaringType != null && z.DeclaringType.Name.Contains("<PrivateImplementationDetails>")))
                return false;

            // why would we do that?
            //if (IsEmptyImplementationType(z))
            //    return false;

            if (ScriptAttribute.IsAnonymousType(z))
                return false;

            //WriteMachineGeneratedWarning();
            WriteCommentLine(Path.GetFileName(z.Assembly.Location));

            if (z.Namespace != null)
            {
                this.WriteIndent();
                this.WriteKeywordSpace(Keywords._package);

                var _namespace = string.Join(".", NamespaceFixup(z.Namespace, z).Split('.').Select(k => GetSafeLiteral(k)).ToArray());

                this.Write(_namespace + ";");

                //this.Write(NamespaceFixup(z.Namespace, z) + ";");
                this.WriteLine();
                this.WriteLine();
            }

            this.WriteImportTypes(z);

            WriteLine();


            ScriptAttribute za = ScriptAttribute.Of(z, true);

            var z_Implements = za.Implements;
            var z_NonPrimitiveValueType = z_Implements != null && z_Implements.IsValueType && !z_Implements.IsPrimitive;


            #region type summary
            XmlNode u = GetXMLNode(z);

            if (u != null)
                WriteBlockComment(u["summary"].InnerText);
            #endregion

            CompileType_WriteAdditionalMembers = delegate { };

            WriteTypeSignature(z, za);

            using (CreateScope())
            {
                WriteTypeFields(z, za);
                WriteLine();
                WriteTypeStaticConstructor(z, za);
                WriteLine();

                // why was this check here?
                //if (za.Implements == null)
                //{
                WriteTypeInstanceConstructors(z);
                WriteLine();
                //}

                WriteTypeInstanceMethods(z, za);
                WriteLine();
                WriteTypeStaticMethods(z, za);

                if (za.Implements == typeof(Delegate))
                {
                    DelegateImplementationProvider.WriteExtensionMethodSupport(this, z);
                }

                CompileType_WriteAdditionalMembers();


                if (z_NonPrimitiveValueType)
                {
                    // define ctor as methods
                    WriteIndent();
                    WriteCommentLine("NonPrimitiveValueType");

                    foreach (var NonPrimitiveValueTypeConstructor in z.GetInstanceConstructors())
                    {
                        InternalWriteMethodSignature(
                            NonPrimitiveValueTypeConstructor,
                            false,
                            "NonPrimitiveValueTypeConstructor",
                            false
                        );

                        WriteMethodBody(NonPrimitiveValueTypeConstructor);
                    }
                }
            }

            //Thread.Sleep(100);

            return true;
        }

    }
}
