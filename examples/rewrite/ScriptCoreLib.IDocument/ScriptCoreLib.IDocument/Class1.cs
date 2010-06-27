﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.IO;
using jsc.meta.Library.CodeTrace;

[assembly: Obfuscation(Feature = "merge")]



public class IDocument<TConstraint1>
{
    public IXMLDocument __Type1;
}


public class IXMLDocument : IDocument<object>
{
    void Method2<TConstraint2>(TConstraint2 e) where TConstraint2 : ICloneable
    {

    }
}

public class Program
{
    static string Output = "z.exe";

    public static void Main()
    {
        //var Output = "z.exe";

        using (var ct = new FileInfo("x.exe").ToCodeTrace())
        {
            var a = default(AssemblyBuilder);
            var m = default(ModuleBuilder);

            ct.Invoke(
                delegate
                {
                    var name = new AssemblyName(Path.GetFileNameWithoutExtension(Output));

                    a = AppDomain.CurrentDomain.DefineDynamicAssembly(
                       name, AssemblyBuilderAccess.RunAndSave
                    );

                    m = a.DefineDynamicModule(Path.GetFileNameWithoutExtension(Output), Output);

                }
            );

            ct.Invoke(
                  delegate
                  {
                  j3: var IDocument = m.DefineType("IDocument`1");
                  j4: IDocument.SetParent(typeof(object));
                  j5: var TConstraint1 = IDocument.DefineGenericParameters("TConstraint1");


                  j6: var IXMLDocument = m.DefineType("IXMLDocument");


                      var IXMLDocumentParent = IDocument.MakeGenericType(typeof(object));
                  j7: IXMLDocument.SetParent(IXMLDocumentParent);

                  j8: var Method2 = IXMLDocument.DefineMethod("Method2", MethodAttributes.Public);



                  j9: var TConstraint2 = Method2.DefineGenericParameters("TConstraint2")[0];

                  j10: TConstraint2.SetInterfaceConstraints(typeof(ICloneable));

                      Method2.SetParameters(TConstraint2);

                      Method2.GetILGenerator().Emit(OpCodes.Ret);

                  j11: IDocument.DefineField("__Type1", IXMLDocument, FieldAttributes.Public);

                  j12: IDocument.CreateType();
                  j13: IXMLDocument.CreateType();
                  }
              );

            ct.Invoke(
                delegate
                {
                    //a.SetEntryPoint(null, PEFileKinds.ConsoleApplication);

                    a.Save(Output);
                }
            );
        }

    }
}