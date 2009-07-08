using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.CSharp.Extensions;
using ScriptCoreLib;

namespace jsc.Languages.ActionScript
{
	static class EmbedResourcesProvider
	{
		public static Type ResolveEmbedResourcesCollector(this ActionScriptCompiler w)
		{
			return w.MySession.Types.Where(k => k.GetCustomAttributes<EmbedResourcesAttribute>().Any()).FirstOrDefault();
		}

		public static void WriteEmbedResources(this ActionScriptCompiler w, Type z)
		{
			if (z.Assembly != w.MySession.Options.TargetAssemblyReference)
			{
				w.WriteIdent();
				w.WriteCommentLine("there is a superior entrypoint which will embed the assets...");

				return;
			}

			if (!z.GetCustomAttributes<ScriptApplicationEntryPointAttribute>().First().WithResources)
				return;

			// its all fine and dandy that we are ready to register
			// our resources within this flash application
			// but do we have a collector somewhere in the core lib?


			var Collector = w.ResolveEmbedResourcesCollector();

			if (Collector == null)
			{
				// no dice...

				return;
			}

			w.WriteIdent();
			w.WriteCommentLine("adding resources to the collector...");

			// kind of cool to think about meta commenting :)

			var Default = Collector.GetField("Default", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
			var Default_setter = Collector.GetMethod("set_Item", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

			Action<string, string> AddResource =
				(path, member) =>
				{
					if (!path.StartsWith("assets/"))
					{
						// The scriptcorelib has embedded actionscript3 source
						// code. We need to skip them.
						return;
					}

					w.CompileType_WriteAdditionalMembers +=
						delegate
						{
							var AttributeRef = new ActionScriptCompiler.EmbedAttributeStub
							{
								source = "/" + path,
								mimeType = EmbedMimeTypes.Resolve(path)
							};

							w.WriteCustomAttribute("Embed", AttributeRef, typeof(ActionScriptCompiler.EmbedAttributeStub).GetFields());



							w.WriteIdent();
							w.WriteKeywordSpace(ActionScriptCompiler.Keywords._internal);
							w.WriteKeywordSpace(ActionScriptCompiler.Keywords._static);
							w.WriteKeywordSpace(ActionScriptCompiler.Keywords._var);

							w.WriteSafeLiteral(member);

							w.Write(":");
							w.Write("Class");
							w.WriteLine(";");
						};


					w.WriteIdent();
					w.WriteDecoratedTypeName(Collector);
					w.Write(".");
					w.WriteSafeLiteral(Default.Name);
					w.Write(".");
					w.WriteDecoratedMethodName(Default_setter, false);
					w.Write("(");
					w.WriteQuotedLiteral(path);
					w.Write(", ");
					w.WriteDecoratedTypeName(z);
					w.Write(".");
					w.WriteSafeLiteral(member);
					w.Write(")");
					w.WriteLine(";");
				};

			var i = 0;

			foreach (var a in SharedHelper.LoadReferencedAssemblies(z.Assembly, true))
			{
				w.WriteIdent();
				w.WriteCommentLine(a.FullName);

				foreach (var n in EmbeddedResourcesExtensions.GetEmbeddedResources(null, a))
				{
					i++;
					AddResource(n, "__asset" + i);
				}
			}

		}
	}
}
