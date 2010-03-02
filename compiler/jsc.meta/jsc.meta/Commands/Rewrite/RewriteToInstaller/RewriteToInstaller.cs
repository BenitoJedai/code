using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using ScriptCoreLib.Archive.ZIP;
using jsc.meta.Library;
using jsc.meta.Commands.Rewrite.RewriteToInstaller.Templates;
using System.Threading;
using System.Windows.Forms;
using System.Reflection.Emit;
using ScriptCoreLib.Ultra.Library.Extensions;
using System.Diagnostics;

namespace jsc.meta.Commands.Rewrite.RewriteToInstaller
{
	public class RewriteToInstaller : CommandBase
	{
		public bool AttachDebugger;


		public string Feature;

		internal const string jsc_installer_zip = "jsc.installer.zip";

		public override void Invoke()
		{
			if (this.AttachDebugger)
				Debugger.Launch();

			if (Feature == null)
			{
				var jsc = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.Parent;

				var zip = new ZIPFile();

				var bin = new DirectoryInfo(Path.Combine(jsc.FullName, "bin"));
				foreach (var item in bin.GetFilesByPattern("*.exe", "*.dll", "*.xml"))
				{
					zip.Add(item.FullName.Substring(jsc.FullName.Length + 1), File.ReadAllBytes(item.FullName));
				}

				var lib = new DirectoryInfo(Path.Combine(jsc.FullName, "lib"));
				foreach (var item in bin.GetFilesByPattern("*.exe", "*.dll", "*.xml"))
				{
					zip.Add(item.FullName.Substring(jsc.FullName.Length + 1), File.ReadAllBytes(item.FullName));
				}

				var templates = new DirectoryInfo(Path.Combine(jsc.FullName, "templates"));
				foreach (var item in templates.GetAllFilesByPattern("*.zip"))
				{
					zip.Add(item.FullName.Substring(jsc.FullName.Length + 1), File.ReadAllBytes(item.FullName));
				}

				// http://www.theregister.co.uk/2007/04/23/vista_program_naming_oddness/

				var name = DateTime.Now.ToString("yyyyMMdd") + "_jsc.installer";
				var name_zip = name + ".zip";

				Console.WriteLine(name);


				// create an installer now!

				var r = default(RewriteToAssembly);

				r = new RewriteToAssembly
				{
					staging = jsc,

					product = name,
					productExtension = ".exe",

					merge = new RewriteToAssembly.MergeInstruction[] { "ScriptCoreLib.Archive.ZIP" },

					PostAssemblyRewrite =
						a =>
						{
							var AssetPath = "assets/jsc/" + name_zip;

							a.Module.DefineManifestResource(
								jsc_installer_zip,
								new MemoryStream(zip.ToBytes()),
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


				//r.AtILOverride +=
				//    (m, e) =>
				//    {
				//        if (((Func<byte[]>)Installer.GetArchive).Method == m)
				//        {
				//            e.BeforeInstructions +=
				//                i =>
				//                {
				//                    i.il.Emit(OpCodes.Ldnull);
				//                    i.il.Emit(OpCodes.Ret);
				//                    i.Complete();
				//                };
				//        }
				//    };

				r.Invoke();

				var exe_zip = new ZIPFile
				{
					{ r.Output.Name, File.ReadAllBytes(r.Output.FullName)}
				};

				File.WriteAllBytes(

					Path.Combine(jsc.FullName,
						name_zip
					),

					exe_zip.ToBytes()
				);

			}
		}


	}

	namespace Templates
	{
		public static class Installer
		{
			public static ZIPFile Archive
			{
				get
				{
					return typeof(Installer).Assembly.GetManifestResourceStream(RewriteToInstaller.jsc_installer_zip);
				}
			}

			public static void Install(string[] e)
			{
				var zip = new FileInfo(Path.ChangeExtension(new FileInfo(typeof(Installer).Assembly.Location).FullName, ".zip"));


				Console.WriteLine("jsc installer");
				Console.WriteLine("The following files will be created:");
				Console.WriteLine();

				var files = new Dictionary<string, byte[]>();
				var a = Archive;

				files[zip.FullName] = a.ToBytes();

				var MyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

				foreach (var template in a.Entries.Where(k => k.FileName.StartsWith("templates/")))
				{
					var file = template.FileName.SkipUntilIfAny("/");
					var mvs = file.TakeUntilIfAny("/");

					var p = Path.Combine(MyDocuments, mvs);
					if (Directory.Exists(p))
					{
						files[Path.Combine(MyDocuments, file)] = template.Bytes;
					}
				}

				foreach (var item in files)
				{
					Console.WriteLine(item.Key + " - " + item.Value.Length + " bytes");
				}

				Console.WriteLine();
				Console.WriteLine("Do you want to install jsc? [y/n]");

				if (Console.ReadKey(true).KeyChar != 'y')
					return;




				Console.WriteLine("Thank you for installing jsc!");
				Console.ReadKey(true);
			}
		}
	}
}
