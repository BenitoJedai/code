using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace jsc.meta.Commands.Rewrite.RewriteToVSProjectTemplate
{
	partial class RewriteToMVSProjectTemplate
	{
		/// <summary>
		/// If there is no project file we could also do a IL to C# compilation...
		/// </summary>
		public FileInfo ProjectFileName;

		/// <summary>
		/// We need to know the title, description and the company.
		/// </summary>
		public FileInfo Assembly;

		/// <summary>
		/// Either be it under My Documents or a full path where to create the new template.
		/// </summary>
		public string UserProjectTemplates = @"Visual Studio 10\Templates\ProjectTemplates";

		public bool AttachDebugger;

		public DirectoryInfo SDKProjectTemplates = new DirectoryInfo(
			@"c:\util\jsc\templates\Visual Studio 10\Templates\ProjectTemplates"
		);

	}
}
