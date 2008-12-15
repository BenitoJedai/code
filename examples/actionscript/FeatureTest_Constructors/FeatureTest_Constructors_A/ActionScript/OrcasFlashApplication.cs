using ScriptCoreLib;
using System.Collections.Generic;
using System;
using System.IO;
using ScriptCoreLib.ActionScript;

namespace FeatureTest_Constructors_A.ActionScript
{

	[Script]
	public class A
	{
		public A(string a)
		{

		}
	}

#if T2
	// this will fail intentionally

	[Script]
	public class B
	{
		public B(object a)
		{
			var i = 2;
		}

		public B(string a)
		{
			var i = 1;
		}
	}
#endif

	[Script]
	public class C
	{
		public C()
			: this("constant")
		{
		}

		public C(string a)
		{
			var i = 1;
		}
	}

	[Script]
	public class T2
	{

	}

	[Script]
	public class T3 : T2
	{
		string A;
		int B;

		void C()
		{

		}
	}

	[Script]
	public class T4
	{
		int C(bool a)
		{
			if (a)
				return 1;

			return 0;
		}
	}


	[Script]
	public class T5
	{
		int C(bool a)
		{
			var v = 0;

			if (a)
				v = 1;
			else
				v = -1;

			return v;
		}
	}

	[Script]
	public class T6
	{
		void C()
		{
			for (int i = 0; i < 10; i++)
			{

			}
		}
	}

	[Script]
	public class T7
	{
		void C()
		{
			while (true)
			{

			}
		}
	}

	[Script]
	public class T8
	{
		void C()
		{
			try
			{

			}
			catch
			{

			}
			finally
			{

			}
		}
	}

	[Script]
	public class T9
	{
		void C()
		{
			var a = new
				global::FeatureTest_Reference1.ActionScript.
				FeatureTest_Reference1();


		}
	}

	[Script]
	public class T10
	{
		[Script(Implements = typeof(global::System.Math))]
		internal class __Math
		{
			public static double Sin(double a)
			{ return 0; }
			public static double Cos(double a)
			{ return 0; }
			public static double Tan(double a)
			{ return 0; }
		}

		void C()
		{
			var s = global::System.Math.Sin(1);
		}
	}

	[Script]
	public class T11 : IDisposable
	{
		[Script(Implements = typeof(global::System.IDisposable))]
		public interface __IDisposable
		{
			void Dispose();
		}

		
		static void C()
		{
			using (var s = new T11())
			{

			}
		}

		#region IDisposable Members

		public void Dispose()
		{
		}

		#endregion
	}
}