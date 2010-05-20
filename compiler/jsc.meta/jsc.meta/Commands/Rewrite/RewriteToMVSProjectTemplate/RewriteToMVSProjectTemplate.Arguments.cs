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

		public bool AttachDebugger;


		/// <summary>
		/// Either be it under My Documents or a full path where to create the new template.
		/// </summary>
		public string UserProjectTemplates = ProjectTemplatesTwentyTen;


		public DirectoryInfo SDKProjectTemplates;

		public const string ProjectTemplatesTwentyTen = @"Visual Studio 2010\Templates\ProjectTemplates";
		public const string ProjectTemplatesOrcas = @"Visual Studio 2008\Templates\ProjectTemplates";

		public bool DefaultToOrcas;

        public AssemblyAttributesType AssemblyAttributes = new AssemblyAttributesType();

        public class AssemblyAttributesType
        {
            public string Description;
            public string Title;
            public string Company;
        }

	}
}
