using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace jsc.meta.Library
{
	public static class MyExtensions
	{
		public static ConstructorInfo ToConstructorInfo<T1, T2>(this Func<T1, T2> signature)
		{
			return typeof(T2).GetConstructor(new[] { typeof(T1) });
		}

		public static void AsParametersFor(this string[] args, params Delegate[] x)
		{
			var e = x.Single(k => k.Method.Name == args.First());

			e.Method.Invoke(e.Target,
				Enumerable.ToArray(
					from p in e.Method.GetParameters()
					let prefix = "/" + p.Name + ":"
					select Enumerable.FirstOrDefault(
						from k in args
						where k.StartsWith(prefix)
						let n = k.Substring(prefix.Length)
						select
							p.ParameterType == typeof(FileInfo) ? (object)new FileInfo(n) :
							p.ParameterType == typeof(DirectoryInfo) ? (object)new DirectoryInfo(n) :
							(object)n
						)
				)
			);

		}

	}
}
