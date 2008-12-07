using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace jsc.Languages
{
	public class PropertyDetector
	{
		public PropertyInfo SetProperty;
		public PropertyInfo GetProperty;

		public Type PropertyType
		{
			get
			{
				if (SetProperty != null)
					return SetProperty.PropertyType;

				if (GetProperty != null)
					return GetProperty.PropertyType;

				return null;
			}
		}

		public PropertyDetector(MethodBase m)
		{
			//if (m.IsConstructor)
				//return;

			var mi = m as MethodInfo;
			
			if (mi == null)
				return;

			var any = BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

			#region set
			{
				var prefix = "set_";
				if (m.Name.StartsWith(prefix))
				{
					try
					{
						var Types = new Stack<Type>(mi.GetParameters().Select(k => k.ParameterType));
						var Types_Return = Types.Pop();
						var Types_Params = Types.ToArray();

						//SetProperty = m.DeclaringType.GetProperty(m.Name.Substring(prefix.Length), any);
						SetProperty = m.DeclaringType.GetProperty(m.Name.Substring(prefix.Length), any,
							null,
							Types_Return,
							Types_Params,
							null
						);

					}
					catch (AmbiguousMatchException)
					{

					}
				}
			}
			#endregion

			#region get
			{
				var prefix = "get_";
				if (m.Name.StartsWith(prefix))
				{
					try
					{
						//GetProperty = m.DeclaringType.GetProperty(m.Name.Substring(prefix.Length), any);
						GetProperty = m.DeclaringType.GetProperty(m.Name.Substring(prefix.Length), any,
							null,
							mi.ReturnType,
							mi.GetParameters().Select(k => k.ParameterType).ToArray(),
							null
						);
					}
					catch (AmbiguousMatchException)
					{

					}
				}
			}
			#endregion

		}

		public static bool IsProperty(MethodBase zfn)
		{
			var v = new PropertyDetector(zfn);

			if (v.SetProperty != null)
				return true;

			if (v.GetProperty != null)
				return true;

			return false;
		}
	}
}
