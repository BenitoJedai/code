using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
	public partial class VisualFSharpLanguage : SolutionProjectLanguage
	{
		public override string ProjectFileExtension { get { return ".fsproj"; } }
		public override string CodeFileExtension { get { return ".fs"; } }

		public override string Kind
		{
			get { return "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"; }
		}

		public override void WriteLinkCommentLine(SolutionFile File, Uri Link)
		{
			File.Write(SolutionFileTextFragment.Comment, "// ");
			File.Write(Link);
			File.WriteLine();
		}

		public override void WriteCommentLine(SolutionFile File, string Text)
		{
			File.WriteLine(SolutionFileTextFragment.Comment, "// " + Text);
		}

		public override void WriteXMLCommentLine(SolutionFile File, string Text)
		{
			// http://msdn.microsoft.com/en-us/library/aa288481(VS.71).aspx

			File.WriteLine(SolutionFileTextFragment.Comment, "/// " + Text);
		}

		public override void WriteIndent(SolutionFile File)
		{
			File.IndentStack.Invoke();
		}

		public override void WriteMethod(SolutionFile File, SolutionProjectLanguageMethod Method, SolutionBuilder Context)
		{
		}

		public override void WriteMethodBody(SolutionFile File, SolutionProjectLanguageCode Code, SolutionBuilder Context)
		{
		}

		public override void WriteTypeName(SolutionFile File, SolutionProjectLanguageType Type)
		{
			if (Type.DeclaringType != null)
			{
				WriteTypeName(File, Type.DeclaringType);
				File.Write(".");
			}

			if (Type.ElementType != null)
			{
				WriteTypeName(File, Type.ElementType);
				File.Write("[]");

				return;
			}

			File.Write(Type);

			if (Type.Arguments.Count > 0)
			{
				File.Write("<");

				var Arguments = Type.Arguments.ToArray();

				for (int i = 0; i < Arguments.Length; i++)
				{
					if (i > 0)
					{
						File.Write(",");
						File.WriteSpace();
					}

					this.WriteTypeName(File, Arguments[i].Type);
				}

				File.Write(">");

			}
		}

		public override void WriteType(SolutionFile File, SolutionProjectLanguageType Type, SolutionBuilder Context)
		{
			File.Write(this, Context, Type.Comments);

			File.Region(
				delegate
				{
					File.Write(Keywords.@namespace);
					File.WriteSpace();
					File.Write(Type.Namespace);
					File.WriteLine();

					File.WriteLine();

					File.Indent(this,
						delegate
						{
							File.WriteUsingNamespaceList(this, Type);
							
							File.WriteLine();

							WriteIndent(File);
							File.Write(Keywords.type);
							File.WriteSpace();
							WriteTypeName(File, Type);
							File.Write("(");
							File.Write(")");
							File.WriteSpace();
							File.Write("=");
							File.WriteSpace();
							File.Write(Keywords.@do);
							File.WriteLine();
						}
					);
				}
			);

		}

		public override void WriteAssemblyAttribute(SolutionFile File, SolutionProjectLanguageAttribute Attribute)
		{
		}

		public override void WriteUsingNamespace(SolutionFile File, string item)
		{
			WriteIndent(File);

			File.Write(Keywords.@open);
			File.WriteSpace();
			File.Write(item);

			File.WriteLine();
		}

		public override void WritePseudoExpression(SolutionFile File, object Parameter)
		{
		}

		public override void WritePseudoCallExpression(SolutionFile File, ScriptCoreLib.Ultra.Studio.PseudoExpressions.PseudoCallExpression Lambda)
		{
		}
	}
}
