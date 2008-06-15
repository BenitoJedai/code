using System;
using System.Globalization;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Runtime;
using System.Reflection.Emit;
using System.Reflection;
using System.Xml;

using ScriptCoreLib;

#if WITH_DIA
using Dia2Lib;
#endif

using System.Runtime.InteropServices;

namespace jsc.Script
{

    public abstract partial class CompilerBase
    {

        public static class DIACache
        {

            class Session
            {

                enum NameSearchOptions
                {
                    nsNone = 0,
                    nsfCaseSensitive = 0x1,
                    nsfCaseInsensitive = 0x2,
                    nsfFNameExt = 0x4,
                    nsfRegularExpression = 0x8,
                    nsfUndecoratedName = 0x10,
                    nsCaseSensitive = nsfCaseSensitive,
                    nsCaseInsensitive = nsfCaseInsensitive,
                    nsFNameExt = nsfCaseInsensitive | nsfFNameExt,
                    nsRegularExpression = nsfRegularExpression | nsfCaseSensitive,
                    nsCaseInRegularExpression = nsfRegularExpression | nsfCaseInsensitive
                };

                public string pdbPath = null;

#if WITH_DIA
                public DiaSource DiaSource = null;
                public IDiaSession DiaSession = null;
#endif


                public Session(Assembly a, CompilerBase z)
                {
                    if (z.CurrentJob != null)
                    {
                        pdbPath = z.CurrentJob.AssamblyFile.DirectoryName + "\\" + new FileInfo(a.Location).Name;
                        pdbPath = pdbPath.Substring(0, pdbPath.Length - 4) + ".pdb";


                    }
                    else
                        pdbPath = a.Location.Substring(0, a.Location.Length - 4) + ".pdb";


#if WITH_DIA
                    if (File.Exists(pdbPath))
                    {
                        try
                        {
                            this.DiaSource = new Dia2Lib.DiaSource();
                            this.DiaSource.loadDataFromPdb(this.pdbPath);

                            this.DiaSource.openSession(out this.DiaSession);
                        }
                        catch (COMException exc)
                        {
                            this.DiaSource = null;

                            Console.WriteLine("DIA API not found for {0}", pdbPath);
                        }

                    }
                    else
                        Console.WriteLine("pdb not found {0}", pdbPath);
#endif

                }

#if WITH_DIA
                private static bool NextSymbol(IDiaEnumSymbols es, out IDiaSymbol sym)
                {
                    sym = null;

                    uint fet = 0;

                    es.Next(1, out sym, out fet);

                    return fet != 0;
                }

                public static IDiaSymbol[] GetSymbols(IDiaSymbol parent, SymTagEnum tag)
                {
                    List<IDiaSymbol> a = new List<IDiaSymbol>();

                    IDiaEnumSymbols ss;

                    parent.findChildren(tag, null, 0, out ss);


                        IDiaSymbol symbol = null;

                        while (NextSymbol(ss, out symbol))
                        {
                            a.Add(symbol);
                        }

                    return a.ToArray();
                }
#endif

                public string GetVariableName(MethodBase m, LocalVariableInfo var)
                {
                    // nameless exceptions cause null LocalVariableInfo

                    if (var == null)
                        return null;

#if WITH_DIA
                    try
                    {
                        

                        if (DiaSession != null )
                        {
                            IDiaSymbol methodsymbol = null;

                            DiaSession.findSymbolByToken((uint)m.MetadataToken, SymTagEnum.SymTagFunction, out methodsymbol);

                            if (methodsymbol != null)
                            {
                                string _name = ResolveVariableNameByBlock(var, methodsymbol);


                                // variables with the same name?

                                if (_name != null)
                                {
                                    _name = _name.Replace("<", "_");
                                    _name = _name.Replace(">", "_");

                                    return _name;
                                }
                            }
                            // + var.LocalIndex;  
                        }


                    }
                    catch
                    {
                        throw new Exception("pdb version mismatch");
                    }
#endif


                    return GuessName(var);

                }

#if WITH_DIA
                private static string ResolveVariableNameByBlock(LocalVariableInfo var, IDiaSymbol s)
                {

                    string _name = null;

                    foreach (IDiaSymbol block in GetSymbols(s, SymTagEnum.SymTagBlock))
                    {
                        _name = ResolveVariableName(var, block);

                        if (_name != null)
                            break;

                        _name = ResolveVariableNameByBlock(var, block);

                        if (_name != null)
                            break;
                    }

                    return _name;
                }

                private static string ResolveVariableName(LocalVariableInfo var, IDiaSymbol s)
                {
                    string _name = null;

                    foreach (IDiaSymbol symbol in GetSymbols(s, SymTagEnum.SymTagData))
                    {
                        if (symbol.slot == var.LocalIndex)
                        {
                            if (symbol.compilerGenerated > 0)
                            {
                                break;
                            }

                            _name = symbol.name;
                        }
                    }
                    return _name;
                }
#endif

                private static string GuessName(LocalVariableInfo var)
                {
                    Type xt = var.LocalType;

                    while (xt.IsArray)
                        xt = xt.GetElementType();

                    string x = xt.Name;

                    if (xt == typeof(bool))
                        x = "flag";
                    else if (xt == typeof(int))
                        x = "num";
                    else if (xt == typeof(long))
                        x = "num";
                    else if (xt == typeof(short))
                        x = "num";
                    else
                    {
                        int xi = 0;

                        for (int i = 0; i < x.Length; i++)
                        {
                            if (char.IsUpper(x[i]))
                                xi = i;
                        }

                        x = x.Substring(xi).ToLower();
                    }

                    if (var.LocalType.IsArray)
                        x += "Array";

                    return x + var.LocalIndex;
                }
            }

            static Dictionary<Assembly, Session> dict = new Dictionary<Assembly, Session>();


            public static string GetVariableName(Type t, MethodBase m, LocalVariableInfo var, CompilerBase z)
            {
                string _name = null;


                if (!dict.ContainsKey(t.Assembly))
                {
                    dict[t.Assembly] = new Session(t.Assembly, z);
                }


                _name = dict[t.Assembly].GetVariableName(m, var);


                return _name;
            }
        }

        /// <summary>
        /// tries to read the actual variable name aswell
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <param name="localVariableInfo"></param>
        public void WriteVariableName(Type type, MethodBase method, LocalVariableInfo var)
        {
            WriteSafeLiteral(DIACache.GetVariableName(type, method, var, this) ?? "_");


        }







    }
}
