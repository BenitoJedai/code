using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Resources;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Threading;
using System.Xml;
using ScriptCoreLib;

namespace jsc
{
	using jsc.Languages.JavaScript;
	using Script;
	using ScriptCoreLib.CSharp.Extensions;


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

            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);
            CommandLineOptions options = sinfo.Options;

            if (!options.IsNoLogo || options.IsHelp)
                ShowLogo();

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

                    foreach (Assembly v in SharedHelper.LoadReferencedAssemblies(Assembly.LoadFile(options.TargetAssembly.FullName), true))
                    {
						EmbeddedResourcesExtensions.ExtractEmbeddedResources(TargetDirectory, v);
                    }
                }
                #endregion

                try
                {
                    // ExtractResourcesStreams(options);

                    if (options.IsNoThreads)
                    {
                        /// sinfo.Logging.LogMessage("Threads will be disabled");
                    }

                    #region detection

                    if (options.IsSmart)
                    {
                        ConvertAssamblySpawned(options.TargetAssembly.FullName, ScriptType.C, sinfo);
                        ConvertAssamblySpawned(options.TargetAssembly.FullName, ScriptType.JavaScript, sinfo);
                        ConvertAssamblySpawned(options.TargetAssembly.FullName, ScriptType.PHP, sinfo);

                        Languages.CompilerJob.Compile(sinfo.Options.TargetAssembly.FullName, sinfo);
                    }
                    else if (options.IsAllModulesAllLanguages)
                    {

                        string[] m = SharedHelper.ModulesOf(
                            Assembly.LoadFile(
                                options.TargetAssembly.FullName
                                )
                            );


                        foreach (string v in m)
                        {
                            ConvertAssamblySpawned(v, ScriptType.C, sinfo);
                            ConvertAssamblySpawned(v, ScriptType.JavaScript, sinfo);
                            ConvertAssamblySpawned(v, ScriptType.PHP, sinfo);

                            Languages.CompilerJob.Compile(v, sinfo);
                        }


                    }
                    else if (options.IsJavaOnly)
                    {
                        Languages.CompilerJob.Compile(options.TargetAssembly.FullName, sinfo);
                    }
                    else if (options.Trim)
                    {
                        if (options.IsJavaScript)
                            ConvertAssamblySpawned(options.TargetAssembly.FullName, ScriptType.JavaScript, sinfo);

                    }
                    else
                    {

                        string[] m = SharedHelper.ModulesOf(
                                 Assembly.LoadFile(
                                     options.TargetAssembly.FullName
                                     )
                                 );


                        foreach (string v in m)
                        {
                            //ConvertAssamblySpawned(v, ScriptType.C, sinfo);

                            if (options.IsJavaScript)
                                ConvertAssamblySpawned(v, ScriptType.JavaScript, sinfo);

                            if (options.IsPHP)
                                ConvertAssamblySpawned(v, ScriptType.PHP, sinfo);

                            if (options.IsJava)
                                Languages.CompilerJob.Compile(v, sinfo);

                            if (options.IsActionScript)
                                Languages.CompilerJob.Compile(v, sinfo);

                            if (options.IsCSharp2)
                                Languages.CompilerJob.Compile(v, sinfo);
                        }

                        //ScriptType t = ScriptType.Unknown;

                        //if (options.IsJavascript)
                        //    t = ScriptType.JavaScript;

                        //if (options.IsPHP)
                        //    t = ScriptType.PHP;

                        //if (options.IsJava)
                        //    t = ScriptType.Java;



                        //ConvertAssambly(options.TargetAssembly.FullName, t,  sinfo);
                    }
                    #endregion



                }
                catch (Exception exc)
                {
					if (Debugger.IsAttached)
						Debugger.Break();

                    Task.Error("internal compiler failure: " + exc + "\n\n" + exc.StackTrace);

                }

            }
            catch (Exception excc)
            {
                Console.WriteLine(" *** FATAL ERROR: " + excc.Message);
            }
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




        static Thread ConvertAssamblyThreaded(string target_assambly, ScriptType type, CompileSessionInfo sinfo)
        {
            Thread u = new Thread(
                (ThreadStart)
                delegate
                {
                    ConvertAssamblySpawned(target_assambly, type, sinfo);
                }
                );

            u.Priority = ThreadPriority.BelowNormal;
            u.Name = "compiler: " + target_assambly + " type:" + Enum.GetName(typeof(ScriptType), type);

            u.Start();


            return u;
        }

        static void ConvertAssambly(string target_assambly, ScriptType type, CompileSessionInfo sinfo)
        {
            Thread u = ConvertAssamblyThreaded(target_assambly, type, sinfo);

            if (Debugger.IsAttached)
                u.Join();
            else if (!u.Join(new TimeSpan(0, 0, 20)))
            {
                throw new NotSupportedException("*** operation is taking too long, aborting...");
            }


        }

        

        static void ConvertAssamblySpawned(string target_assambly, ScriptType type, CompileSessionInfo sinfo)
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

            string TargetFileName = AssamblyFileName + "." + ScriptFileExtension;


            DirectoryInfo TargetDirectory = new DirectoryInfo("web");

            if (!TargetDirectory.Exists)
                TargetDirectory.Create();

            Assembly _assambly_loaded = Assembly.LoadFile(target_assambly);



            DirectoryInfo SourceVersionDir = TargetDirectory.CreateSubdirectory("version");

            FileInfo AssamblyFile = new FileInfo(_assambly_loaded.Location);

            FileInfo SourceVersion = new FileInfo(SourceVersionDir.FullName + "/" + _assambly_loaded.ManifestModule.Name + "." + Enum.GetName(typeof(ScriptType), type) + ".version.txt");

            if (SourceVersion.Exists)
            {
                if (AssamblyFile.LastWriteTime <= SourceVersion.LastWriteTime)
                {
                    Console.WriteLine("this version is already built: " + SourceVersion.Name);

                    //if (!Debugger.IsAttached)
                    //{
                    return;
                    //}
                }
            }




            #region allow only assamblies with script attribute
            if (ScriptAttribute.OfProvider(_assambly_loaded) == null)
            {
                Console.WriteLine("*** asambly not prepared for script compiler");

                return;
            }
            #endregion


            bool WillWriteFile = true;

            IdentWriter xw = new IdentWriter();

            xw.Session = new AssamblyTypeInfo();
            xw.Session.Options = sinfo.Options;

            if (sinfo.Options.Trim)
            {
               

                throw new NotImplementedException();
            }
            else
            {
                #region loading types
                xw.Session.Types = ScriptAttribute.FindTypes(_assambly_loaded, type);

                if (xw.Session.Types.Length == 0)
                {
                    Console.WriteLine("zero types found to compile, skipping");

                    return;
                }

                xw.Session.ImplementationTypes.AddRange(xw.Session.Types);

                LoadReferencedAssamblies(type, ta, xw, _assambly_loaded);
                #endregion
            }


            FileInfo _target_file = new FileInfo(TargetDirectory.FullName + "/" + TargetFileName);

            // the dll
            FileInfo TargetAssamblyFile = new FileInfo(target_assambly);


            Console.WriteLine("will compile " + xw.Session.Types.Length + " types");




            ScriptAttribute AssamblyS = ScriptAttribute.Of(_assambly_loaded);


            //WriteAssamblyAttributes(target_assambly, xw, _assambly_loaded);


            if (type == ScriptType.PHP)
            {
                new jsc.Script.PHP.PHPCompiler(xw, xw.Session).Compile(sinfo);
            }
            else if (type == ScriptType.C)
            {
                #region c
                jsc.Languages.C.CCompiler c = new jsc.Languages.C.CCompiler(xw, xw.Session);

                AttachXMLDoc(TargetAssamblyFile, c);

                c.HeaderFileName = AssamblyFileName + ".h";

                // .c
                c.Compile();

                // .h
                c.IsHeaderOnlyMode = true;
                c.MyWriter = new StringWriter();

                c.Compile();


                StreamWriter _stream = new StreamWriter(new FileStream(TargetDirectory.FullName + "/" + c.HeaderFileName, FileMode.Create));

                _stream.Write(c.MyWriter.ToString());
                _stream.Flush();

                _stream.Close();
                #endregion

            }
            else if (type == ScriptType.JavaScript)
            {
                IL2Script.DeclareTypes(xw, xw.Session.Types, false, AssamblyS, _assambly_loaded);

                _assambly_loaded.WriteEntryPoints(TargetDirectory);
            }



            if (WillWriteFile)
            {
                xw.Flush();

                string TargetFile = TargetDirectory.FullName + "/" + TargetFileName;
               

                xw.ToFile(TargetFile);
            }

        corelib_protection:

            StreamWriter SVW = new StreamWriter(SourceVersion.OpenWrite());

            SVW.WriteLine(CompilerBase.ConstantCompilerBuildDate);
            SVW.Close();


			EmbeddedResourcesExtensions.ExtractEmbeddedResources(TargetDirectory, _assambly_loaded);
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

        private static void WarningNoXMLDoc(FileInfo XmlDocumentInfo)
        {
            jsc.Script.CompilerBase.WriteVisualStudioMessage(
                CompilerBase.MessageType.warning,
                101,
                "no xml doc at " + XmlDocumentInfo.FullName
            );
        }



        public static void WriteSingleScriptFile(string ScriptFileExtension, DirectoryInfo SourceDir, CompilerBase c, Type x)
        {


            DirectoryInfo p = SourceDir;

            string _ns = c.NamespaceFixup(x.Namespace);


            if (!string.IsNullOrEmpty(_ns))
            {
                string[] n = _ns.Split('.');

                for (int i = 0; i < n.Length; i++)
                    p = p.CreateSubdirectory(n[i]);
            }

            string content = c.MyWriter.ToString();


            StreamWriter sw = new StreamWriter(new FileStream(p.FullName + "/" + c.GetTypeNameForFilename(x) + "." + ScriptFileExtension, FileMode.Create));

            sw.Write(content);
            sw.Flush();

            sw.Close();
            
        }

        private static bool IsAssamblyUptodate(FileInfo f0, FileInfo f1)
        {
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

            //AssemblyName[] r = _assambly_loaded.GetReferencedAssemblies();

            //// should we even load?

            //foreach (AssemblyName x in r)
            //{

            //    try
            //    {

            //        FileInfo AssamblyFile = new FileInfo(x.Name + ".dll");


            //        if (!AssamblyFile.Exists)
            //            continue;

            //        Assembly xa = Assembly.LoadFile(AssamblyFile.FullName);

            //        ScriptAttribute sa = ScriptAttribute.OfProvider(xa);

            //        if (sa == null)
            //            continue;

            //        Type[] impl = ScriptAttribute.FindTypes(xa, type);

            //        //Console.WriteLine("reference: " + x.FullName + " with " + impl.Length + " type");

            //        xw.Session.ImplementationTypes.AddRange(impl);

            //    }
            //    catch
            //    {
            //        string desc = "cannot load reference " + x.FullName;

            //        Script.CompilerBase.WriteVisualStudioMessage(CompilerBase.MessageType.error, 0, desc);


            //        throw new Exception(desc);
            //    }
            //}

        }

        //private static void WriteAssamblyAttributes(string target_assambly, IL2ScriptWriter xw, Assembly _assambly_loaded)
        //{
        //    //xw.Helper.DOMAttribute("description", "jsc compiler", "http://zproxy.zapto.org/jsc");
        //    ////xw.Helper.DOMAttribute("csharp", "http://www.ecma-international.org/publications/standards/Ecma-334.htm");
        //    ////xw.Helper.DOMAttribute("javascript", "http://www.ecma-international.org/publications/standards/Ecma-262.htm");
        //    //xw.Helper.DOMAttribute("assambly", target_assambly, _assambly_loaded.ToString());
        //    //xw.Helper.DOMAttribute("environment", Environment.MachineName + "/" + Environment.UserName);
        //    xw.Helper.DOMAttribute("time", DateTime.Now.ToUniversalTime().ToString());
        //    //xw.Helper.DOMAttribute("copyright", "all rights reserved");
        //}







    }
}