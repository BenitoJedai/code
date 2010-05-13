using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.Studio.Formatting
{
	public class HTMLElementFormatting : XElementFormatting
	{
		public int StyleDeclarationWarpTreshold = 20;

		public HTMLElementFormatting()
		{
			this.GetName =
				e =>
				{
					return e.Name.LocalName.ToLower();
				};

			var Collapsed = new[]
			{
				"img",
				"br",
				"hr"
			};

			// http://www.htmldog.com/guides/htmlbeginner/tags/

			this.CanCollapse =
				e =>
				{
					var n = this.GetName(e);

					return Collapsed.Contains(n);
				};

			var WriteXMLAttributeValue = this.WriteXMLAttributeValue;


			this.WriteXMLAttributeValue =
				(e, a, File) =>
				{
					if (a.Name == "style")
					{
						// http://www.w3schools.com/css/css_syntax.asp


						var Declarations = a.Value.Split(
							new[] { ";" },
							StringSplitOptions.None
						);

						Declarations =
							(from k in Declarations
							 let t = k.Trim()
							 where t.Length > 0
							 select t + ";").ToArray();


						File.Indent(null,
							delegate
							{
								Func<string, Action> ToWriteDeclaration =
									x => () =>
									{
										// IE whats wrong with ya uppercasing HTML and CSS? :)
										var DeclarationProperty = x.TakeUntilOrEmpty(":").Trim().ToLower();
										var DeclarationValue = x.SkipUntilIfAny(":").Trim();


										File.Write(SolutionFileTextFragment.XMLAttributeValue, DeclarationProperty);
										File.Write(SolutionFileTextFragment.XMLAttributeValue, ": ");
										File.Write(SolutionFileTextFragment.XMLAttributeValue, DeclarationValue);
									};

								Action Separator =
									delegate
									{
										if (a.Value.Length > StyleDeclarationWarpTreshold)
										{
											File.WriteLine();
											File.IndentStack.Invoke();
										}
									};

								Declarations.Select(ToWriteDeclaration).SelectWithSeparator(Separator).Invoke();

							}
						);



						return;
					}

					WriteXMLAttributeValue(e, a, File);
				};
		}
	}
}
