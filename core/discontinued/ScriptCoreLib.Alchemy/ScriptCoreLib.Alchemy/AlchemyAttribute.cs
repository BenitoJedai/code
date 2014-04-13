﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Alchemy
{
	public class AlchemyAttribute : Attribute
	{
		public Type TargetType;
		
		public AlchemyAttribute(Type t)
		{
			this.TargetType = t;
		}
	}
}
