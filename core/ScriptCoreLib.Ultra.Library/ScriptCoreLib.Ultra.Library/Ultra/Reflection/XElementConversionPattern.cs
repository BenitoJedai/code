using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace ScriptCoreLib.Ultra.Reflection
{
	public class XElementConversionPattern : ConversionPattern
	{
		public MethodInfo ToXElement
		{
			get
			{
				return this.LocalToTarget;
			}
		}

		public MethodInfo FromXElement
		{
			get
			{
				return this.TargetToLocal;
			}
		}

		public XElementConversionPattern(Type LocalType)
			: base(LocalType, typeof(XElement))
		{

		}
	}
}
