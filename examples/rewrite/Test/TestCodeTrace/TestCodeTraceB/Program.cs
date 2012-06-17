using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.IO;
using System.Reflection;

internal static class Program
{
    // Fields
    public static Type _ct_DeclaringType;
    public static GenericTypeParameterBuilder[] _ct_GenericParameters;
    public static string _ct_Product_Name;
    public static int _ct_SourceMethod;
    public static Dictionary<int, Func<ILGenerator>> _ct_SourceMethodHashToILGeneratorLookup;
    public static Dictionary<int, MethodBuilder> _ct_SourceMethodHashToMethodBuilderLookup;
    public static Dictionary<int, Type> _ct_SourceTypeHashToTypeLookup;
    public static AssemblyBuilder a;
    public static FieldBuilder DeclaringField;
    public static MethodBuilder DeclaringMethod;
    public static TypeBuilder DeclaringType;
    public static string DefineTypeName;
    public static ModuleBuilder m;

    // Methods
    /* private scope */ static void Main()
    {
        Console.WriteLine("CodeTrace was used to record the steps to build a specific assembly.");
        Console.WriteLine("CodeTrace started at 28/06/2010 11:46:26");
        _ct_Product_Name = "TestCodeTrace.Rewrite.exe";
        Dictionary<int, Type> dictionary = new Dictionary<int, Type>();
        dictionary.Add(0, null);
        dictionary.Add(typeof(object).GetHashCode(), typeof(object));
        _ct_SourceTypeHashToTypeLookup = dictionary;
        Dictionary<int, MethodBuilder> dictionary2 = new Dictionary<int, MethodBuilder>();
        dictionary2.Add(0, null);
        _ct_SourceMethodHashToMethodBuilderLookup = dictionary2;
        _ct_SourceMethodHashToILGeneratorLookup = new Dictionary<int, Func<ILGenerator>>();
        a = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(Path.GetFileNameWithoutExtension(_ct_Product_Name)), AssemblyBuilderAccess.RunAndSave, @"W:\jsc.svn\examples\rewrite\TestCodeTrace\TestCodeTrace\bin\Debug\staging");
        m = a.DefineDynamicModule(Path.GetFileNameWithoutExtension(_ct_Product_Name), "~TestCodeTrace.Rewrite.exe");
        DefineTypeName = "TestCodeTrace.Program";
        Console.WriteLine("CodeTrace DefineType " + DefineTypeName);
        DeclaringType = m.DefineType(DefineTypeName, TypeAttributes.BeforeFieldInit, _ct_SourceTypeHashToTypeLookup[0], new Type[0]);
        _ct_SourceTypeHashToTypeLookup[0x52a258] = DeclaringType;
        (_ct_SourceTypeHashToTypeLookup[0x52a258] as TypeBuilder).SetParent(_ct_SourceTypeHashToTypeLookup[0x704e84dc]);
        DeclaringMethod = (_ct_SourceTypeHashToTypeLookup[0x52a258] as TypeBuilder).DefineMethod("Main", MethodAttributes.HideBySig | MethodAttributes.Static | MethodAttributes.FamORAssem, CallingConventions.Standard, null, null);
        _ct_SourceMethodHashToMethodBuilderLookup[-1395084204] = DeclaringMethod;
        _ct_SourceMethod = -1395084204;
        _ct_SourceMethodHashToILGeneratorLookup[_ct_SourceMethod] = new Func<ILGenerator>(_ct_SourceMethodHashToMethodBuilderLookup[_ct_SourceMethod].GetILGenerator);
        _ct_SourceMethodHashToILGeneratorLookup[-1395084204]().Emit(OpCodes.Nop);
        _ct_SourceMethodHashToILGeneratorLookup[-1395084204]().Emit(OpCodes.Ret);
        DeclaringMethod = (_ct_SourceTypeHashToTypeLookup[0x52a258] as TypeBuilder).DefineMethod("Method2", MethodAttributes.HideBySig | MethodAttributes.Static | MethodAttributes.Private, CallingConventions.Standard, null, null);
        _ct_SourceMethodHashToMethodBuilderLookup[-1395116976] = DeclaringMethod;
        _ct_SourceMethod = -1395116976;
        _ct_SourceMethodHashToILGeneratorLookup[_ct_SourceMethod] = new Func<ILGenerator>(_ct_SourceMethodHashToMethodBuilderLookup[_ct_SourceMethod].GetILGenerator);
        DefineTypeName = "TestCodeTrace.IDocument`1";
        Console.WriteLine("CodeTrace DefineType " + DefineTypeName);
        DeclaringType = m.DefineType(DefineTypeName, TypeAttributes.BeforeFieldInit | TypeAttributes.Public, _ct_SourceTypeHashToTypeLookup[0], new Type[0]);
        _ct_SourceTypeHashToTypeLookup[0x41330fc] = DeclaringType;
        (_ct_SourceTypeHashToTypeLookup[0x41330fc] as TypeBuilder).SetParent(_ct_SourceTypeHashToTypeLookup[0x704e84dc]);
        _ct_GenericParameters = (_ct_SourceTypeHashToTypeLookup[0x41330fc] as TypeBuilder).DefineGenericParameters(new string[] { "TConstraint1" });
        List<Type> list = new List<Type>();
        foreach (int num in new int[] { 0x704e84dc })
        {
            list.Add(_ct_SourceTypeHashToTypeLookup[num]);
        }
        _ct_DeclaringType = _ct_SourceTypeHashToTypeLookup[0x41330fc].MakeGenericType(list.ToArray());
        _ct_SourceTypeHashToTypeLookup[0x41331e0] = _ct_DeclaringType;
        DefineTypeName = "TestCodeTrace.IXMLDocument";
        Console.WriteLine("CodeTrace DefineType " + DefineTypeName);
        DeclaringType = m.DefineType(DefineTypeName, TypeAttributes.BeforeFieldInit | TypeAttributes.Public, _ct_SourceTypeHashToTypeLookup[0], new Type[0]);
        _ct_SourceTypeHashToTypeLookup[0x4133dec] = DeclaringType;
        (_ct_SourceTypeHashToTypeLookup[0x4133dec] as TypeBuilder).SetParent(_ct_SourceTypeHashToTypeLookup[0x41331e0]);
        DeclaringMethod = (_ct_SourceTypeHashToTypeLookup[0x4133dec] as TypeBuilder).DefineMethod("Method2", MethodAttributes.HideBySig | MethodAttributes.Private, CallingConventions.HasThis | CallingConventions.Standard, null, null);
        _ct_SourceMethodHashToMethodBuilderLookup[-1129754532] = DeclaringMethod;
        _ct_SourceMethod = -1129754532;
        _ct_SourceMethodHashToILGeneratorLookup[_ct_SourceMethod] = new Func<ILGenerator>(_ct_SourceMethodHashToMethodBuilderLookup[_ct_SourceMethod].GetILGenerator);
        _ct_SourceMethodHashToILGeneratorLookup[-1129754532]().Emit(OpCodes.Nop);
        _ct_SourceMethodHashToILGeneratorLookup[-1129754532]().Emit(OpCodes.Ret);
        DeclaringField = (_ct_SourceTypeHashToTypeLookup[0x41330fc] as TypeBuilder).DefineField("__Type1", _ct_SourceTypeHashToTypeLookup[0x4133dec], FieldAttributes.Public);
        Console.WriteLine("CodeTrace CreateType: " + "TestCodeTrace.IDocument`1");
        (_ct_SourceTypeHashToTypeLookup[0x41330fc] as TypeBuilder).CreateType();
        Console.WriteLine("CodeTrace CreateType: " + "TestCodeTrace.IXMLDocument");
        (_ct_SourceTypeHashToTypeLookup[0x4133dec] as TypeBuilder).CreateType();
        _ct_SourceMethodHashToILGeneratorLookup[-1395116976]().Emit(OpCodes.Nop);
        _ct_SourceMethodHashToILGeneratorLookup[-1395116976]().Emit(OpCodes.Ret);
        Console.WriteLine("CodeTrace CreateType: " + "TestCodeTrace.Program");
        (_ct_SourceTypeHashToTypeLookup[0x52a258] as TypeBuilder).CreateType();
        m.CreateGlobalFunctions();
        a.Save("~TestCodeTrace.Rewrite.exe");
        Console.WriteLine("CodeTrace ended at 28/06/2010 11:46:28");
    }
}

 
 
