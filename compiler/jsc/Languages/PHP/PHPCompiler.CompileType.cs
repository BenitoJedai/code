using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

using jsc.CodeModel;

using ScriptCoreLib;

namespace jsc.Script.PHP
{

    partial class PHPCompiler 
    {
       
        public override bool CompileType(Type z)
        {
            try
            {
            

                if (z.IsEnum)
                    return false;

                if (z.IsValueType)
                    Break("ValueType not supported : " + z.FullName);

                var za = z.ToScriptAttributeOrDefault();

                if (z.BaseType == typeof(global::System.MulticastDelegate))
                {
                    jsc.Languages.PHP.DelegateImplementationProvider.Write(this, z);

                    return true;
                }


                if (z.BaseType != typeof(object) && z.BaseType != null)
                {
                    WriteImport(TypeInfoOf(z.BaseType));
                }


                //Console.WriteLine(z.FullName);



                if (!z.ToScriptAttributeOrDefault().InternalConstructor)
                {
                    WriteTypeSignature(z, za);

                    using (CreateScope())
                    {

                        WriteTypeFields(z, za);
                        WriteTypeInstanceConstructors(z);
                        WriteTypeInstanceMethods(z, za);

                        WriteTypeVirtualMethods(z, za);

                    }
                }

                WriteLine();


                WriteTypeStaticMethods(z, za);

                WriteLine();


                WriteTypeStaticConstructor(z, true);

                WriteLine();
            }
            catch
            {
                Break("internal error while compiling type " + z.FullName);
            }


            return true;
        }

    }
}
