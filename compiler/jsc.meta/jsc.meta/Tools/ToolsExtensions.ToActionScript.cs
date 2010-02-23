using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using jsc.meta.Library.VolumeFunctions;
using jsc.meta.Library;

namespace jsc.meta.Tools
{
	static partial class ToolsExtensions
	{


		public static void ToActionScript(
			this FileInfo TargetAssembly_,
			FileInfo mxmlc,
			FileInfo flashplayer,
			Type sprite,
			FileInfo FusionAssembly,
			string ProductName,

			Action<string> RaiseProccessStatusChanged
			)
		{
			if (mxmlc == null)
				throw new ArgumentNullException();

			using (var v = TargetAssembly_.ToVirtualDrive())
			{
				var TargetAssembly = v.VirtualFile;

				// we need to use subst to overcome long path limitations!

				// http://forum.sysinternals.com/forum_posts.asp?TID=9063&amp;PN=1
				// http://msdn.microsoft.com/en-us/library/aa363904(VS.85).aspx
				// http://www.ntwind.com/software/utilities/visual-subst.html

				// FusionAssembly ??

				jsc.Program.TypedMain(
					new jsc.CompileSessionInfo
					{
						Options = new jsc.CommandLineOptions
						{
							TargetAssembly = TargetAssembly,
							IsActionScript = true,
							IsNoLogo = true,
						}.Apply(
							k =>
							{
								if (RaiseProccessStatusChanged != null)
									k.ProccessStatusChanged +=
										e =>
										{
											RaiseProccessStatusChanged(e);
										};
							}
						)
					}
				);

				var obj_web = Path.Combine(TargetAssembly.Directory.FullName, "web");


				#region mxmlc

				//// each jar file which has made it to the bin folder
				//// needs to get referenced in our java build
				//foreach (var r in from k in Directory.GetFiles(obj_web_bin, "*.jar")
				//                  where k != bin_jar.FullName
				//                  select k)
				//{
				//    TargetSourceFiles += ";" + Path.Combine("bin", Path.GetFileName(r));
				//}

				var obj_web_swf = Path.Combine(obj_web,
					ProductName ?? sprite.Name + ".swf"
				);


				if (RaiseProccessStatusChanged != null)
					RaiseProccessStatusChanged("mxmlc");

				var proccess_mxmlc = Process.Start(
					new ProcessStartInfo(
						mxmlc.FullName,
						"-sp=. -strict -output=\"" +
							obj_web_swf + "\" "
							+ sprite.FullName.Replace(".", @"\").Replace("+", "_") + @".as"
						)
					{
						UseShellExecute = false,

						WorkingDirectory = obj_web
					}
				);

				proccess_mxmlc.WaitForExit();

				if (proccess_mxmlc.ExitCode != 0)
					throw new ArgumentOutOfRangeException();

				var m = new MemoryStream();
				var w = new BinaryWriter(m);
				w.Write(File.ReadAllBytes(flashplayer.FullName));
				w.Write(File.ReadAllBytes(obj_web_swf));
				w.Write((uint)0xFA123456);
				w.Write((uint)new FileInfo(obj_web_swf).Length);

				File.WriteAllBytes(obj_web_swf + ".exe", m.ToArray());
				#endregion

			}

			// call C:\util\flex\bin\mxmlc.exe -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as

		}
	}
}
