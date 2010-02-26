﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace jsc.meta.Library
{
	public static class BCLExtensions
	{

		public static byte[] ToBytes(this Stream s)
		{
			var x = new byte[s.Length];

			s.Read(x, 0, x.Length);

			return x;
		}

		public static bool IsInitializedDataFieldType(this Type SourceType)
		{
			return (SourceType.StructLayoutAttribute != null && SourceType.StructLayoutAttribute.Size > 0);
		}

		public static T ToConsole<T>(this T e) where T : class
		{
			Console.WriteLine(e);

			return e;
		}

		public static DirectoryInfo Create(this DirectoryInfo e, Func<DirectoryInfo> f)
		{
			if (e == null)
				return f();

			if (!e.Exists)
				e.Create();

			return e;
		}

		public static Type[] GetParameterTypes(this MethodBase e)
		{
			return e.GetParameters().Select(k => k.ParameterType).ToArray();
		}

		public static Type[] GetSignatureTypes(this MethodInfo e)
		{
			return e.GetParameters().Select(k => k.ParameterType).Concat(new[] { e.ReturnType }).ToArray();
		}

	}
}
