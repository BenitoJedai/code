using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

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
    public static void Main()
    {
    j1: var a = AppDomain.CurrentDomain.DefineDynamicAssembly(
       new AssemblyName("z"), AssemblyBuilderAccess.RunAndSave
    );

    j2: var m = a.DefineDynamicModule("z", "z.dll");

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

        a.Save("z.dll");

    }
}