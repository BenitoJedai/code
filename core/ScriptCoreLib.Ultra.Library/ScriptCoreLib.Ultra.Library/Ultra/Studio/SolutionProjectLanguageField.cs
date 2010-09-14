﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio
{
	public class SolutionProjectLanguageField
	{
        public bool IsReadOnly;
		public bool IsPrivate;

		public string Summary;

		public string Name;

		public SolutionProjectLanguageType FieldType;

        public PseudoCallExpression FieldConstructor;


        SolutionProjectLanguageType InternalDeclaringType;
        public SolutionProjectLanguageType DeclaringType
        {
            get
            {
                return InternalDeclaringType;
            }
            set
            {
                if (InternalDeclaringType != null)
                    InternalDeclaringType.Fields.Remove(this);

                InternalDeclaringType = value;

                if (InternalDeclaringType != null)
                    InternalDeclaringType.Fields.Add(this);

            }
        }
	}
}
