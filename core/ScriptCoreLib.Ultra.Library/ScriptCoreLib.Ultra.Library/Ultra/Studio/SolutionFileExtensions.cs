using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Library;
using System.Xml.Linq;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.Studio
{
	public static class SolutionFileExtensions
	{

		public static void WriteUsingNamespaceList(this SolutionFile File, SolutionProjectLanguage Language, SolutionProjectLanguageType Type)
		{
			File.Region(
				delegate
				{
					foreach (var item in Type.UsingNamespaces.ToArray())
					{
						Language.WriteUsingNamespace(File, item);
					}
				}
			);
		}

		public static void Write(this SolutionFile File, SolutionProjectLanguage Language, SolutionBuilder Context, SolutionFileComment[] Comments)
		{
			if (Comments != null)
			{
				File.Region(
					delegate
					{
						foreach (var item in Comments)
						{
							item.WriteTo(File, Language, Context);

						}
					}
				);
			}
		}


		public static void Write(this SolutionFile File, XElement Content)
		{
			Action<XElement> WriteXElement = null;

			Action Indent =
				delegate
				{
					File.IndentStack.Invoke();
				};

			WriteXElement =
				e =>
				{
					var Depth = File.IndentStack.Count;

					File.IndentStack.Push(
						delegate
						{
							if (Depth > 1)
								File.Write("\t");
						}
					);

					File.Region(
						delegate
						{
							Indent();
							File.Write(SolutionFileTextFragment.XMLKeyword, "<");
							File.Write(SolutionFileTextFragment.XMLElement, e.Name.LocalName);

							foreach (var item in e.Attributes().ToArray())
							{
								File.WriteSpace();
								File.Write(SolutionFileTextFragment.XMLAttributeName, item.Name.LocalName);
								File.Write(SolutionFileTextFragment.XMLKeyword, "=");
								File.Write(SolutionFileTextFragment.XMLKeyword, "'");

								// we need to escape
								File.Write(SolutionFileTextFragment.XMLAttributeValue, item.Value);
								File.Write(SolutionFileTextFragment.XMLKeyword, "'");
							}

							File.Write(SolutionFileTextFragment.XMLKeyword, ">");

							if (e.Nodes().FirstOrDefault() is XElement)
							{
								File.WriteLine();
							}

							foreach (var item in e.Nodes().ToArray())
							{
								// we need to escape

								var _XElement = item as XElement;
								if (_XElement != null)
								{
									WriteXElement(_XElement);
								}

								var _XText = item as XText;
								if (_XText != null)
								{
									File.Write(SolutionFileTextFragment.XMLText, _XText.Value);
								}

								var _XComment = item as XComment;
								if (_XComment != null)
								{
									File.IndentStack.Invoke();
									File.Write(SolutionFileTextFragment.XMLKeyword, "<!--");
									File.Write(SolutionFileTextFragment.XMLComment, _XComment.Value);
									File.Write(SolutionFileTextFragment.XMLKeyword, "-->");
									File.WriteLine();
								}
							}

							if (e.Nodes().LastOrDefault() is XElement)
							{
								Indent();
							}

							File.Write(SolutionFileTextFragment.XMLKeyword, "</");
							File.Write(SolutionFileTextFragment.XMLElement, e.Name.LocalName);
							File.Write(SolutionFileTextFragment.XMLKeyword, ">");

							if (e != Content)
							{
								File.WriteLine();
							}
						}
					);

					File.IndentStack.Pop();
				};

			WriteXElement(Content);
		}

		public static void Indent(this SolutionFile File, SolutionProjectLanguage Language, Action y)
		{
			var Depth = File.IndentStack.Count;


			File.IndentStack.Push(
				delegate
				{
					File.Write(SolutionFileTextFragment.Indent, "    ");
				}
			);

			y();

			File.IndentStack.Pop();
		}

		public static void Region(this SolutionFile File, Action y)
		{
			File.Write(new SolutionFileWriteArguments.BeginRegion());

			y();

			File.Write(new SolutionFileWriteArguments.EndRegion());
		}

		public static StringBuilder ToSolutionFile(this jsc.meta.Library.MVSSolutionFile.ProjectElement[] Projects)
		{
			var w = new StringBuilder();

			w.AppendLine();
			w.AppendLine("Microsoft Visual Studio Solution File, Format Version 11.00");
			w.AppendLine("# Visual Studio 2010");

			foreach (var item in Projects)
			{
				w.AppendLine("Project(\"" + item.Kind + "\") = \"" + item.Name + "\", \"" + item.ProjectFile + "\", \"" + item.Identifier + "\"");
				w.AppendLine("EndProject");
			}

			w.AppendLine("Global");

			w.AppendLine("	GlobalSection(SolutionConfigurationPlatforms) = preSolution");
			w.AppendLine("		Debug|Any CPU = Debug|Any CPU");
			w.AppendLine("		Release|Any CPU = Release|Any CPU");
			w.AppendLine("	EndGlobalSection");
			w.AppendLine("	GlobalSection(ProjectConfigurationPlatforms) = postSolution");

			foreach (var item in Projects)
			{
				w.AppendLine("		" + item.Identifier + ".Debug|Any CPU.ActiveCfg = Debug|Any CPU");
				w.AppendLine("		" + item.Identifier + ".Debug|Any CPU.Build.0 = Debug|Any CPU");
				w.AppendLine("		" + item.Identifier + ".Release|Any CPU.ActiveCfg = Release|Any CPU");
				w.AppendLine("		" + item.Identifier + ".Release|Any CPU.Build.0 = Release|Any CPU");
			}

			w.AppendLine("	EndGlobalSection");
			w.AppendLine("	GlobalSection(SolutionProperties) = preSolution");
			w.AppendLine("		HideSolutionNode = FALSE");
			w.AppendLine("	EndGlobalSection");

			w.AppendLine("EndGlobal");
			return w;
		}

	}
}
