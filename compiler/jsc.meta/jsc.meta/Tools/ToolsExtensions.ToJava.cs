using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using ScriptCoreLib.Ultra.Library;

namespace jsc.meta.Tools
{
    static partial class ToolsExtensions
    {
        public static void ToJava(this FileInfo TargetAssembly, DirectoryInfo javapath, MethodInfo assembly_metaentrypoint, FileInfo FusionAssembly)
        {
            ToJava(TargetAssembly, javapath, assembly_metaentrypoint, FusionAssembly, null);
        }

        public static void ToJava(this FileInfo TargetAssembly, DirectoryInfo javapath, MethodInfo assembly_metaentrypoint, FileInfo FusionAssembly, string jarname)
        {
            ToJava(TargetAssembly, javapath, assembly_metaentrypoint, FusionAssembly, jarname, null, true);
        }

        public static void ToJava(
            this FileInfo TargetAssembly,
            DirectoryInfo javapath,
            MethodInfo assembly_metaentrypoint,
            FileInfo FusionAssembly,
            string jarname,
            Type TargetType,
            bool CreateNoWindow)
        {

            // we should run jsc in another appdomain actually
            // just to be sure our nice tool gets unloaded :)

            jsc.Program.TypedMain(
                new jsc.CompileSessionInfo
                {
                    Options = new jsc.CommandLineOptions
                    {
                        CachedFileGeneratorConstructor = CachedFileGenerator.Create,
                        TargetAssembly = TargetAssembly,
                        IsJava = true,
                        IsNoLogo = true
                    }
                }
            );


            if (javapath == null)
            {
                Console.WriteLine("java path not specified");
                return;
            }

            var obj_web = Path.Combine(TargetAssembly.Directory.FullName, "web");
            var obj_web_bin = Path.Combine(obj_web, "bin");
            var bin_jar = new FileInfo(Path.Combine(obj_web_bin,
                jarname ?? (Path.GetFileNameWithoutExtension(TargetAssembly.Name) + @".jar")
            ));

            var TargetTypeFullName = (assembly_metaentrypoint == null ? TargetType : assembly_metaentrypoint.DeclaringType).FullName;


            #region javac
            Console.WriteLine("- javac");
            var TargetSourceFiles = "java";

            // each jar file which has made it to the bin folder
            // needs to get referenced in our java build
            foreach (var r in from k in Directory.GetFiles(obj_web_bin, "*.jar")
                              where k != bin_jar.FullName
                              select k)
            {
                TargetSourceFiles += ";" + Path.Combine("bin", Path.GetFileName(r));
            }

            var proccess_javac = Process.Start(
                new ProcessStartInfo(
                    Path.Combine(javapath.FullName, "javac.exe"),
                    @"-classpath " + TargetSourceFiles + @" -d release java\" + TargetTypeFullName.Replace(".", @"\").Replace("+", "_") + @".java"
                    )
                {
                    UseShellExecute = false,

                    CreateNoWindow = CreateNoWindow,

                    WorkingDirectory = obj_web
                }
            );

            proccess_javac.WaitForExit();

            if (proccess_javac.ExitCode != 0)
                throw new ArgumentOutOfRangeException();
            #endregion




            #region jar
            Console.WriteLine("- jar:");
            Console.WriteLine(bin_jar.FullName);

            var proccess_jar =
                new Process
                {
                    StartInfo = new ProcessStartInfo(
                        Path.Combine(javapath.FullName, "jar.exe"),
                        @"cvM -C release ."
                    )
                    {
                        UseShellExecute = false,
                        CreateNoWindow = true,

                        WorkingDirectory = obj_web,

                        RedirectStandardOutput = true,
                    }
                };

            if (bin_jar.Exists)
                bin_jar.Delete();

            using (var bin_jar_stream = bin_jar.OpenWrite())
            {
                proccess_jar.Start();

                var proccess_jar_output = new Thread(
                    delegate()
                    {
                        while (true)
                        {
                            var data = new byte[4096];
                            var datac = proccess_jar.StandardOutput.BaseStream.Read(data, 0, data.Length);

                            if (datac <= 0)
                                break;

                            bin_jar_stream.Write(data, 0, datac);
                        }
                    }
                )
                {
                    IsBackground = true
                };

                proccess_jar_output.Start();
                proccess_jar.WaitForExit();
                proccess_jar_output.Join();
            }
            #endregion




            #region run_jar
            // 4
            var run_jar = Path.Combine(TargetAssembly.Directory.FullName, Path.GetFileNameWithoutExtension(TargetAssembly.Name) + ".jar.bat");
            Console.WriteLine("- created bat entrypoint:");
            Console.WriteLine(run_jar);

            var library_path = bin_jar.Directory.FullName.Substring(new FileInfo(run_jar).Directory.FullName.Length + 1);

            var ClassPath = library_path + @"\" + bin_jar.Name;

            foreach (var r in from k in Directory.GetFiles(obj_web_bin, "*.jar")
                              where k != bin_jar.FullName
                              select k)
            {
                ClassPath += ";" + Path.Combine(library_path, Path.GetFileName(r));
            }

            File.WriteAllText(run_jar,
                @"
@echo off
setlocal

call """ + javapath.FullName + @"\java.exe"" -Djava.library.path=""" + library_path + @""" -cp """ + ClassPath + @""" " + TargetTypeFullName + @" %*

endlocal
"

            );

            if (FusionAssembly != null)
            {

                File.WriteAllBytes(FusionAssembly.FullName,
                    File.ReadAllBytes(TargetAssembly.FullName).Concat(
                        File.ReadAllBytes(bin_jar.FullName)
                    ).ToArray()
                );

                var FusionAssemblyStart = Path.ChangeExtension(FusionAssembly.FullName, "bat");

                Console.WriteLine("- created fusion bat entrypoint:");
                Console.WriteLine(FusionAssemblyStart);

                File.WriteAllText(FusionAssemblyStart,
                    @"@call """ + javapath.FullName + @"\java.exe"" -Djava.library.path=""."" -cp """ + FusionAssembly.Name + @""" " + TargetTypeFullName + @" %*"
                );
            }

            #endregion
        }


    }
}
