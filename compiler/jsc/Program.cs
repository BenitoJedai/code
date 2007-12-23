using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.IO;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Xml;
using System.Threading;
using System.Security.Policy;
using System.Resources;


using ScriptCoreLib;

using System.Drawing.Imaging;
using System.Drawing;

namespace jsc
{
    using Script;
    using jsc.Languages.JavaScript;


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

            Assembly a = Assembly.GetExecutingAssembly();

            try
            {
                //ExpireVersion();

                if (!options.EnableRemoteExecution)
                    DisableRemoteExecution();

                //Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.BelowNormal;
                //Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;


                //AppDomain.CurrentDomain.AppendPrivatePath( options.TargetAssembly.DirectoryName );


                AppDomain.CurrentDomain.AssemblyResolve += delegate(object sender, ResolveEventArgs args)
                {

                    string name = new AssemblyName(args.Name).Name;


                    string file = options.TargetAssembly.DirectoryName + @"\" + name + ".dll";

                    Assembly x = File.Exists(file) ? Assembly.LoadFile(file) : null;

                    return x;
                };


                Console.WriteLine("Current Path: " + Environment.CurrentDirectory);
                //sinfo.Logging.LogMessage("Current Search Path: " + searchpath);

                if (options.ShowReferences)
                {
                    DoShowReferences(options);
                }

                if (options.ExtractAssets)
                {
                    DirectoryInfo TargetDirectory = options.TargetAssembly.Directory.CreateSubdirectory("web");

                    foreach (Assembly v in SharedHelper.LoadReferencedAssemblies(Assembly.LoadFile(options.TargetAssembly.FullName), true))
                    {

                        Languages.CompilerJob.ExtractEmbeddedResources(TargetDirectory, v);

                    }

                }

                try
                {
                    ExtractResourcesStreams(options);

                    if (options.IsNoThreads)
                    {
                        sinfo.Logging.LogMessage("Threads will be disabled");
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
                            if (options.IsJavascript)
                                ConvertAssamblySpawned(v, ScriptType.JavaScript, sinfo);

                            if (options.IsPHP)
                                ConvertAssamblySpawned(v, ScriptType.PHP, sinfo);

                            if (options.IsJava)
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
                    Task.Error("internal compiler failure: " + exc + "\n\n" + exc.StackTrace);

                }

                //if (Debugger.IsAttached)
                //{
                //    Console.WriteLine("press any key!");
                //    Console.ReadKey(true);

                //}
            }
            catch
            {
                Console.WriteLine(" *** FATAL ERROR");
            }
        }

        private static void DoShowReferences(CommandLineOptions options)
        {
            foreach (Assembly v in SharedHelper.LoadReferencedAssemblies(Assembly.LoadFile(options.TargetAssembly.FullName), true))
            {
                Console.WriteLine("assembly: " + v.CodeBase);
            }
        }

        private static void ExtractResourcesStreams(CommandLineOptions options)
        {
            foreach (Assembly v in SharedHelper.LoadReferencedAssemblies(Assembly.LoadFile(options.TargetAssembly.FullName), true))
            {
                Console.WriteLine("assembly: " + v.CodeBase);

                foreach (string vname in v.GetManifestResourceNames())
                {
                    if (vname.EndsWith(".resources"))
                        try
                        {

                            // http://blogs.clearscreen.com/kartones/archive/2007/03/24/6240.aspx

                            ResourceSet rs = new ResourceSet(v.GetManifestResourceStream(vname));

                            foreach (DictionaryEntry vx in rs)
                            {
                                if (vx.Value is System.Drawing.Bitmap)
                                {
                                    using (Bitmap img = (System.Drawing.Bitmap)vx.Value)
                                    {

                                        DirectoryInfo dir = options.TargetAssembly.Directory;
                                        DirectoryInfo assets = dir.
                                            CreateSubdirectory("web").
                                            CreateSubdirectory("assets").
                                            CreateSubdirectory(options.TargetAssembly.Name).
                                            CreateSubdirectory(vname);


                                        string target = null;

                                        if (img.RawFormat.Guid.Equals(ImageFormat.Gif.Guid))
                                            target = assets.FullName + "/" + vx.Key.ToString() + ".gif";
                                        else if (img.RawFormat.Guid.Equals(ImageFormat.Png.Guid))
                                            target = assets.FullName + "/" + vx.Key.ToString() + ".png";
                                        else if (img.RawFormat.Guid.Equals(ImageFormat.Jpeg.Guid))
                                            target = assets.FullName + "/" + vx.Key.ToString() + ".jpg";

                                        if (target != null)
                                        {
                                            img.Save(target);


                                            Console.WriteLine("res: " + target);
                                        }

                                    }
                                    // web/assets/FormsExample.Properties.Resources.resources/cal.png
                                }

                            }
                        }
                        catch
                        {

                        }



                }

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
            xw.Session.Types = ScriptAttribute.FindTypes(_assambly_loaded, type);

            if (xw.Session.Types.Length == 0)
            {
                Console.WriteLine("zero types found to compile, skipping");

                return;
            }

            FileInfo _target_file = new FileInfo(TargetDirectory.FullName + "/" + TargetFileName);

            // the dll
            FileInfo TargetAssamblyFile = new FileInfo(target_assambly);


            xw.Session.ImplementationTypes.AddRange(xw.Session.Types);

            LoadReferencedAssamblies(type, ta, xw, _assambly_loaded);

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
                /*
                #region javascript packer (*.packed.js)
                bool bPackJS = type == ScriptType.JavaScript;

                if (bPackJS)
                {
                    // trim down

                    ECMAScriptPacker p = new ECMAScriptPacker(ECMAScriptPacker.PackerEncoding.None, true, false);

                    Task.WriteLine("packing...");

                    StreamWriter sw = new StreamWriter(new FileStream(TargetFile + ".packed.js", FileMode.Create));

                    string packed = p.Pack(xw.ToString());

                    sw.Write(packed);
                    sw.Flush();

                    sw.Close();

                    //if (AssamblyS.IsCoreLib)
                    //{
                    //    jsc.Helper.WriteFile(TargetFile, packed);

                    //    goto corelib_protection;

                    //}
                }

                #endregion
                */

                xw.ToFile(TargetFile);
            }

        corelib_protection:

            StreamWriter SVW = new StreamWriter(SourceVersion.OpenWrite());

            SVW.WriteLine(CompilerBase.ConstantCompilerBuildDate);
            SVW.Close();


            Languages.CompilerJob.ExtractEmbeddedResources(TargetDirectory, _assambly_loaded);
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
                ScriptFileExtension = Script.Java.JavaCompiler.FileExtension;
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



        public static void WriteJavaFile(string ScriptFileExtension, DirectoryInfo SourceDir, CompilerBase c, Type x)
        {


            DirectoryInfo p = SourceDir;

            string _ns = c.NamespaceFixup(x.Namespace);


            if (_ns != null)
            {
                string[] n = _ns.Split('.');

                for (int i = 0; i < n.Length; i++)
                    p = p.CreateSubdirectory(n[i]);
            }

            string content = c.MyWriter.ToString();


            StreamWriter sw = new StreamWriter(new FileStream(p.FullName + "/" + c.GetDecoratedTypeNameWithinNestedName(x) + "." + ScriptFileExtension, FileMode.Create));

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