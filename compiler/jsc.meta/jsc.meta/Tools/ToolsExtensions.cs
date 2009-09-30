﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace jsc.meta.Tools
{
	static class ToolsExtensions
	{
		public static void ToJavaScript(this FileInfo TargetAssembly)
		{
			// we should run jsc in another appdomain actually
			// just to be sure our nice tool gets unloaded :)

			jsc.Program.TypedMain(
				new jsc.CompileSessionInfo
				{
					Options = new jsc.CommandLineOptions
					{
						TargetAssembly = TargetAssembly,
						IsJavaScript = true,
						IsNoLogo = true
					}
				}
			);
		}

		public static void ToJava(this FileInfo TargetAssembly, DirectoryInfo javapath, MethodInfo assembly_metaentrypoint, FileInfo FusionAssembly)
		{

			// we should run jsc in another appdomain actually
			// just to be sure our nice tool gets unloaded :)

			jsc.Program.TypedMain(
				new jsc.CompileSessionInfo
				{
					Options = new jsc.CommandLineOptions
					{
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
			var bin_jar = new FileInfo(Path.Combine(obj_web_bin, Path.GetFileNameWithoutExtension(TargetAssembly.Name) + @".jar"));

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
					@"-classpath " + TargetSourceFiles + @" -d release java\" + assembly_metaentrypoint.DeclaringType.FullName.Replace(".", @"\") + @".java"
					)
				{
					UseShellExecute = false,

					WorkingDirectory = obj_web
				}
			);

			proccess_javac.WaitForExit();
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

call """ + javapath.FullName + @"\java.exe"" -Djava.library.path=""" + library_path + @""" -cp """ + ClassPath + @""" " + assembly_metaentrypoint.DeclaringType.FullName + @" %*

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
					@"@call """ + javapath.FullName + @"\java.exe"" -Djava.library.path=""."" -cp """ + FusionAssembly.Name + @""" " + assembly_metaentrypoint.DeclaringType.FullName + @" %*"
				);
			}

			#endregion
		}

		public static void ToActionScript(this FileInfo TargetAssembly, FileInfo mxmlc, FileInfo flashplayer, Type sprite, FileInfo FusionAssembly)
		{
			jsc.Program.TypedMain(
				new jsc.CompileSessionInfo
				{
					Options = new jsc.CommandLineOptions
					{
						TargetAssembly = TargetAssembly,
						IsActionScript = true,
						IsNoLogo = true
					}
				}
			);

			var obj_web = Path.Combine(TargetAssembly.Directory.FullName, "web");


			#region javac

			//// each jar file which has made it to the bin folder
			//// needs to get referenced in our java build
			//foreach (var r in from k in Directory.GetFiles(obj_web_bin, "*.jar")
			//                  where k != bin_jar.FullName
			//                  select k)
			//{
			//    TargetSourceFiles += ";" + Path.Combine("bin", Path.GetFileName(r));
			//}

			var obj_web_swf = Path.Combine(obj_web, sprite.Name + ".swf");

			var proccess_mxmlc = Process.Start(
				new ProcessStartInfo(
					mxmlc.FullName,
					"-sp=. -strict -output=\"" +
						obj_web_swf + "\" "
						+ sprite.FullName.Replace(".", @"\") + @".as"
					)
				{
					UseShellExecute = false,

					WorkingDirectory = obj_web
				}
			);

			proccess_mxmlc.WaitForExit();

			var m = new MemoryStream();
			var w = new BinaryWriter(m);
			w.Write(File.ReadAllBytes(flashplayer.FullName));
			w.Write(File.ReadAllBytes(obj_web_swf));
			w.Write((uint)0xFA123456);
			w.Write((uint)new FileInfo(obj_web_swf).Length);

			File.WriteAllBytes(obj_web_swf + ".exe", m.ToArray());
			#endregion



			// call C:\util\flex\bin\mxmlc.exe -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as

		}
	}
}
