﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq
{
    // https://github.com/dotnet/corefx/blob/master/src/System.Xml.XDocument/System/Xml/Linq/XComment.cs

    [Script(Implements = typeof(XComment))]
	internal class __XComment : __XNode
	{
		public __XComment(string value)
		{
			this.Value = value;
		}

		string InternalCommentData;
		public string Value
		{
			get
			{
				if (this.InternalValue == null)
					return InternalCommentData;

				return ((ICommentNode)this.InternalValue).nodeValue;
			}
			set
			{
				this.InternalCommentData = value;
			}
		}
	}
}
