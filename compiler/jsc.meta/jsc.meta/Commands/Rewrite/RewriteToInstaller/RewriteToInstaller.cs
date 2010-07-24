using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using jsc.meta.Commands.Rewrite.RewriteToInstaller.Templates;
using jsc.meta.Library;
using ScriptCoreLib.Archive.ZIP;
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.Extensions;
using System.IO.Compression;

namespace jsc.meta.Commands.Rewrite.RewriteToInstaller
{
    public partial class RewriteToInstaller : CommandBase
    {



        public override void Invoke()
        {
            if (this.AttachDebugger)
                Debugger.Launch();

            if (Feature == null)
            {
                var jsc = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.Parent;

                var zip = new ZIPFile();

                var bin = new DirectoryInfo(Path.Combine(jsc.FullName, "bin"));
                foreach (var item in bin.GetFilesByPattern("*.exe", "*.dll", "*.xml", "*.rtf"))
                {
                    zip.Add(item.FullName.Substring(jsc.FullName.Length + 1), File.ReadAllBytes(item.FullName));
                }

                var lib = new DirectoryInfo(Path.Combine(jsc.FullName, "lib"));
                foreach (var item in lib.GetFilesByPattern("*.exe", "*.dll", "*.xml"))
                {
                    zip.Add(item.FullName.Substring(jsc.FullName.Length + 1), File.ReadAllBytes(item.FullName));
                }

                var templates = new DirectoryInfo(Path.Combine(jsc.FullName, "templates"));
                foreach (var item in templates.GetAllFilesByPattern("*.zip"))
                {
                    zip.Add(item.FullName.Substring(jsc.FullName.Length + 1), File.ReadAllBytes(item.FullName));
                }

                var cache = new DirectoryInfo(Path.Combine(jsc.FullName, "cache"));
                foreach (var item in cache.GetAllFilesByPattern("*.zip"))
                {
                    zip.Add(item.FullName.Substring(jsc.FullName.Length + 1), File.ReadAllBytes(item.FullName));
                }

                // http://www.theregister.co.uk/2007/04/23/vista_program_naming_oddness/

                var name1_zip = DateTime.Now.ToString("yyyyMMdd") + "_jsc.zip";
                var name2_zip = "latest_jsc.zip";

                var name1 = DateTime.Now.ToString("yyyyMMdd") + "_jsc.installer";
                var name_zip = name1 + ".zip";
                var name2 = "latest_jsc.installer";

                Console.WriteLine(name1);


                // create an installer now!

                var r = default(RewriteToAssembly);

                r = new RewriteToAssembly
                {
                    staging = jsc,

                    product = name2,
                    productExtension = ".exe",

                    // does it work? :)
                    obfuscate = Obfuscate,

                    merge = new RewriteToAssembly.MergeInstruction[] { "ScriptCoreLib.Archive.ZIP" },

                    OutputStrongNameKeyPair = this.OutputStrongNameKeyPair,

                    PostAssemblyRewrite =
                        a =>
                        {
                            var AssetPath = "assets/jsc/" + name_zip;

                            var Content = new MemoryStream(zip.ToBytes());
                            var ContentCompressedMemory = new MemoryStream();
                            var ContentCompressed = new GZipStream(ContentCompressedMemory, CompressionMode.Compress);

                            Content.WriteTo(ContentCompressed);


                            a.Module.DefineManifestResource(
                                InstallerArchive.ResourceName,
                                ContentCompressedMemory,
                                System.Reflection.ResourceAttributes.Public
                            );


                            a.Assembly.SetCustomAttribute(
                                typeof(AssemblyFileVersionAttribute).GetConstructors().Single(),
                                "1.0.0.0"
                            );

                            a.Assembly.SetEntryPoint(
                                a.context.MethodCache[((Action<string[]>)Installer.Install).Method]
                            );
                        }


                };
      
                r.Invoke();

                if (this.Splash != null)
                {
                    this.Splash.PrimaryAssembly = r.Output;
                    this.Splash.OutputStrongNameKeyPair = this.OutputStrongNameKeyPair;
                    this.Splash.Invoke();
                }

                // copy with the date prefix
                r.Output.CopyTo(
                    Path.Combine(
                        jsc.FullName,
                        name1 + ".exe"
                    ), true
                );

                var exe_zip = new ZIPFile
				{
					{ r.Output.Name, File.ReadAllBytes(r.Output.FullName)}
				};

                //File.WriteAllBytes(

                //    Path.Combine(jsc.FullName,
                //        name_zip
                //    ),

                //    exe_zip.ToBytes()
                //);


                //File.WriteAllBytes(
                //    Path.Combine(jsc.FullName, name1_zip),
                //    zip.ToBytes()
                //);

                File.WriteAllBytes(
                    Path.Combine(jsc.FullName, name2_zip),
                    zip.ToBytes()
                );

            }
        }


    }

    namespace Templates
    {
        public static class InstallerArchive
        {
            public const string ResourceName = RewriteToInstaller.jsc_installer_zip + ".gz";
        }

