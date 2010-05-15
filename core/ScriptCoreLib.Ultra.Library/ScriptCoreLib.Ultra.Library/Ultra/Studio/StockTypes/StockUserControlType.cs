using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio.StockTypes
{
	public class StockUserControlType : SolutionProjectLanguageType
	{
		public StockUserControlType(string Namespace, string Name)
		{
			var UserControl1DesignerType =
				new SolutionProjectLanguagePartialType
				{
					Name = Name + ".Designer",
					Type = new SolutionProjectLanguageType
					{
						Namespace = Namespace,
						Name = Name,
					},
				};

			var disposing = new SolutionProjectLanguageArgument
			{
				Summary = "true if managed resources should be disposed; otherwise, false.",
				Name = "disposing",
				Type = new StockSystemBooleanType(),
			};

			var Dispose = new SolutionProjectLanguageMethod
			{
				IsProtected = true,
				IsOverride = true,
				Summary = "Clean up any resources being used.",
				Name = "Dispose",
				DeclaringType = UserControl1DesignerType.Type,
				Code = new SolutionProjectLanguageCode
				{
					"Note: This jsc project does not support unmanaged resources.",
					new PseudoCallExpression
					{
						Object = new PseudoBaseExpression(),
						Method = new SolutionProjectLanguageMethod
						{
							Name = "Dispose",
						},
						ParameterExpressions = new[]
						{
							disposing
						}
					}
				}
			};

			Dispose.Parameters.Add(disposing);

			var components =
				new SolutionProjectLanguageField
				{
					IsPrivate = true,
					Name = "components",
					Summary = "Required designer variable.",
					FieldType = new SolutionProjectLanguageType
					{
						Namespace = "System.ComponentModel",
						Name = "IContainer"
					}
				};

			var set_Name = new PseudoCallExpression
			{
				Object = new PseudoThisExpression(),
				Method = new SolutionProjectLanguageMethod
				{
					IsProperty = true,
					Name = "set_Name",
				},
				ParameterExpressions = new[]
				{
					new PseudoStringConstantExpression
					{
						Value = Name
					}
				}
			};

			var set_Size = new PseudoCallExpression
			{
				Object = new PseudoThisExpression(),
				Method = new SolutionProjectLanguageMethod
				{
					IsProperty = true,
					Name = "set_Size",
				},
				ParameterExpressions = new[]
				{
					new PseudoCallExpression
					{
						Method = new SolutionProjectLanguageMethod
						{
							DeclaringType = new SolutionProjectLanguageType
							{
								Namespace = "System.Drawing",
								Name = "Size"
							},
							Name = SolutionProjectLanguageMethod.ConstructorName
						},
						ParameterExpressions = new []
						{
							(PseudoInt32ConstantExpression) 400,
							(PseudoInt32ConstantExpression) 300,
						}
					}
				}
			};

			var InitializeComponent =
				new SolutionProjectLanguageMethod
				{
					Summary = @"Required method for Designer support - do not modify
the contents of this method with the code editor.",

					IsPrivate = true,
					DeclaringType = UserControl1DesignerType.Type,
					Name = "InitializeComponent",
					Code = new SolutionProjectLanguageCode
					{
						set_Name,
						set_Size
					}
				};


			var UserControl1Constructor =
				new SolutionProjectLanguageMethod
				{
					DeclaringType = UserControl1DesignerType.Type,
					Name = SolutionProjectLanguageMethod.ConstructorName,
					Code = new SolutionProjectLanguageCode
					{
						new PseudoCallExpression
						{
							Object = new PseudoThisExpression(),
							Method = InitializeComponent
						}
					}
				};


			this.Namespace = Namespace;
			this.Name = Name;

			this.BaseType = new SolutionProjectLanguageType
				{
					Namespace = "System.Windows.Forms",
					Name = "UserControl"
				};

			this.DependentPartialTypes = new[]
				{
					UserControl1DesignerType
				};



			UserControl1DesignerType.Type.UsingNamespaces.Add("System.ComponentModel");
			UserControl1DesignerType.Type.UsingNamespaces.Add("System.Windows.Forms");
			UserControl1DesignerType.Type.Fields.Add(components);

			this.Methods.Add(UserControl1Constructor);
			this.UsingNamespaces.Add("System.Collections.Generic");
			this.UsingNamespaces.Add("System.ComponentModel");
			this.UsingNamespaces.Add("System.Drawing");
			this.UsingNamespaces.Add("System.Linq");
			this.UsingNamespaces.Add("System.Text");
			this.UsingNamespaces.Add("System.Windows.Forms");



			UserControl1DesignerType.Type.Methods.Add(Dispose);
			UserControl1DesignerType.Type.Methods.Add(InitializeComponent);

		}
	}
}
