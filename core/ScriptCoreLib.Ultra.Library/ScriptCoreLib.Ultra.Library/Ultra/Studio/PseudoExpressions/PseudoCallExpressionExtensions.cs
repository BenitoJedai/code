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

			var XName = new SolutionProjectLanguageType
			{
				Namespace = "System.Xml.Linq",
				Name = "XName"
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
							new PseudoConstantExpression { 
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

					foreach (var item in ee.Nodes().ToArray())
					{
						var _XText = item as XText;
						if (_XText != null)
						{
							Content.Items.Add(
								new PseudoConstantExpression
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
