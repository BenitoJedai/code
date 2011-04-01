using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using java.io;
using java.net;
using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;

namespace ScriptCoreLib.Java
{
    public interface IJavaArchiveReflector
    {
        string FileNameString { get; }

        int Count { get; }

        bool IsType(int TypeIndex);

        string GetTypeFullName(int index);

        int GetMethodCount(int TypeIndex);

        string GetMethodName(int TypeIndex, int MethodIndex);
    }

    partial class JavaArchiveReflector
    {
        public bool IsType(int TypeIndex)
        {
            return !string.IsNullOrEmpty(this.Entries[TypeIndex].TypeFullName); 
        }

        public int GetMethodCount(int TypeIndex)
        {
            return this.Entries[TypeIndex].Methods.Length;
        }

        public string GetMethodName(int TypeIndex, int MethodIndex)
        {
            return this.Entries[TypeIndex].Methods[MethodIndex].Name;
        }
    }
}
