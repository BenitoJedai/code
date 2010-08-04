using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;
using ScriptCoreLib.ActionScript;
using System.IO;
using ScriptCoreLib.CSharp.Extensions;
using ScriptCoreLib.Shared;

namespace jsc.Languages.ActionScript
{
	partial class ActionScriptCompiler
	{
		protected override bool WriteMethodCustomBody(MethodBase m)
		{
			var EmbedByFileName = m.GetCustomAttributes<EmbedByFileNameAttribute>().FirstOrDefault();

			if (EmbedByFileName != null)
			{

				WriteMethodCustomBody_EmbedByFileName(m);

				return true;
			}

			var EmbedGetFileNames = m.GetCustomAttributes<EmbedGetFileNamesAttribute>().FirstOrDefault();

			if (EmbedGetFileNames != null)
			{

				WriteMethodCustomBody_EmbedGetFileNames(m);

				return true;
			}

			return false;
		}

		private void WriteMethodCustomBody_EmbedGetFileNames(MethodBase m)
		{
			WriteIndent();
			WriteKeywordSpace(Keywords._return);
			Write("[");
			WriteLine();

			var a = EmbeddedResourcesExtensions.GetEmbeddedResources(null, m.DeclaringType.Assembly);



			for (int i = 0; i < a.Length; i++)
			{
				var source = a[i];
				
				WriteIndent();
				WriteQuotedLiteral(source);

				if (i < a.Length - 1)
					Write(",");

				WriteLine();
			}

			WriteIndent();
			Write("]");
			Write(";");
			WriteLine();

		}

		private void WriteMethodCustomBody_EmbedByFileName(MethodBase m)
		{
			var web = new DirectoryInfo(new FileInfo(m.DeclaringType.Assembly.Location).Directory.FullName + "/web");
			var counter = 0;

			Action<int> WriteIndexedField =
				index =>
				{
					Write("embedded_" + index);
				};

			#region fields
			EmbeddedResourcesExtensions.ExtractEmbeddedResources(web, m.DeclaringType.Assembly,
				(v, tf, Path, File) =>
				{
					var source = Path + "/" + File;
					var index = counter;

					counter++;

					CompileType_WriteAdditionalMembers +=
						delegate
						{
							var AttributeRef = new EmbedAttributeStub
							{
								source = "/" + source,
								mimeType = EmbedMimeTypes.Resolve(source)
							};

							WriteCustomAttribute("Embed", AttributeRef, typeof(EmbedAttributeStub).GetFields());



							WriteIndent();
							WriteKeywordSpace(Keywords._internal);
							WriteKeywordSpace(Keywords._static);
							WriteKeywordSpace(Keywords._var);

							WriteIndexedField(index);

							Write(":");
							Write("Class");
							WriteLine(";");
						};

					WriteIndent();
					WriteKeywordSpace(Keywords._if);
					Write("(");
					WriteSafeLiteral(m.GetParameters().First().Name);
					WriteSpace();
					Write("==");
					WriteSpace();
					WriteQuotedLiteral(source);
					Write(")");
					WriteSpace();
					WriteKeywordSpace(Keywords._return);
					WriteIndexedField(index);
					WriteLine(";");
				}
			);
			#endregion

			WriteIndent();
			WriteKeywordSpace(Keywords._return);
			WriteKeyword(Keywords._null);
			WriteLine(";");
		}
	}
}
