using System.Threading;
using System;

using ScriptCoreLib;
using java.lang;
using java.net;
using java.io;
using java.lang.reflect;


namespace NativePluginContainer
{
	[Script]
	public class Program
	{


		public static void Main(string[] args)
		{
			// http://java.sun.com/docs/books/jni/html/jniTOC.html

			Console.WriteLine("this example is a workaround for: 'already loaded in another classloader' error");
			Console.WriteLine("each plugin will use its own version of a native");

			var x = new PluginMainDelegate
			{
				AssemblyPath = "NativePluginExample_x.jar",
				TypeName = "ExampleCompany.Program",
				NativeAssemblyPath = "lib_jnistb10_x",
			};
			
			// the first call will load the native api
			x.Invoke();
			x.Invoke();

			new PluginMainDelegate
			{
				AssemblyPath = "NativePluginExample_y.jar",
				TypeName = "ExampleCompany.Program",
				NativeAssemblyPath = "lib_jnistb10_y",
			}.Invoke();


		}
	}

	[Script]
	public class PluginMainDelegate
	{
		public string AssemblyPath;
		public string TypeName;

		
		public string NativeAssemblyPath;

		Method Method;

		
		public void Invoke(params string[] args)
		{
			try
			{
				// http://bigjavablog.blogspot.com/2008/08/load-jar-files-and-java-classes.html

				if (Method != null)
				{
					var main_args = new string[0];

					this.Method.invoke(null, new object[] { main_args });
				}
				else
				{
					/* We need and URL to load the jar file. */
					URL u = new File(this.AssemblyPath).toURL();
					/* Load jar file using URLClassLoader. */
					URLClassLoader cl = new URLClassLoader(new URL[] { u });

					if (NativeAssemblyPath != null)
					{
						var nc = cl.loadClass("jni.CPtrLibrary");
						var ncf = nc.getField("LibraryPath");

						ncf.set(null, NativeAssemblyPath);
					}

					var c = cl.loadClass(this.TypeName);

					foreach (var m in c.getMethods())
					{
						if (m.getDeclaringClass() == c)
						{
							if (m.getName() == "main")
							{
								this.Method = m;

								var main_args = new string[0];

								this.Method.invoke(null, new object[] { main_args });
							}
						}
					}
				}

			}
			catch (csharp.ThrowableException ex)
			{
				var exo = (object)ex;

				Console.WriteLine("error!");
				Console.WriteLine(ex.Message + " - " + ex.ToString());

				ErrorHandler(exo);

			}
		}

		private static void ErrorHandler(object exo)
		{


			if (exo is InvocationTargetException)
			{
				var ext = (InvocationTargetException)exo;


				ext.printStackTrace();

				var TargetException = ext.getTargetException();

				Console.WriteLine("InvocationTargetException: " + TargetException.getMessage());

				TargetException.printStackTrace();

			}
			
		}
	}
}
