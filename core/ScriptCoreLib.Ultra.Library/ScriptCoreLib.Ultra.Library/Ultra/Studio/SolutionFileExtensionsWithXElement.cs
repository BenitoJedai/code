using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Library;
using System.Xml.Linq;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.Studio.Formatting;

namespace ScriptCoreLib.Ultra.Studio
{
	public static class SolutionFileExtensionsWithXElement
	{




		public static void WriteHTMLElement(this SolutionFile File, XElement Content)
		{
			Write(File, Content, File.HTMLElementFormatting);
		}

		public static void WriteXElement(this SolutionFile File, XElement Content)
		{
			Write(File, Content, File.XElementFormatting);
		}

		public static void Write(this SolutionFile File, XElement Content, XElementFormatting Arguments)
		{
			Action<XElement> WriteXElement = null;
			Action<XElement> WriteXElementRoot = null;


			Action PrettyPrintLine =
				delegate
				{
					File.Indent(null,
						delegate
						{
							File.WriteLine();
							File.IndentStack.Invoke();
						}
					);
				};

			WriteXElementRoot =
				e =>
				{
					File.Region(
						delegate
						{
							//if (e != Content)
							//    File.IndentStack.Invoke();

							File.Write(SolutionFileTextFragment.XMLKeyword, "<");
							File.Write(SolutionFileTextFragment.XMLElement, Arguments.GetName(e));

							foreach (var item in e.Attributes().ToArray())
							{
								File.WriteSpace();
								File.Write(SolutionFileTextFragment.XMLAttributeName, item.Name.LocalName);
								File.Write(SolutionFileTextFragment.XMLKeyword, "=");
								File.Write(SolutionFileTextFragment.XMLKeyword, "'");

								// we need to escape
								// http://bytes.com/topic/net/answers/551321-incomplete-escaping-functionality

								Arguments.WriteXMLAttributeValue(e, item, File);

								File.Write(SolutionFileTextFragment.XMLKeyword, "'");
							}

							var IsCollapsed = Arguments.CanCollapse(e) && !e.Nodes().Any();

							if (IsCollapsed)
							{
								File.Write(SolutionFileTextFragment.XMLKeyword, "/>");
							}
							else
							{
								File.Write(SolutionFileTextFragment.XMLKeyword, ">");

								#region content

								var IsElementOnly = !e.Nodes().Any(k => k is XText);

								//if (IsElementOnly)
								//{
								//    if (e.Nodes().FirstOrDefault() is XElement)
								//    {
								//        PrettyPrintLine();
								//    }
								//}

								var Previous = default(XNode);
								foreach (var item in e.Nodes().ToArray())
								{
									// we need to escape



									var _XElement = item as XElement;
									if (_XElement != null)
									{
										if (IsElementOnly)
										{
											PrettyPrintLine();
										}

										WriteXElement(_XElement);
									}

									var _XText = item as XText;
									if (_XText != null)
									{
										Func<string, Action> WriteXMLText =
											vv =>
											{
												return delegate
												{
													File.Write(SolutionFileTextFragment.XMLText, InternalXMLExtensions.ToXMLString(vv));
												};
											};

										Action Separator =
											delegate
											{
												File.WriteLine();
												File.IndentStack.Invoke();
											};

										_XText.Value.Replace("\n", Environment.NewLine).Replace("\r" + Environment.NewLine, Environment.NewLine).ToLines().Select(WriteXMLText).SelectWithSeparator(Separator).Invoke();
									}

									var _XComment = item as XComment;
									if (_XComment != null)
									{
										File.Write(SolutionFileTextFragment.XMLKeyword, "<!--");
										File.Write(SolutionFileTextFragment.XMLComment, _XComment.Value);
										File.Write(SolutionFileTextFragment.XMLKeyword, "-->");
									}

									Previous = item;
								}

								if (IsElementOnly)
								{
									if (e.Nodes().LastOrDefault() is XElement)
									{
										File.WriteLine();
										File.IndentStack.Invoke();
									}
								}
								#endregion


								File.Write(SolutionFileTextFragment.XMLKeyword, "</");
								File.Write(SolutionFileTextFragment.XMLElement, Arguments.GetName(e));
								File.Write(SolutionFileTextFragment.XMLKeyword, ">");
							}

							//if (e != Content)
							//{
							//    File.WriteLine();
							//}
						}
					);

				};

			WriteXElement =
				e =>
				{
					File.Indent(null,
						delegate
						{
							WriteXElementRoot(e);
						}
					);
				};

			WriteXElementRoot(Content);
		}


	}
}
