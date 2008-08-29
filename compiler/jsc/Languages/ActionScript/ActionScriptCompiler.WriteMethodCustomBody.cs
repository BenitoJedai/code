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
			var web = new DirectoryInfo(new FileInfo(m.DeclaringType.Assembly.Location).Directory.FullName + "/web");

			WriteIdent();
			WriteKeywordSpace(Keywords._return);
			Write("[");
			WriteLine();

			var a = new List<string>();

			#region fields
			CompilerJob.ExtractEmbeddedResources(web, m.DeclaringType.Assembly,
				(v, tf, Path, File) =>
				{
					var source = Path + "/" + File;

					a.Add(source);
				}
			);
			#endregion

			for (int i = 0; i < a.Count; i++)
			{
				var source = a[i];
				
				WriteIdent();
				WriteQuotedLiteral(source);

				if (i < a.Count - 1)
					Write(",");

				WriteLine();
			}

			WriteIdent();
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
			CompilerJob.ExtractEmbeddedResources(web, m.DeclaringType.Assembly,
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



							WriteIdent();
							WriteKeywordSpace(Keywords._internal);
							WriteKeywordSpace(Keywords._static);
							WriteKeywordSpace(Keywords._var);

							WriteIndexedField(index);

							Write(":");
							Write("Class");
							WriteLine(";");
						};

					WriteIdent();
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

			WriteIdent();
			WriteKeywordSpace(Keywords._return);
			WriteKeyword(Keywords._null);
			WriteLine(";");
		}
	}
}
