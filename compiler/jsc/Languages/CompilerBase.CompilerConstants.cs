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

using Dia2Lib;


namespace jsc.Script
{

    public abstract partial class CompilerBase
    {

        public static string ReplaceWithCompilerConstants(string code, MethodBase m, Func<ParameterInfo, string> h)
        {
            code = code.Replace("{BuildDate}", DateTime.Now.ToUniversalTime() + "");
            code = code.Replace("{CompilerBuildDate}", ConstantCompilerBuildDate);
            code = code.Replace("{Module.Name}", m.Module.Name);

            int i = 0;

            foreach (ParameterInfo v in m.GetParameters())
            {
                code = code.Replace("{arg" + (i++) + "}", h(v));

            }




            return code;
        }

        private  string ReplaceWithCompilerConstants(string code, MethodBase m)
        {
            return CompilerBase.ReplaceWithCompilerConstants(code, m, GetDecoratedMethodParameter);

        }

        public static string ConstantCompilerBuildDate
        {
            get
            {
                return GetExecutingAssamblyWriteTime().ToUniversalTime() + "";
            }
        }

        private static DateTime GetExecutingAssamblyWriteTime()
        {
            Assembly a = Assembly.GetExecutingAssembly();

            DateTime x = new FileInfo(a.ManifestModule.FullyQualifiedName).LastWriteTimeUtc;
            return x;
        }



        public string NamespaceFixup(string p)
        {
            if (this.CurrentJob != null)
                return this.CurrentJob.NamespaceFixup(p);

            return p;
        }

        internal void ToConsole(Type xx, CompileSessionInfo sinfo)
        {
            string u = NamespaceFixup(xx.FullName);

            if (u == xx.FullName)
                sinfo.Logging.LogMessage(u);
            else
                sinfo.Logging.LogMessage("{0} -> {1}", xx.FullName, u);
        }
    }
}
