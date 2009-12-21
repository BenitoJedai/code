using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;
using System.IO;
using jsc.CodeModel;

namespace jsc.Script.PHP
{
	partial class PHPCompiler
	{
		/// <summary>
		/// compiles the main file for the assambly, also compile web/inc/*.dll/class.*.php multithreaded
		/// </summary>
		public void Compile(Assembly ja, CompileSessionInfo sinfo)
		{

			var web = sinfo.Options.TargetAssembly.Directory.CreateSubdirectory("web");

			var u = web.CreateSubdirectory("inc");

			// http://php.net/manual/en/ini.core.php
			// looks like some webservers are not using short_open_tag
			WriteLine("<?php");

			Helper.WorkPool n = new Helper.WorkPool();

			//n.IsThreaded = !Debugger.IsAttached;

			List<TypeInfo> req = new List<TypeInfo>();

			using (new Helper.ConsoleStopper("php type compiler"))
			{
				foreach (var z in ActiveTypes)
				{

					if (z.Value.Assembly != ja)
						continue;

					CompilerBase c = new Script.PHP.PHPCompiler(new StringWriter(), this.MySession);

					c.CurrentJob = null;

					Program.AttachXMLDoc(new FileInfo(z.Value.Assembly.ManifestModule.FullyQualifiedName), c);

					if (c.CompileType(z.Value))
					{
						c.ToConsole(z.Value, sinfo);

						DirectoryInfo x = u.CreateSubdirectory(z.AssamblyFileName);

						string content = c.MyWriter.ToString();


						StreamWriter sw = new StreamWriter(new FileStream(web.FullName + "/" + z.TargetFileName, FileMode.Create));

						sw.WriteLine("<?php");
						sw.Write(content);
						sw.WriteLine("?>");
						sw.Flush();

						sw.Close();

						req.Add(z);
					}
				}

			}

			foreach (TypeInfo z in req)
			{
				WriteImport(z);
			}

			WriteLine();

			WriteLine("// calling static constructors for current assembly...");

			foreach (Type z in MySession.Types)
			{
				if (z.Assembly == ja)
					WriteTypeStaticConstructor(z, false);
			}

			WriteLine("?>");
		}

	}
}
