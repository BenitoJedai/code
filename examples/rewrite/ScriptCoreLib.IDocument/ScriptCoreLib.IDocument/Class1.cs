using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.IO;
using jsc.meta.Library.CodeTrace;
using System.Diagnostics;

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

class __IDocument
{
}

class __IXMLDocument 
{
}
public class Program
{

    public static void Main()
    {
        var Output = "z.exe";
        try
        {
            new FileInfo("z.CodeTrace.exe").ToCodeTrace(
                ct =>
                {
                    var a = default(AssemblyBuilder);
                    var m = default(ModuleBuilder);
                    var TypeCache = default(Dictionary<int, TypeBuilder>);

                    var SourceType_IDocument = typeof(__IDocument).MetadataToken;
                    var SourceType_IXMLDocument = typeof(__IXMLDocument).MetadataToken;

                    ct.Invoke(
                        delegate
                        {
                            TypeCache = new Dictionary<int, TypeBuilder>();

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
                              TypeCache[SourceType_IDocument] = IDocument;
                          j4: IDocument.SetParent(typeof(object));
                          j5: var TConstraint1 = IDocument.DefineGenericParameters("TConstraint1");


                          j6: var IXMLDocument = m.DefineType("IXMLDocument");
                              TypeCache[SourceType_IXMLDocument] = IXMLDocument;

                              var IXMLDocumentParent = IDocument.MakeGenericType(typeof(object));
                          j7: IXMLDocument.SetParent(IXMLDocumentParent);

                          j8: var Method2 = IXMLDocument.DefineMethod("Method2", MethodAttributes.Public);



                          j9: var TConstraint2 = Method2.DefineGenericParameters("TConstraint2")[0];

                          j10: TConstraint2.SetInterfaceConstraints(typeof(ICloneable));

                              Method2.SetParameters(TConstraint2);

                              Method2.GetILGenerator().Emit(OpCodes.Ret);


                          }
                      );

                    //throw new Exception();

                    ct.Invoke(
                        delegate
                        {

                            TypeCache[SourceType_IDocument].DefineField("__Type1", TypeCache[SourceType_IXMLDocument], FieldAttributes.Public);

                            TypeCache[SourceType_IDocument].CreateType();
                            TypeCache[SourceType_IXMLDocument].CreateType();

                            //a.SetEntryPoint(null, PEFileKinds.ConsoleApplication);

                            a.Save(Output);
                        }
                    );
                }
            );
        }
        catch
        {
            Debugger.Break();
        }

    }
}