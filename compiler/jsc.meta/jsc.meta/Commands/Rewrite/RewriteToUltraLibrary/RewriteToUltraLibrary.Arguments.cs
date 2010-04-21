using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using jsc.meta.Library;
using System.Reflection;
using jsc.meta.Commands.Reference.ReferenceUltraSource;
using System.Diagnostics;

namespace jsc.meta.Commands.Rewrite.RewriteToUltraLibrary
{
	partial class RewriteToUltraLibrary
	{
		/* 

		 * To be used in Assets build configuration post build event!
		 * 
		 usage: 
		 
		 RewriteToUltraLibrary /PrimaryAssembly:"W:\jsc.svn\templates\Orcas\UltraLibraryWithAssets\UltraLibraryWithAssets\bin\Debug\UltraLibraryWithAssets.dll" /PrimaryProject:"W:\jsc.svn\templates\Orcas\UltraLibraryWithAssets\UltraLibraryWithAssets\UltraLibraryWithAssets.csproj"
		 
		 c:\util\jsc\bin\jsc.meta.exe RewriteToUltraLibrary /PrimaryAssembly:"$(TargetPath)" /PrimaryProject:"$(ProjectPath)"
		
		 */

		public FileInfo PrimaryAssembly;

		public FileInfo Output;

		/// <summary>
		/// At runtime we cannot see the unreferenced assemblies. For now we just also load the project file to get additional intel.
		/// </summary>
		public MSVSProjectFile PrimaryProject;

		public bool AttachDebugger;

		public bool DisableUltraSourceDetection;

		public bool Obfuscate;

		public class EntryPointInfo
		{
			public string TypeName;
			public string Method = "Main";

			public static EntryPointInfo FromType(string TypeName)
			{
				return new EntryPointInfo { TypeName = TypeName };
			}
		}

		public EntryPointInfo EntryPoint;

		public RewriteToAssembly.MergeInstruction[] merge = new RewriteToAssembly.MergeInstruction[0];
	}
}
