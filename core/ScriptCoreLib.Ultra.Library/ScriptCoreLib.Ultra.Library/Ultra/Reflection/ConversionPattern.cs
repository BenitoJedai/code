using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ScriptCoreLib.Ultra.Reflection
{
	public class ConversionPattern
	{
		public readonly MethodInfo LocalToTarget;
		public readonly MethodInfo TargetToLocal;

		public ConversionPattern(Type LocalType, Type TargetType)
		{

			Func<Type[], IEnumerable<MethodInfo>> f = z =>
				from m in LocalType.GetMethods(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public)
				let p = m.GetParameters()
				where p.Length == 1
				let r = m.ReturnType
				let a = p[0].ParameterType
				where new[] { r, a }.SequenceEqual(z)
				select m;

			this.TargetToLocal = f(new[] { LocalType, TargetType }).FirstOrDefault();
			this.LocalToTarget = f(new[] { TargetType, LocalType }).FirstOrDefault();
		}

		public bool IsValid
		{
			get
			{
				if (this.TargetToLocal == null)
					return false;

				if (this.LocalToTarget == null)
					return false;

				return true;
			}

		}
	}
}
