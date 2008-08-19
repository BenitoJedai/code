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

				return true;
			}

			return false;
		}
	}
}
