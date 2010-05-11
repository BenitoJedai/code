using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio.InteractiveExpressions
{
	public class ApplicationYieldToDocumentTitleExpression : PseudoCallExpression
	{
		public readonly InteractiveComment
			InteractiveComment = "Show server message as document title";




		public ApplicationYieldToDocumentTitleExpression(SolutionBuilderInteractive Interactive)
		{
			this.Comment = InteractiveComment;

			PseudoConstantExpression ElementName = new PseudoConstantExpression
			{
				Value =
					"Data"
			};

			InteractiveComment.Click +=
				delegate
				{
					if ((string)ElementName.Value == "Data")
					{
						ElementName.Value = "Client";
						InteractiveComment.Comment = "Show the original data as document title";
					}
					else
					{
						ElementName.Value = "Data";
						InteractiveComment.Comment = "Show server message as document title";
					}
				};

			this.Method = new SolutionProjectLanguageMethod
			{
				IsExtensionMethod = true,
				Name = "ToDocumentTitle",
				DeclaringType = new SolutionProjectLanguageType
				{
					Namespace = "ScriptCoreLib.JavaScript.Extensions",
					Name = "JavaScriptStringExtensions"
				},
				ReturnType = new SolutionProjectLanguageType
				{
					Namespace = "System",
					Name = "String"
				}
			};

			// we need a factory or context for equality
			var XElement = new SolutionProjectLanguageType
			{
				Namespace = "System.Xml.Linq",
				Name = "XElement"
			};

			var XName = new SolutionProjectLanguageType
			{
				Namespace = "System.Xml.Linq",
				Name = "XName"
			};



			var doc_DataElement =
				new PseudoCallExpression
				{
					Object = Interactive.YieldMethod_doc,

					Method = new SolutionProjectLanguageMethod
					{
						DeclaringType = XElement,
						Name = "Element"
					},

					ParameterExpressions = new[] {
						new PseudoCallExpression {
							
							Method = new SolutionProjectLanguageMethod
							{
								DeclaringType = new SolutionProjectLanguageType
								{
									Namespace = "System.Xml.Linq",
									Name = "XName"
								},
								IsStatic = true,
								Name = SolutionProjectLanguageMethod.op_Implicit
							},
							ParameterExpressions = new []
							{
								ElementName
							}
						}
						
					}
				};

			var doc_DataElement_value =
				new PseudoCallExpression
				{
					Object = doc_DataElement,

					Method = new SolutionProjectLanguageMethod
					{
						IsProperty = true,
						DeclaringType = XElement,
						Name = "get_Value"
					},

					ParameterExpressions = new object[] { }
				};

			this.ParameterExpressions = new[] {
				doc_DataElement_value
			};
		}
	}

}
