using ScriptCoreLib;
using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;

namespace ScriptCoreLib.Shared.Lambda
{
	partial class LambdaExtensions
	{
		[Script]
		public class FlaggableStream
		{
			public bool SkipElements;
		}

		[Script]
		public class FlaggableElement<T>
		{
			public readonly T Current;
			public readonly FlaggableStream Stream;

			public bool SkipAtNextIteration;


			public FlaggableElement(T Current, FlaggableStream Stream)
			{
				this.Current = Current;
				this.Stream = Stream;
			}
		}

		/// <summary>
		/// Enables to skip items on next iteration or all items at once
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static IEnumerable<FlaggableElement<T>> ToFlaggable<T>(this IEnumerable<T> source)
		{
			var s = new FlaggableStream();
			var a = source.ToArray(k => new FlaggableElement<T>(k, s));

			return from k in a
				   where !k.SkipAtNextIteration
				   where !k.Stream.SkipElements
				   select k;
		}
	}
}
