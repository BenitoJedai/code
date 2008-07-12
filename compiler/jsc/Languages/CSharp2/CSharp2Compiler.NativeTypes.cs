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
        Dictionary<Type, string> NativeTypes =
           new Dictionary<Type, string>
                {
                    {typeof(byte), "byte"},
                    {typeof(sbyte), "sbyte"},
                    {typeof(short), "short"},

                    {typeof(int), "int"},
                    {typeof(char), "char"},
                    {typeof(uint), "uint"},
                    {typeof(ushort), "ushort"},

                    {typeof(bool), "bool"},

                    {typeof(long), "long"},
                    {typeof(ulong), "ulong"},

                    {typeof(double), "double"},
                    {typeof(decimal), "decimal"},
                    {typeof(float), "float"},

                    {typeof(void), "void"},
                    {typeof(string), "string"},
                    {typeof(object), "object"},
                };
    }
}
