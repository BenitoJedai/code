using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.YAML
{
	internal static class YAMLExtensions
	{
		public delegate void ApplyActionDelegate(Action a);

		public static Action ApplyAction(this Action a, ApplyActionDelegate h)
		{
			return () => h(a);
		}

		public static void SetFieldValue(this Type t, string FieldName, object target, string FieldValue)
		{
			foreach (var f in t.GetFields())
			{
				if (f.Name == FieldName)
					f.SetValue(target, FieldValue);
			}
		}
	}


}
