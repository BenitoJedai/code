using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace jsc.meta.Commands.Rewrite.RewriteToInstaller
{
	public class RewriteToInstaller : CommandBase
	{
		public string Feature;

		public override void Invoke()
		{
			if (Feature == null)
			{
				File.WriteAllBytes(

					Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName,

					"jsc.create-installer.exe"),

					new byte[0]
				);
			}
		}
	}
}
