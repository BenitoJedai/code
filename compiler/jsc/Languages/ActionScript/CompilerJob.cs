using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace jsc.Languages
{
    using ScriptCoreLib;

    using Languages;
    using Script;

    public partial class CompilerJob
    {

        private static void CompileActionScript(CompilerJob j, CompileSessionInfo sinfo)
        {
            IdentWriter xw = new IdentWriter();

            xw.Session = new AssamblyTypeInfo();

            //sinfo.Logging.LogMessage("loading types");

            Type[] alltypes = j.LoadTypes(ScriptType.ActionScript);


            xw.Session.Types = /*JustMyCodeFilter(sinfo.Options.JustMyCode,*/ alltypes/*, j.AssamblyInfo)*/;

            xw.Session.ImplementationTypes.AddRange(alltypes);

            sinfo.Logging.LogMessage("found {0} types to be compiled", xw.Session.Types.Length);

            DirectoryInfo TargetDirectory = j.AssamblyFile.Directory.CreateSubdirectory("web");
            //DirectoryInfo SourceDir = TargetDirectory.CreateSubdirectory("java");
            //DirectoryInfo SourceCompiledDir = TargetDirectory.CreateSubdirectory("release");
            //DirectoryInfo SourceCompiledHeadersDir = TargetDirectory.CreateSubdirectory("headers");
            //DirectoryInfo SourceNativeDir = TargetDirectory.CreateSubdirectory("native");
            DirectoryInfo SourceBinDir = TargetDirectory.CreateSubdirectory("bin");
            DirectoryInfo SourceVersionDir = TargetDirectory.CreateSubdirectory("version");

            #region SourceVersion
            FileInfo SourceVersion = new FileInfo(
                SourceVersionDir.FullName + "/" 
                + j.AssamblyInfo.ManifestModule.Name 
                + "." + Enum.GetName(typeof(ScriptType), ScriptType.ActionScript) 
                + ".version.txt"
                );

            if (SourceVersion.Exists)
            {
                if (j.AssamblyFile.LastWriteTime <= SourceVersion.LastWriteTime)
                {
                    Console.WriteLine("this version is already built: " + SourceVersion.Name);

                    if (!Debugger.IsAttached)
                    {
                        return;
                    }
                }
            }


            #endregion

            Helper.WorkPool n = new Helper.WorkPool();

            n.IsThreaded = !Debugger.IsAttached && !sinfo.Options.IsNoThreads;

            using (new Helper.ConsoleStopper("actionscript type compiler"))
            {
                n.ForEach(xw.Session.Types,
                    delegate(Type xx)
                    {
                        if (xx.IsEnum) return;

                        CompilerBase c = new Languages.ActionScript.ActionScriptCompiler(new StringWriter(), xw.Session);

                        c.CurrentJob = j;

                        Program.AttachXMLDoc(new FileInfo(xx.Assembly.ManifestModule.FullyQualifiedName), c);

                        if (c.CompileType(xx))
                        {
                            c.ToConsole(xx, sinfo);

                            Program.WriteSingleScriptFile(Languages.ActionScript.ActionScriptCompiler.FileExtension, TargetDirectory, c, xx);
                        }
                    }
                );

                // Languages.CompilerJob.InvokeEntryPoints(TargetDirectory, j.AssamblyInfo);
            }

            if (!Debugger.IsAttached)
            {
                StreamWriter SVW = new StreamWriter(SourceVersion.OpenWrite());

                SVW.WriteLine("ConstantCompilerBuildDate: " + CompilerBase.ConstantCompilerBuildDate);
                SVW.WriteLine("AssamblyFile.LastWriteTime: " + j.AssamblyFile.LastWriteTime);
                SVW.WriteLine("SourceVersion.LastWriteTime: " + SourceVersion.LastWriteTime);

                SVW.Close();
            }
        }

    }
}
