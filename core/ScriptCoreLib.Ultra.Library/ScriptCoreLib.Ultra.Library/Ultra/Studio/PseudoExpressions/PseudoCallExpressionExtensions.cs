using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections;

namespace ScriptCoreLib.Ultra.Studio.PseudoExpressions
{
	public static class PseudoCallExpressionExtensions
	{
		public static PseudoCallExpression ToPseudoCallExpression(this XElement e)
		{
			// we need a factory or context for equality
			var XElement = new SolutionProjectLanguageType
			{
				Namespace = "System.Xml.Linq",
				Name = "XElement"
			};

			var XAttribute = new SolutionProjectLanguageType
			{
				Namespace = "System.Xml.Linq",
				Name = "XAttribute"
			};

			var XName = new SolutionProjectLanguageType
			{
				Namespace = "System.Xml.Linq",
				Name = "XName"
			};

			var XComment = new SolutionProjectLanguageType
			{
				Namespace = "System.Xml.Linq",
				Name = "XComment"
			};

			#region XNameFromString
			Func<string, PseudoCallExpression> XNameFromString =
				Name =>
				{
					return new PseudoCallExpression
					{

						Method = new SolutionProjectLanguageMethod
						{
							DeclaringType = XName,
							IsStatic = true,
							Name = SolutionProjectLanguageMethod.op_Implicit
						},
						ParameterExpressions = new[]
						{
							new PseudoStringConstantExpression { 
								Value = Name
							}
						}
					};
				};
			#endregion

			var CreateXElement = default(Func<XElement, PseudoCallExpression>);

			Func<XElement, object[]> GetParameters =
				ee =>
				{

					var NewParameters = new ArrayList();

					NewParameters.Add(XNameFromString(ee.Name.LocalName));

					var Content = new PseudoArrayExpression();

					Content.ElementType = new SolutionProjectLanguageType { Name = "object" };

					foreach (var item in ee.Attributes().ToArray())
					{
						Content.Items.Add(
							new PseudoCallExpression
							{

								Method = new SolutionProjectLanguageMethod
								{
									Name = SolutionProjectLanguageMethod.ConstructorName,

									DeclaringType = XAttribute,
									ReturnType = XAttribute
								},

								ParameterExpressions = new object []
								{
									XNameFromString(item.Name.LocalName),
									new PseudoStringConstantExpression
									{
										Value = item.Value
									}
								}
							}
						);
					}

					foreach (var item in ee.Nodes().ToArray())
					{
						var _XText = item as XText;
						if (_XText != null)
						{
							Content.Items.Add(
								new PseudoStringConstantExpression
								{
									Value = _XText.Value
								}
							);
						}

						var _XElement = item as XElement;
						if (_XElement != null)
						{
							Content.Items.Add(
								CreateXElement(_XElement)
							);
						}


						var _XComment = item as XComment;
						if (_XComment != null)
						{
							Content.Items.Add(
								new PseudoCallExpression
								{

									Method = new SolutionProjectLanguageMethod
									{
										Name = SolutionProjectLanguageMethod.ConstructorName,

										DeclaringType = XComment,
										ReturnType = XComment
									},

									ParameterExpressions = new object[]
									{
										new PseudoStringConstantExpression
										{
											Value = _XComment.Value
										}
									}
								}
							);
						}

						
					}

					NewParameters.Add(Content);

					return NewParameters.ToArray();
				};

			CreateXElement =
				ee =>
				{
					return new PseudoCallExpression
					{
						XLinq = ee,

						Method = new SolutionProjectLanguageMethod
						{
							Name = SolutionProjectLanguageMethod.ConstructorName,

							DeclaringType = XElement,
							ReturnType = XElement
						},

						ParameterExpressions = GetParameters(ee)
					};
				};

			return CreateXElement(e);
		}

	}
}
