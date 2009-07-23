using System.Threading;
using System;
using ScriptCoreLib;
using System.IO;
using ScriptCoreLib.Archive.ZIP;
using System.Diagnostics;
using System.Collections;

namespace SimpleWorkflowExample
{

	[Script]
	public class Program
	{
		public static void Main(string[] args)
		{
			// Use Release Build to use jsc to generate java program
			// Use Debug Build to develop on .net

			Console.WriteLine("SimpleWorkflowExample. Crosscompiled from C# to Java.");

			var Archive = new DirectoryInfo("archive");

			ContinueViaFastLane(new byte[] { /* WorkIdentity */ 5,  /* Data */ 5, 6, 7, 8 }, 0, Archive);

			ContinueAt(15);

			ContinueViaFastLane(new byte[0], /* WorkIdentity */ 5, Archive);

			ContinueAt(15);

			ContinueViaFastLane(new byte[0], /* WorkIdentity */ 5, Archive);


		}

		private static void ContinueAt(int j)
		{
			var i = j;

			Console.WriteLine();
			while (i > 0)
			{
				Console.WriteLine("Continue in " + i + " sec...");
				Thread.Sleep(1000);
				i--;
			}
			Console.WriteLine();
		}

		public static void ContinueViaFastLane(byte[] Data, byte WorkIdentity, DirectoryInfo Archive)
		{
			#region Infer WorkIdentity
			if (Data.Length > 0)
				WorkIdentity = Data[0];
			#endregion


			const string FileExtension = ".zip";

			using (
				var f = new ZIPFilePersistance
				{
					Timeout = 1100,

					// are we starting the job?
					Data = Data,

					Identity = "" + WorkIdentity,
					Description = "This file contains a suspended workflow for SimpleWorkflowExample. The files cannot be compressed.",

					Archive = ZIPFilePersistance.Storage.Of(
						Path.Combine(Archive.FullName, WorkIdentity + FileExtension),
						File.Exists,
						File.Delete,
						File.ReadAllBytes,
						File.WriteAllBytes
					)
				}
			)
			{
				if (f.Data.Length == 0)
				{
					Console.WriteLine("WorkIdentity cannot be resumed at this time...");
				}
				else
				{
					#region info
					f.TaskEnqeued +=
						n =>
						{
							Console.WriteLine("TaskEnqeued: " + n);
						};

					f.TaskCompleted +=
						n =>
						{
							Console.WriteLine("TaskCompleted: " + n);
						};

					f.TaskSkipped +=
						n =>
						{
							Console.WriteLine("TaskSkipped: " + n);
						};
					#endregion

					var result1 = f.GetProperty("result1");
					var result2 = f.GetProperty("result2");



					f["prep1"] = delegate
					{
						Console.WriteLine("prep1 begins");
						Thread.Sleep(1200);

						result1.Text = "prep1 result";

						Console.WriteLine("prep1 ends at " + f.Stopwatch.ElapsedMilliseconds + "ms");

					};

					f["prep2"] = delegate
					{
						Console.WriteLine("prep2 begins");
						Thread.Sleep(1200);

						result2.Text = "prep2 result";

						Console.WriteLine("prep2 ends");

						// we can decide not to allow persistance to kick in on us...
						f.PersistanceDisabled = true;

					};

					f["prep3"] = delegate
					{
						Console.WriteLine("prep3 at " + f.Stopwatch.ElapsedMilliseconds + "ms");

						Console.WriteLine(result1);
						Console.WriteLine(result2);

					};

					foreach (var t in f.Tasks)
					{
						Console.WriteLine("Statistics: " + t.TaskName + " completed in " + t.Elapsed.ValueUInt32 + "ms");
					}


					if (f.PersistanceRequired)
						Console.WriteLine("Tasks will be saved... " + f.Stopwatch.ElapsedMilliseconds + "ms");
					else
						Console.WriteLine("Tasks completed");
				}
			}
		}

	}


}
