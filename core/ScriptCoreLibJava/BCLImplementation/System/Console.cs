using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(Console))]
	internal class __Console
	{
		// http://java.sun.com/javase/6/docs/api/java/text/Normalizer.html
		// http://stackoverflow.com/questions/1272032/java-utf-8-strange-behaviour
		// http://bugs.sun.com/bugdatabase/view_bug.do?bug_id=4038677
		// http://blogs.msdn.com/oldnewthing/archive/2005/08/29/457483.aspx

		static string InternalGetEnvironmentEncoding()
		{
			// http://en.wikipedia.org/wiki/Code_page

			// yay, this was fixed for java6...
			// http://bugs.sun.com/bugdatabase/view_bug.do?bug_id=4153167


			var u = java.lang.JavaSystem.getProperty("file.encoding");

			// default:
			if (string.IsNullOrEmpty(u))
				return "UTF-8";

			// translate Windows (ANSI) code pagesto IBM PC (OEM) code pages

			// baltic
			if ("Cp1257" == u) return "Cp775";

			// ...

			return u;
		}

		static java.io.PrintStream InternalOutCache;
		static java.io.PrintStream InternalOut
		{
			get
			{
				try
				{
					if (InternalOutCache == null)
						InternalOutCache = new java.io.PrintStream(java.lang.JavaSystem.@out, true,
							InternalGetEnvironmentEncoding()
						);
				}
				catch
				{
					throw new InvalidOperationException();
				}

				return InternalOutCache;
			}
		}

		public static void Beep()
		{
			global::java.awt.Toolkit.getDefaultToolkit().beep();
		}

		public static void Write(string p)
		{
			InternalOut.print(p);
		}

		public static void Write(char c)
		{
			Write(new string(new char[] { c }));
		}


		public static void WriteLine()
		{
			WriteLine("");
		}

		public static void WriteLine(object e)
		{
			WriteLine("" + e);
		}

		public static void WriteLine(string e)
		{
			InternalOut.println(e);
		}

		public static string ReadLine()
		{
			string z = null;

			try
			{
				var r0 = new global::java.io.InputStreamReader(global::java.lang.JavaSystem.@in);
				var r1 = new global::java.io.BufferedReader(r0);


				z = r1.readLine();
			}
			catch (Exception)
			{

			}

			return z;
		}

	}
}
