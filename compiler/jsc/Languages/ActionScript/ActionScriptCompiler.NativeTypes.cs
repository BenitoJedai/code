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
        Dictionary<Type, string> NativeTypes =
            new Dictionary<Type, string>
                {
                    {typeof(byte), "uint"},
                    {typeof(sbyte), "int"},
                    {typeof(short), "int"},

                    {typeof(int), "int"},
                    {typeof(char), "int"}, // char = int
                    {typeof(uint), "uint"},
                    {typeof(ushort), "uint"},

                    {typeof(bool), "Boolean"},

                    {typeof(long), "Number"},
                    {typeof(ulong), "Number"},

                    {typeof(double), "Number"},
                    {typeof(decimal), "Number"},
                    {typeof(float), "Number"},

                    {typeof(void), "void"},
                    {typeof(string), "String"},
                    {typeof(object), "Object"},
                };
    }
}
