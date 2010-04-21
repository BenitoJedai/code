using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using jsc.meta.Library;
using System.Reflection;
using jsc.meta.Commands.Reference.ReferenceUltraSource;
using System.Diagnostics;
using System.Reflection.Emit;

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

			var AssemblyMerge =
				Enumerable.Concat(
					new RewriteToAssembly.AssemblyMergeInstruction[] { PrimaryAssembly.FullName },

					from k in UltraSourceReferences
					select (RewriteToAssembly.AssemblyMergeInstruction)k


					// the idea is that when we want to merge our UltraSource or Primary with EntryPoint
					// then we omit unreferenced types. this does not work yet.
				).Where(k => !this.merge.Any(kk => Path.GetFileNameWithoutExtension(k.name) == kk.name)).ToArray();


			var r = new RewriteToAssembly
			{
				DisableIsMarkedForMerge = true,

				obfuscate = this.Obfuscate,


				AssemblyMerge = AssemblyMerge,

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

		

			#region on demand code with resources
			r.merge = this.merge;

			var CurrentScriptResources = new jsc.meta.Commands.Rewrite.RewriteToJavaScriptDocument.ScriptResources();

			#region AtILOverride copy assets
			r.AtILOverride +=
				(context, x) =>
				{

					x.BeforeInstruction +=
						e =>
						{
							if (e.i.OpCode == OpCodes.Ldstr)
							{
								// if it is a websource we need to copy it.
								var Assembly = e.i.OwnerMethod.DeclaringType.Assembly;

								if (r.AssemblyMerge.Any(k => Path.GetFileNameWithoutExtension(k.name) == Assembly.GetName().Name))
								{
									// already handled as all resources were copied!
								}
								else
								{
									CurrentScriptResources.Cache[Assembly].AddWhenResource(
										r.RewriteArguments.ScriptResourceWriter,
										e.i.TargetLiteral
									);
								}
							}
						};
				};
			#endregion
			#endregion

			r.Invoke();

			foreach (var item in UltraSourceReferences)
			{
				if (this.Output.Directory == new FileInfo(item).Directory)
				{
					Console.WriteLine("removing " + item);
					File.Delete(item);
				}
			}
		}
	}
}
