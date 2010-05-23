using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.CSharp.Extensions;
using System.Linq;
using jsc.Script;
using ScriptCoreLib.Ultra.Library;
using ScriptCoreLib.Extensions;

namespace jsc.Languages
{


    public partial class CompilerJob
    {

        private static void CompileActionScript(CompilerJob j, CompileSessionInfo sinfo)
        {
            DirectoryInfo TargetDirectory = sinfo.Options.TargetAssembly.Directory.CreateSubdirectory("web");

            var VersionLookup = sinfo.Options.CachedFileGeneratorConstructor(
                   new CachedFileGeneratorBase.Arguments
                   {
                       TargetDirectory = TargetDirectory,
                       AssamblyFile = new FileInfo(j.AssamblyInfo.Location),
                       Language = ScriptType.ActionScript
                   }
               );

            if (VersionLookup.Validate())
            {
                Console.WriteLine("this version is already built: " + VersionLookup.SourceVersion.Name);

                return;
            }



            IdentWriter xw = new IdentWriter();

            xw.Session = new AssamblyTypeInfo();
            xw.Session.Options = sinfo.Options;

            //sinfo.Logging.LogMessage("loading types");

            Type[] alltypes = CompilerJob.LoadTypesFromReferencedAssemblies(ScriptType.ActionScript, Assembly.LoadFile(sinfo.Options.TargetAssembly.FullName));

            // ?
            var __CommonExtensions = alltypes.Where(k => k.FullName.Contains("CommonExtensions")).ToArray();


            xw.Session.Types = /*JustMyCodeFilter(sinfo.Options.JustMyCode,*/ alltypes/*, j.AssamblyInfo)*/;

            xw.Session.ImplementationTypes.AddRange(alltypes);

            //sinfo.Logging.LogMessage("found {0} types to be compiled", xw.Session.Types.Length);


            // assets
            foreach (Assembly v in SharedHelper.LoadReferencedAssemblies(Assembly.LoadFile(sinfo.Options.TargetAssembly.FullName), true))
            {
                EmbeddedResourcesExtensions.ExtractEmbeddedResources(TargetDirectory, v);
            }



            Helper.WorkPool n = new Helper.WorkPool();

            //n.IsThreaded = !Debugger.IsAttached && !sinfo.Options.IsNoThreads;
            xw.Session.Types.WithEach(
                delegate(Type xx)
                {
                    if (xx.IsEnum) return;

                    if (xx.Assembly != j.AssamblyInfo)
                        return;

                    CompilerBase c = new Languages.ActionScript.ActionScriptCompiler(new StringWriter(), xw.Session);

                    c.CurrentJob = j;

                    //Program.AttachXMLDoc(new FileInfo(xx.Assembly.ManifestModule.FullyQualifiedName), c);

                    if (c.CompileType(xx))
                    {

                        c.ToConsole(xx, sinfo);


                        c.MyWriter.Flush();

                        var Namespace__ = string.Join(
                            "/",

                            new[] {
                                TargetDirectory.FullName
                            }.Concat(
                                c.NamespaceFixup(xx.Namespace, xx).Split('.').Select(
                                    k => c.GetSafeLiteral(k)
                                )
                            ).ToArray()
                        );

                        VersionLookup.Add(
                             Namespace__ + "/" + c.GetTypeNameForFilename(xx) + ".as",
                            c.MyWriter.ToString()
                        );
                    }
                }
            );

            Languages.ActionScript.EntryPointProvider.WriteEntryPoints(j.AssamblyInfo, TargetDirectory, xw.Session.Types);

            VersionLookup.WriteTokens();
        }

    }
}
