using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.IO;

namespace jsc.meta.Commands.Rewrite.RewriteToVSProjectTemplate
{
	[Description(Description)]
	public partial class RewriteToMVSProjectTemplate : CommandBase
	{
		public const string Description = "Create project templates with ease!";

		public override void Invoke()
		{
			Console.WriteLine(Description);

			var Assembly = System.Reflection.Assembly.LoadFile(this.Assembly.FullName);

			var Attributes = new
			{
				Assembly.GetCustomAttributes<AssemblyDescriptionAttribute>().First().Description,
				Assembly.GetCustomAttributes<AssemblyTitleAttribute>().First().Title,
				Assembly.GetCustomAttributes<AssemblyCompanyAttribute>().First().Company
			};

			var MyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var MVSPath = Path.Combine(MyDocuments, this.MVSPath);

			Console.WriteLine(Attributes);
			Console.WriteLine(MVSPath);
		}
	}
}
