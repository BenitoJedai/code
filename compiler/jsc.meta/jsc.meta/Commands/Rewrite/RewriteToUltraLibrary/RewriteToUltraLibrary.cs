using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using jsc.meta.Library;
using System.Reflection;
using jsc.meta.Commands.Reference.ReferenceUltraSource;

namespace jsc.meta.Commands.Rewrite.RewriteToUltraLibrary
{
	public class RewriteToUltraLibrary : CommandBase
	{
		/* 

		 * To be used in Assets build configuration post build event!
		 * 
		 usage: 
		 
		 RewriteToUltraLibrary /PrimaryAssembly:"W:\jsc.svn\templates\Orcas\UltraLibraryWithAssets\UltraLibraryWithAssets\bin\Debug\UltraLibraryWithAssets.dll" /PrimaryProject:"W:\jsc.svn\templates\Orcas\UltraLibraryWithAssets\UltraLibraryWithAssets\UltraLibraryWithAssets.csproj"
		 
		 c:\util\jsc\bin\jsc.meta.exe RewriteToUltraLibrary /PrimaryAssembly:"$(TargetPath)" /PrimaryProject:"$(ProjectPath)"
		
		 */

		public FileInfo PrimaryAssembly;

		/// <summary>
		/// At runtime we cannot see the unreferenced assemblies. For now we just also load the project file to get additional intel.
		/// </summary>
		public MSVSProjectFile PrimaryProject;


		public override void Invoke()
		{

	
			var UltraSourceReferences = Enumerable.ToArray(
				from k in PrimaryProject.HintPaths
				where Path.GetFileNameWithoutExtension(k).EndsWith(ReferenceUltraSource.UltraSource)
				let p = Path.Combine(PrimaryAssembly.Directory.FullName, Path.GetFileName(k))
				where File.Exists(p)
				select p
			);

			if (!UltraSourceReferences.Any())
			{
				Console.WriteLine("No UltraSource assemblies found to be merged.");
				return;
			}

			var r = new RewriteToAssembly
			{

				DisableIsMarkedForMerge = true,

				AssemblyMerge = Enumerable.ToArray(
					Enumerable.Concat(
				

						new RewriteToAssembly.AssemblyMergeInstruction[] { PrimaryAssembly.FullName },

						from k in UltraSourceReferences
						select (RewriteToAssembly.AssemblyMergeInstruction)k
					)
				),

				Output = PrimaryAssembly
			};

			r.Invoke();

			foreach (var item in UltraSourceReferences)
			{
				Console.WriteLine("removing " + item);
				File.Delete(item);
			}
		}
	}
}
