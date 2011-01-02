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

                    // http://blogs.msdn.com/jmstall/archive/2006/09/28/CreateNoWindow.aspx
                    CreateNoWindow = false,

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

            // so where is our jar?
            var bin_jar_FullName = bin_jar.Directory.FullName;
            var run_jar_FullName = new FileInfo(run_jar).Directory.FullName;

            var library_path = bin_jar_FullName.Length == run_jar_FullName.Length ? "." : bin_jar_FullName.Substring(run_jar_FullName.Length + 1);


            Func<FileInfo, string> ClassPathFromFile =
                TargetFile =>
                {
                    var ClassPath = @".\" + TargetFile.Name;

                    foreach (var r in from k in Directory.GetFiles(obj_web_bin, "*.jar")
                                      where k != bin_jar.FullName
                                      select k)
                    {
                        ClassPath += ";" + @".\" + Path.GetFileName(r);
                    }

                    return ClassPath;
                };


     

            if (FusionAssembly == null)
            {
                File.WriteAllText(run_jar,
         @"
@echo off
setlocal

set PATH=%PATH%

pushd " + library_path + @"
call """ + javapath.FullName + @"\java.exe"" -Djava.library.path=""."" -cp """ + ClassPathFromFile(bin_jar) + @""" " + TargetTypeFullName + @" %*

popd

endlocal
"
     );
            }
            else
            {
                // um, this is dirty :) I am sure we could just append the bytes via IO.
                File.WriteAllBytes(FusionAssembly.FullName,

                    // breaking change, we expect the fusion assebly to be the assembly we are appending...

                    File.ReadAllBytes(FusionAssembly.FullName).Concat(
                        File.ReadAllBytes(bin_jar.FullName)
                    ).ToArray()
                );

                //var FusionAssemblyStart = Path.Combine(TargetAssembly.Directory.FullName, Path.GetFileNameWithoutExtension(FusionAssembly.Name) + ".fusion.bat");

                //var FusionAssemblyStart = Path.ChangeExtension(FusionAssembly.FullName, "bat");

                //Console.WriteLine("- created fusion bat entrypoint:");
                //Console.WriteLine(FusionAssemblyStart);

                File.WriteAllText(run_jar,
    @"
@echo off
setlocal

pushd " + library_path + @"
call """ + javapath.FullName + @"\java.exe"" -Djava.library.path=""."" -cp """ + ClassPathFromFile(FusionAssembly) + @""" " + TargetTypeFullName + @" %*

popd

endlocal
"
);

             
                // let's clean up. while we could have created the jar in memory, we have not done that for now
                // so we have to remove that file as we are in fusion mode.

                bin_jar.Delete();
            }

            #endregion
        }


    }
}
