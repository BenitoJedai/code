using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Ultra.Studio.StockTypes;

namespace ScriptCoreLib.Ultra.Studio
{
    public class SolutionProjectLanguageMethod
    {
        public const string op_Implicit = "op_Implicit";

        public const string ConstructorName = ".ctor";

        public string OperatorName;

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

        public bool IsOverride;
        public bool IsProtected;
        public bool IsPrivate;

        public bool IsStatic;

        public string Name;

        public string Summary;

        public readonly List<SolutionProjectLanguageArgument> Parameters = new List<SolutionProjectLanguageArgument>();

        SolutionProjectLanguageCode InternalCode;

        public SolutionProjectLanguageCode Code
        {
            get
            {
                return InternalCode;
            }
            set
            {
                InternalCode = value;
                value.OwnerMethod = this;
            }
        }

        public SolutionProjectLanguageType DeclaringType;

        public bool IsEvent;
        public bool IsProperty;
        public bool IsExtensionMethod;

        /// <summary>
        /// In FSharp we may need to use "|> ignore" operator
        /// </summary>
        public SolutionProjectLanguageType ReturnType;

        public bool IsFunction
        {
            get
            {
                if (this.IsConstructor)
                    return false;

                return this.ReturnType != null;
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public static class SolutionProjectLanguageMethodExtensions
    {
        public static PseudoCallExpression ToCallExpression(this SolutionProjectLanguageMethod Method, object Object = null, params object[] ParameterExpressions)
        {
            if (Method.IsExtensionMethod)
            {
                if (Object != null)
                {
                    // promote Object to arg0

                    return ToCallExpression(Method, null,
                        new[] { Object }.Concat(ParameterExpressions).ToArray()

                    );
                }
            }

            return new PseudoCallExpression
            {
                Method = Method,
                Object = Object,
                ParameterExpressions = ParameterExpressions
            };
        }
    }
}
