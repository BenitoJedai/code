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
	public partial class RewriteToUltraLibrary : CommandBase
	{

		public override void Invoke()
		{
			if (this.AttachDebugger)
				Debugger.Launch();

			var UltraSourceReferences = Enumerable.ToArray(
				from pp in new[] { PrimaryProject }
				where pp != null
				from k in pp.HintPaths
				where Path.GetFileNameWithoutExtension(k).EndsWith(ReferenceUltraSource.UltraSource)
				let p = Path.Combine(PrimaryAssembly.Directory.FullName, Path.GetFileName(k))
				where File.Exists(p)
				select p
			);

			if (this.Output != null)
			{
				this.DisableUltraSourceDetection = true;
			}

			if (this.Output == null)
				this.Output = this.PrimaryAssembly;

			if (DisableUltraSourceDetection)
			{
				// see? no checks!
			}
			else
			{
				if (!UltraSourceReferences.Any())
				{
					Console.WriteLine("No UltraSource assemblies found to be merged.");
					return;
				}
			}

			var CurrentEntryPoint = default(MethodInfo);

			var r = new RewriteToAssembly
			{

				DisableIsMarkedForMerge = true,

				obfuscate = this.Obfuscate,

				AssemblyMerge = Enumerable.ToArray(
					Enumerable.Concat(


						new RewriteToAssembly.AssemblyMergeInstruction[] { PrimaryAssembly.FullName },

						from k in UltraSourceReferences 
						select (RewriteToAssembly.AssemblyMergeInstruction)k
					)
				),

				Output = this.Output,

				PostTypeRewrite =
					a =>
					{
						if (this.EntryPoint != null)
						{
							if (a.SourceType.FullName == this.EntryPoint.TypeName)
							{
								var m = a.SourceType.GetMethod(this.EntryPoint.Method, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

								a.Assembly.SetEntryPoint(a.context.MethodCache[m], System.Reflection.Emit.PEFileKinds.WindowApplication);
							}
						}
						else
						{
							if (CurrentEntryPoint != null)
							{
								a.Assembly.SetEntryPoint(a.context.MethodCache[CurrentEntryPoint]);
							}
						}
					}
			};

			r.AssemblyMergeLoadHint +=
				s =>
				{
					CurrentEntryPoint = CurrentEntryPoint ?? s.EntryPoint;
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
