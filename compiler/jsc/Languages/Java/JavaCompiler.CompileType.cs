
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;
using System.Threading;

using jsc.CodeModel;

using ScriptCoreLib;
using jsc.Script;

namespace jsc.Languages.Java
{

    partial class JavaCompiler
    {

        public override bool CompileType(Type z)
        {
            if (IsNativeType(z))
                return false;

            if (IsEmptyImplementationType(z))
                return false;

            if (ScriptAttribute.IsAnonymousType(z))
                return false;

            //WriteMachineGeneratedWarning();

            if (z.Namespace != null)
            {
                WriteIdent();
                Write("package " + NamespaceFixup(z.Namespace, z) + ";");
                WriteLine();
                WriteLine();
            }

            this.WriteImportTypes(z);

            WriteLine();


            ScriptAttribute za = ScriptAttribute.Of(z, true);



            #region type summary
            XmlNode u = GetXMLNode(z);

            if (u != null)
                WriteBlockComment(u["summary"].InnerText);
            #endregion


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


            }

            //Thread.Sleep(100);

            return true;
        }


    }
}
