using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio
{
	public class SolutionProjectLanguageMethod
	{
		public const string op_Implicit = "op_Implicit";

		public const string ConstructorName = ".ctor";

		public bool IsConstructor
		{
			get
			{
				return this.Name == ConstructorName;
			}
		}

		public bool IsLambda
		{
			get
			{
				return this.Name == null;
			}
		}

		public bool IsStatic;

		public string Name;

		public string Summary;

		public readonly List<SolutionProjectLanguageArgument> Parameters = new List<SolutionProjectLanguageArgument>();

		public SolutionProjectLanguageCode Code;

		public SolutionProjectLanguageType DeclaringType;

		public bool IsProperty;
		public bool IsExtensionMethod;

		/// <summary>
		/// In FSharp we may need to use "|> ignore" operator
		/// </summary>
		public SolutionProjectLanguageType ReturnType;

	}
}
