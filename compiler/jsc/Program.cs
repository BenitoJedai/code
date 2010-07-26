using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using jsc.Languages;
using jsc.Languages.JavaScript;
using jsc.Loader;
using ScriptCoreLib;
using ScriptCoreLib.CSharp.Extensions;
using jsc.Script;
using System.Threading;
using jsc.Library;
using ScriptCoreLib.Ultra.Library;

namespace jsc
{



    public class Program
    {


        // packed? http://dean.edwards.name/packer/

        static void ShowLogo()
        {
            //Microsoft (R) Visual C# 2005 Compiler version 8.00.50727.42
            //for Microsoft (R) Windows (R) 2005 Framework version 2.0.50727
            //Copyright (C) Microsoft Corporation 2001-2005. All rights reserved.


            Console.WriteLine(((AssemblyDescriptionAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0]).Description);
            Console.WriteLine(((AssemblyCompanyAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false)[0]).Company);
            Console.WriteLine(((AssemblyCopyrightAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright);
            Console.WriteLine();
        }

        public static void Main(string[] args)
        {
            CompileSessionInfo a = new CompileSessionInfo();

            a.Options = new CommandLineOptions(args);

            TypedMain(a);

            //Thread.Sleep(3000);
        }

        public static void TypedMain(CompileSessionInfo sinfo)
        {
            CheckForUpdates();

            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);
            CommandLineOptions options = sinfo.Options;

            if (options.IsAttachDebugger)
                Debugger.Launch();

            if (!options.IsNoLogo || options.IsHelp)
                ShowLogo();

            //Console.WriteLine("ImageRuntimeVersion: " + Assembly.GetExecutingAssembly().ImageRuntimeVersion);
            Console.WriteLine("CLR: " + typeof(object).Assembly.ImageRuntimeVersion);
            Console.WriteLine();


            if (options.IsHelp)
            {
                options.ShowHelp();

                return;
            }


            /*
            if (options.IsTrace)
            {
                sinfo.Logging.LogMessage("Trace mode enabled. Attach debugger now.");
                Debugger.Launch();
            }
            */

            if (options.TargetAssembly == null)
            {
                Console.WriteLine("No assembly specifed. See help for more details.");
                return;
            }

            if (!options.TargetAssembly.Exists)
            {
                Console.WriteLine("File not found {0}.", options.TargetAssembly);
                return;
            }

            Assembly a = Assembly.GetExecutingAssembly();

            try
            {
                //ExpireVersion();

                if (!options.EnableRemoteExecution)
                    DisableRemoteExecution();

                //Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.BelowNormal;
                //Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;


                //AppDomain.CurrentDomain.AppendPrivatePath( options.TargetAssembly.DirectoryName );


                //AppDomain.CurrentDomain.AssemblyLoad +=
                //    (object sender, AssemblyLoadEventArgs args) =>
                //    {
                //        Console.WriteLine("AssemblyLoad: " + args.LoadedAssembly.Location);
                //    };

                AppDomain.CurrentDomain.AssemblyResolve += delegate(object sender, ResolveEventArgs args)
                {
                    // fixme: to be merged with LoaderStrategy
                    string name = new AssemblyName(args.Name).Name;


                    string file_dll = options.TargetAssembly.DirectoryName + @"\" + name + ".dll";
                    string file_exe = options.TargetAssembly.DirectoryName + @"\" + name + ".exe";

                    //Console.WriteLine("looking for :" + file_dll);
                    //Console.WriteLine("looking for :" + file_exe);

                    var x = File.Exists(file_dll) ? Assembly.LoadFile(file_dll) :
                                    (File.Exists(file_exe) ? Assembly.LoadFile(file_exe) : null);

                    return x;
                };


                Console.WriteLine("Current Path: " + Environment.CurrentDirectory);
                //sinfo.Logging.LogMessage("Current Search Path: " + searchpath);

                if (options.ShowReferences)
                {
                    DoShowReferences(options);
                }

                #region ExtractAssets
                if (options.ExtractAssets)
                {

                    DirectoryInfo TargetDirectory = options.TargetAssembly.Directory.CreateSubdirectory("web");

                    var References = LoaderStrategy.LoadReferencedAssemblies(
                        Assembly.LoadFile(options.TargetAssembly.FullName),
                        options.ToScriptTypes().ToArray()
                    ).Reverse().Distinct().ToArray();


                    foreach (Assembly v in References)
                    //foreach (Assembly v in SharedHelper.LoadReferencedAssemblies(Assembly.LoadFile(options.TargetAssembly.FullName), true))
                    {
                        EmbeddedResourcesExtensions.ExtractEmbeddedResources(TargetDirectory, v);
                    }
                }
                #endregion

                try
                {
                    #region detection

                    var References = LoaderStrategy.LoadReferencedAssemblies(
                        Assembly.LoadFile(options.TargetAssembly.FullName),
                        options.ToScriptTypes().ToArray()
                    ).Reverse().Distinct().ToArray();

                    // we need to load modules excluding unwanted assemblies
                    string[] m = References.Select(k => Path.GetFullPath(k.Location)).ToArray();

                    foreach (string CurrentReference in m)
                    {
                        Console.WriteLine(CurrentReference);

                        //ConvertAssamblySpawned(v, ScriptType.C, sinfo);

                        if (options.IsJavaScript)
                            ConvertAssamblySpawned(CurrentReference, ScriptType.JavaScript, sinfo);

                        if (options.IsPHP)
                            ConvertAssamblySpawned(CurrentReference, ScriptType.PHP, sinfo);

                        if (options.IsJava)
                            Languages.CompilerJob.Compile(CurrentReference, sinfo);

                        if (options.IsActionScript)
                            Languages.CompilerJob.Compile(CurrentReference, sinfo);

                        if (options.IsCSharp2)
                            Languages.CompilerJob.Compile(CurrentReference, sinfo);

                        if (options.IsC)
                            Languages.CompilerJob.Compile(CurrentReference, sinfo);
                    }

                    #endregion



                }
                catch (Exception exc)
                {
                    if (Debugger.IsAttached)
                        Debugger.Break();

                    throw new InvalidOperationException("internal compiler failure: " + exc + "\n\n" + exc.StackTrace);

                }

            }
            catch (Exception excc)
            {
                throw new InvalidOperationException(" *** FATAL ERROR: " + excc.Message);
            }
        }

        private static void CheckForUpdates()
        {
            new Thread(
                delegate()
                {
                    try
                    {
                        Console.WriteLine("checking for updates...");

                        Library.Analytics.AnalyticsForStatCounterImplementation.Invoke(
                            new jsc.Library.Analytics.AnalyticsForStatCounterArguments
                            {
                                assembly = "jsc",
                                // do not reuse these parameters in your applications!
                                // public stats: http://my.statcounter.com/project/standard/stats.php?project_id=5203272&guest=1
                                // http://my.statcounter.com/project/standard2/csv/download_log_file.php?project_id=5203272


                                sc_project = "5203272",
                                security = "94d6fb4a",

                            }
                        );
                    }
                    catch
                    {
                    }
                }
            ) { IsBackground = true }.Start();
        }

        private static void DoShowReferences(CommandLineOptions options)
        {
            foreach (Assembly v in
                SharedHelper.LoadReferencedAssemblies(
                    Assembly.LoadFile(options.TargetAssembly.FullName), true))
            {
                Console.WriteLine("assembly: " + v.GetName().Name + " " + v.Location);
            }
        }



        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {

        }

        private static void DisableRemoteExecution()
        {
            string acb = Assembly.GetExecutingAssembly().CodeBase;

            if (!acb.StartsWith(@"file://"))
            {
                throw new UnauthorizedAccessException("*** try local filesystem instead, " + acb);
            }
        }






        static void ConvertAssamblySpawned(string target_assambly, ScriptType type, CompileSessionInfo sinfo)
        {
            using (new ScriptAttribute.ScriptLibraryContext(Assembly.LoadFile(sinfo.Options.TargetAssembly.FullName)))
                ConvertAssamblySpawnedWithinContext(target_assambly, type, sinfo);

        }

        static void ConvertAssamblySpawnedWithinContext(string target_assambly, ScriptType type, CompileSessionInfo sinfo)
        {
            //Environment .CurrentDirectory = new FileInfo(target_assambly).Directory.FullName;

            string ta = target_assambly;


            if (File.Exists(target_assambly))
            {
                FileInfo asd = new FileInfo(target_assambly);

                target_assambly = asd.FullName;
            }
            else
            {
                Task.Error("file not found: {0}", target_assambly);
                return;
            }


            string ScriptFileExtension = GetExtensionString(type);

            string AssamblyFileName = target_assambly.Substring(target_assambly.LastIndexOf(@"\") + 1);



            Assembly _assambly_loaded = Assembly.LoadFile(target_assambly);

            DirectoryInfo TargetDirectory = sinfo.Options.TargetAssembly.Directory.CreateSubdirectory("web");

            if (!TargetDirectory.Exists)
                TargetDirectory.Create();


            var VersionLookup = sinfo.Options.CachedFileGeneratorConstructor(
                new CachedFileGeneratorBase.Arguments
                {
                    TargetDirectory = TargetDirectory,
                    AssamblyFile = new FileInfo(_assambly_loaded.Location),
                    Language = type
                }
            );

            //EmbeddedResourcesExtensions.ExtractEmbeddedResources(TargetDirectory, _assambly_loaded);

            // assets
            foreach (Assembly v in SharedHelper.LoadReferencedAssemblies(Assembly.LoadFile(sinfo.Options.TargetAssembly.FullName), true))
            {
                EmbeddedResourcesExtensions.ExtractEmbeddedResources(TargetDirectory, v);
            }

            if (VersionLookup.Validate())
            {
                Console.WriteLine("this version is already built: " + VersionLookup.SourceVersion.Name);

                return;
            }




            //#region allow only assamblies with script attribute
            //if (ScriptAttribute.OfProvider(_assambly_loaded) == null)
            //{
            //    Console.WriteLine("*** asambly not prepared for script compiler");

            //    return;
            //}
            //#endregion


            bool WillWriteFile = true;

            IdentWriter xw = new IdentWriter();

            xw.Session = new AssamblyTypeInfo();
            xw.Session.Options = sinfo.Options;


            #region loading types
            xw.Session.Types = CompilerJob.LoadTypesFromReferencedAssemblies(type, Assembly.LoadFile(sinfo.Options.TargetAssembly.FullName));

            //xw.Session.Types = ScriptAttribute.FindTypes(_assambly_loaded, type);

            if (xw.Session.Types.Length == 0)
            {
                Console.WriteLine("zero types found to compile, skipping");

                return;
            }

            //Type[] alltypes = CompilerJob.LoadTypes(type, Assembly.LoadFile(sinfo.Options.TargetAssembly.FullName));

            xw.Session.ImplementationTypes.AddRange(xw.Session.Types);

            LoadReferencedAssamblies(type, ta, xw, _assambly_loaded);
            #endregion


            string TargetFileName = AssamblyFileName + "." + ScriptFileExtension;


            // the dll
            FileInfo TargetAssamblyFile = new FileInfo(target_assambly);






            var AssamblyS = ScriptAttribute.OfProvider(_assambly_loaded);


            //WriteAssamblyAttributes(target_assambly, xw, _assambly_loaded);


            if (type == ScriptType.PHP)
            {

                new jsc.Script.PHP.PHPCompiler(xw, xw.Session).Compile(_assambly_loaded, sinfo);
            }
            else if (type == ScriptType.JavaScript)
            {
                IL2Script.DeclareTypes(
                    xw,
                    xw.Session.Types.Where(k => k.Assembly == _assambly_loaded).ToArray(),
                    false,
                    AssamblyS,
                    _assambly_loaded,
                    sinfo
                );

                _assambly_loaded.WriteEntryPoints(TargetDirectory);
            }



            if (WillWriteFile)
            {
                xw.Flush();

                // currently this will be called only for javascript?
                VersionLookup.Add(
                    TargetDirectory.FullName + "/" + TargetFileName,
                    xw.ToString()
                );
            }


            VersionLookup.WriteTokens();


            // to be removed at some point
            // will we break anything yet? :)
            // old examples maybe :)

            Languages.CompilerJob.InvokeEntryPoints(TargetDirectory, _assambly_loaded);

        }


        public static void AttachXMLDoc(FileInfo TargetAssamblyFile, CompilerBase c)
        {
            FileInfo XmlDocumentInfo = new FileInfo(TargetAssamblyFile.Name.Substring(0, TargetAssamblyFile.Name.LastIndexOf(".")) + ".xml");

            #region doc
            if (XmlDocumentInfo.Exists)
            {
                c.XmlDoc = new XmlDocument();
                c.XmlDoc.Load(XmlDocumentInfo.FullName);

                //Console.WriteLine("*** xmldoc at " + XmlDocumentInfo.FullName);
            }
            else
            {
                //WarningNoXMLDoc(XmlDocumentInfo);
            }
            #endregion
        }



        private static string GetExtensionString(ScriptType type)
        {
            string ScriptFileExtension = null;

            if (type == ScriptType.JavaScript)
                ScriptFileExtension = "js";
            else if (type == ScriptType.C)
                ScriptFileExtension = "c";
            else if (type == ScriptType.PHP)
                ScriptFileExtension = Script.PHP.PHPCompiler.FileExtension;
            else if (type == ScriptType.Java)
                ScriptFileExtension = Languages.Java.JavaCompiler.FileExtension;
            else if (type == ScriptType.ActionScript)
                ScriptFileExtension = Languages.ActionScript.ActionScriptCompiler.FileExtension;
            else
                Script.CompilerBase.BreakToDebugger("language not detected");

            return ScriptFileExtension;
        }

        //private static void WarningNoXMLDoc(FileInfo XmlDocumentInfo)
        //{
        //    jsc.Script.CompilerBase.WriteVisualStudioMessage(
        //        CompilerBase.MessageType.warning,
        //        101,
        //        "no xml doc at " + XmlDocumentInfo.FullName
        //    );
        //}



        public static void WriteSingleScriptFile(string ScriptFileExtension, DirectoryInfo SourceDir, CompilerBase c, Type x)
        {
            //  System.IO.PathTooLongException
            // yay, thanks BCL.

            string p = SourceDir.FullName;

            string _ns = c.NamespaceFixup(x.Namespace, x);


            if (!string.IsNullOrEmpty(_ns))
            {
                string[] n = _ns.Split('.');

                // http://blogs.msdn.com/jnak/archive/2010/01/14/windows-azure-path-too-long.aspx
                for (int i = 0; i < n.Length; i++)
                {
                    var nn = CompilerBase.GetSafeLiteral(n[i], null);

                    p = p + "/" + nn;

                    // http://www.experts-exchange.com/Programming/System/Windows__Programming/MFC/Q_21554390.html
                    Win32File.CreateDirectory(p);
                }
            }

            string content = c.MyWriter.ToString();



            content.WriteToFile(p + "/" + c.GetTypeNameForFilename(x) + "." + ScriptFileExtension);

        }

        private static bool IsAssamblyUptodate(FileInfo f0, FileInfo f1)
        {
            // should check for dependencies?

            bool bUpToDate = false;

            if (!f0.Exists || f1.Exists)
                return false;

            if (f0.LastWriteTime > f1.LastWriteTime)
            {
                if (!Debugger.IsAttached)
                {
                    Console.WriteLine("already up to date {0}, wont update {1}", f1.LastWriteTime, f0.FullName);
                    bUpToDate = true;
                }
            }
            else
            {
                Console.WriteLine("will update {0} [{1}] : {2} [{3}]", f0.Name, f0.LastWriteTime, f1.Name, f1.LastWriteTime);

            }
            return bUpToDate;
        }

        private static void LoadReferencedAssamblies(ScriptType type, string ta, IdentWriter xw, Assembly _assambly_loaded)
        {
            foreach (Assembly xa in SharedHelper.LoadReferencedAssemblies(_assambly_loaded, false))
            {
                ScriptAttribute sa = ScriptAttribute.OfProvider(xa);

                if (sa == null)
                    continue;

                Type[] impl = ScriptAttribute.FindTypes(xa, type);

                //Console.WriteLine("reference: " + x.FullName + " with " + impl.Length + " type");

                xw.Session.ImplementationTypes.AddRange(impl);
            }


        }








    }
}