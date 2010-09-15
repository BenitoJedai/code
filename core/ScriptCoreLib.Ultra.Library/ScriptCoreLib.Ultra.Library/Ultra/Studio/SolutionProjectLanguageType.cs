using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio
{
    public class SolutionProjectLanguageType
    {
        public string Name;
        public string Namespace = "";


        // http://msdn.microsoft.com/en-us/library/dd233188.aspx

        public bool IsInternal;
        public bool IsStatic;
        public bool IsSealed;
        public bool IsInterface;



        public string Summary;

        public SolutionFileComment[] Comments;

        public SolutionProjectLanguageType ElementType;
        public SolutionProjectLanguageType DeclaringType;

        public readonly List<string> UsingNamespaces = new List<string>();

        public readonly List<SolutionProjectLanguageArgument> Arguments = new List<SolutionProjectLanguageArgument>();

        public readonly List<SolutionProjectLanguageProperty> Properties = new List<SolutionProjectLanguageProperty>();
        public readonly List<SolutionProjectLanguageMethod> Methods = new List<SolutionProjectLanguageMethod>();



        public static implicit operator SolutionFileWriteArguments(SolutionProjectLanguageType Type)
        {
            return new SolutionFileWriteArguments
            {
                Fragment = SolutionFileTextFragment.Type,
                Tag = Type,
                Text = Type.Name
            };
        }

        /// <summary>
        /// Partial types. When a language like FSharp does not support this feature
        /// it could simply pull the members.
        /// 
        /// http://stackoverflow.com/questions/793536/split-f-modules-across-multiple-files
        /// </summary>
        public SolutionProjectLanguagePartialType[] DependentPartialTypes = new SolutionProjectLanguagePartialType[0];


        public SolutionProjectLanguageType DependentUpon;

        public bool IsPartial
        {
            get
            {
                if (DependentPartialTypes.Any())
                    return true;

                if (DependentUpon == null)
                    return false;

                return DependentUpon.DependentPartialTypes.Any(k => k.Type == this);
            }
        }

        public SolutionProjectLanguageType BaseType;

        public readonly List<SolutionProjectLanguageField> Fields = new List<SolutionProjectLanguageField>();

        public string FullName
        {
            get
            {
                var w = new StringBuilder();

                if (!string.IsNullOrEmpty(Namespace))
                {
                    w.Append(Namespace);
                    w.Append(".");
                }

                w.Append(Name);

                return w.ToString();
            }
        }

        public override string ToString()
        {
            return this.FullName;
        }





        public SolutionProjectLanguageMethod GetDefaultConstructorDefinition()
        {
            return new SolutionProjectLanguageMethod
                       {
                           Name = SolutionProjectLanguageMethod.ConstructorName,

                           DeclaringType = this,
                           ReturnType = this,

                           Code = new SolutionProjectLanguageCode
                       {
                       }
                       };
        }

        public PseudoCallExpression GetDefaultConstructor()
        {
            return new PseudoCallExpression
                   {

                       Method = GetDefaultConstructorDefinition()
                       ,

                       ParameterExpressions = new object[] {
                        }
                   };
        }
    }

    public static class SolutionProjectLanguageTypeExtensions
    {
        public static SolutionProjectLanguageField ToInitializedField(this SolutionProjectLanguageType FieldType, string FieldName)
        {
            return  new SolutionProjectLanguageField
            {
                FieldType = FieldType,
                FieldConstructor = FieldType.GetDefaultConstructor(),
                Name = FieldName,
                IsReadOnly = true
            }; 
        }

        public static SolutionProjectLanguageProperty ToAutoProperty(this SolutionProjectLanguageType PropertyType, string PropertyName)
        {
            return new SolutionProjectLanguageProperty
            {
                Name = PropertyName,
                GetMethod = new SolutionProjectLanguageMethod(),
                SetMethod = new SolutionProjectLanguageMethod(),
                PropertyType = PropertyType
            };
        }
    }
}
