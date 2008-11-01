using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ScriptCoreLib
{

	[global::System.AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
	public sealed class ScriptTypeFilterAttribute : Attribute
	{
		public override string ToString()
		{
			return "[ScriptType: " + Enum.GetName(typeof(ScriptType), Type) + " Filter: '" + Filter + "']";
		}

		public string FilterTypeName
		{
			get
			{
				return Enum.GetName(typeof(ScriptType), Type);
			}
		}

		public readonly ScriptType Type;
		public readonly string Filter;

		public ScriptTypeFilterAttribute(ScriptType e)
			: this(e, "*")
		{
		}

		public ScriptTypeFilterAttribute(ScriptType e, Type f) : this(e, f.Namespace)
		{

		}

		public ScriptTypeFilterAttribute(ScriptType e, string f)
		{
			Type = e;
			Filter = f;
		}

		public bool MatchType(Type e)
		{
			// should do regex



			string[] a = Filter.Split('.');

			string[] b = new string[] { };

			if (e.Namespace != null)
				b = e.Namespace.Split('.');

			bool w = false;

			int i = -1;
			foreach (string n in a)
			{
				i++;

				if (n == "*")
					continue;

				if (b.Length <= i)
					return false;

				string z = b[i];
				string x = a[i];


				if (z != x)
				{
					if (z.ToLower() == x.ToLower())
					{
						w = true;
					}
					else
						return false;
				}
			}

			if (w)
			{
				throw new Exception("Namespace differs only by case. " + e.FullName);

			}

			return true;
		}


		internal static ScriptTypeFilterAttribute[] ArrayOf(Assembly a, ScriptType _scripttype)
		{
			List<ScriptTypeFilterAttribute> list = new List<ScriptTypeFilterAttribute>();

			ScriptTypeFilterAttribute[] f = a.GetCustomAttributes(typeof(ScriptTypeFilterAttribute), false) as ScriptTypeFilterAttribute[];

			if (f != null)
			{
				foreach (ScriptTypeFilterAttribute x in f)
				{
					if (x.Type == _scripttype)
						list.Add(x);

				}
			}

			return list.ToArray();
		}
	}

}