        public class Installer
        {
            // shall be a commandline argument
            public static DirectoryInfo SDK = new DirectoryInfo(@"c:\util\jsc");


            public static ZIPFile Archive
            {
                get
                {
                    var ContentCompressedMemory = typeof(Installer).Assembly.GetManifestResourceStream(InstallerArchive.ResourceName);

                    var Content = new MemoryStream();

                    var Decompress = new GZipStream(ContentCompressedMemory, CompressionMode.Decompress);

                    //Copy the decompression stream into the output file.
                    byte[] buffer = new byte[4096];
                    int numRead;
                    while ((numRead = Decompress.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        Content.Write(buffer, 0, numRead);
                    }

                    Content.Position = 0;

                    return Content;
                }
            }


            public static void Install(string[] e)
            {
                if (e == null)
                    return;

                new Installer().Invoke();
            }

            public class FileMonkey
            {
                // The only way you could do it with a prerequisite msi is if the user uninstalled that as well as the ClickOnce application.
                // http://social.msdn.microsoft.com/Forums/en-US/winformssetup/thread/a5985a03-77b0-4915-ac08-c70d4343f6d7

                public Dictionary<string, byte[]> files;

                public FileMonkey()
                {
                    //var zip = new FileInfo(Path.ChangeExtension(new FileInfo(typeof(Installer).Assembly.Location).FullName, ".zip"));
                    files = new Dictionary<string, byte[]>();
                    var a = Archive;

                    //files[zip.FullName] = a.ToBytes();

                    var MyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    var ApplicationData = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)).CreateSubdirectory("jsc");


                    foreach (var bin in a.Entries.Where(k => k.FileName.StartsWith("bin/")))
                    {
                        files[Path.Combine(SDK.FullName, bin.FileName)] = bin.Bytes;
                    }

                    // note lib is to be deprecated with ultra
                    foreach (var lib in a.Entries.Where(k => k.FileName.StartsWith("lib/")))
                    {
                        files[Path.Combine(SDK.FullName, lib.FileName)] = lib.Bytes;
                    }

                    foreach (var lib in a.Entries.Where(k => k.FileName.StartsWith("cache/")))
                    {
                        files[Path.Combine(ApplicationData.FullName, lib.FileName)] = lib.Bytes;
                    }

                    foreach (var template in a.Entries.Where(k => k.FileName.StartsWith("templates/")))
                    {
                        files[Path.Combine(SDK.FullName, template.FileName)] = template.Bytes;

                        var file = template.FileName.SkipUntilIfAny("/");
                        var mvs = file.TakeUntilIfAny("/");

                        var p = Path.Combine(MyDocuments, mvs);
                        if (Directory.Exists(p))
                        {
                            files[Path.Combine(MyDocuments, file)] = template.Bytes;
                        }
                    }
                }
            }

            public void Invoke()
            {
                // http://notgartner.wordpress.com/2010/03/04/what-does-a-finished-product-look-like/


                ShowHeaders();

                var m = new FileMonkey();

                Display(m.files);

                Console.WriteLine();
                Console.WriteLine("Do you want to install jsc? [y/n]");

                if (Console.ReadKey(true).KeyChar != 'y')
                    return;

                Continue(m.files, true);
            }

            private static void ShowHeaders()
            {
                Console.Title = "http://jsc-solutions.net";
                Console.WriteLine("Welcome to jsc installer!");
                Console.WriteLine("For more information please visit http://jsc-solutions.net");

                Console.WriteLine();
                Console.WriteLine("The following files will be created:");
                Console.WriteLine();
            }

            public static void Continue(Dictionary<string, byte[]> files, bool IsVerbose)
            {
                var Compiler = default(FileInfo);

                if (IsVerbose)
                    Console.WriteLine();
                foreach (var f in files)
                {
                    if (IsVerbose)
                        Console.Write(".");

                    new FileInfo(f.Key).Directory.Create();

                    File.WriteAllBytes(f.Key, f.Value);

                    if (Path.GetFileName(f.Key) == CompilerName)
                    {
                        Compiler = new FileInfo(f.Key);
                    }
                }

                if (IsVerbose)
                    Console.WriteLine();

                if (IsVerbose)
                {
                    Console.WriteLine("Thank you for installing jsc!");
                }

                var p = Process.Start(
                    new ProcessStartInfo(Compiler.FullName, ConfigurationInitialize)
                    {
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                );

                p.PriorityClass = ProcessPriorityClass.Idle;
            }

            public const string CompilerName = "jsc.meta.exe";
            public const string ConfigurationInitialize = "ConfigurationInitialize";

            private static void Display(Dictionary<string, byte[]> files)
            {
                var q = from k in files
                        let f = new FileInfo(k.Key)
                        orderby f.Name
                        group new { f, k.Value } by f.Directory.FullName;

                foreach (var g in q)
                {

                    Console.WriteLine();
                    Console.WriteLine(g.Key);

                    foreach (var gf in g)
                    {
                        Console.WriteLine("\t" + gf.f.Name);
                    }
                }
            }

            public static void AsService()
            {

            }
        }
    }
}
