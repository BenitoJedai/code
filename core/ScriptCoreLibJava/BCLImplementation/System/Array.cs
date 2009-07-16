using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using System.Collections;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Array), IsArray = true)]
	internal class __Array
	{

		[Script]
		class __Enumerator : IEnumerator
		{
			public object[] Target;

			object InternalCurrent;
			int InternalIndex = -1;

			#region __IEnumerator Members

			public object Current
			{
				get { return InternalCurrent; }
			}

			public bool MoveNext()
			{
				InternalIndex++;

				if (InternalIndex < Target.Length)
				{
					InternalCurrent = Target[InternalIndex];
					return true;
				}

				InternalCurrent = null;
				return false;
			}

			public void Reset()
			{
				throw new NotImplementedException();
			}

			#endregion
		}

		[Script(DefineAsStatic = true)]
		public IEnumerator GetEnumerator()
		{
			return new __Enumerator { Target = (object[])(object)this };
		}

		public static void Sort(Array array)
		{
			java.util.Arrays.sort((object[])(object)array);
		}

		public static void Copy(__Array sourceArray, __Array destinationArray, int length)
		{
			java.lang.JavaSystem.arraycopy(sourceArray, 0, destinationArray, 0, length);
		}

		public static void Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
		{
			java.lang.JavaSystem.arraycopy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
		}

		public static Array CreateInstance(Type elementType, int length)
		{
			__Type t = elementType;
			var o = default(Array);

			try
			{
				o = (Array)java.lang.reflect.Array.newInstance(t.TypeDescription, length);
			}
			catch (csharp.ThrowableException e)
			{
				throw new csharp.RuntimeException(e.ToString());
			}

			return o;
		}
	}
}
