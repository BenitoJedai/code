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

			while (true)
			{
				ContinueAt(3);

				ContinueViaFastLane(new byte[0], /* WorkIdentity */ 5, Archive);
			}


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
					),

					PersistanceDisabled = false
				}
			)
			{
				if (f.Data.Length == 0)
				{
					Console.WriteLine("WorkIdentity cannot be resumed at this time...");
				}
				else
				{
					Console.WriteLine("Counter: " + f.Counter);

					#region info
					f.TaskEnqeued +=
						n =>
						{
							Console.WriteLine("TaskEnqeued: " + n);
						};

					f.TaskFault +=
						n =>
						{
							Console.WriteLine("TaskFault: " + n);
						};

					f.TaskCompleted +=
						(n, elapsed) =>
						{
							Console.WriteLine("TaskCompleted: " + n + " in " + elapsed.TotalMilliseconds + "ms");
						};

					f.TaskSkipped +=
						n =>
						{
							Console.WriteLine("TaskSkipped: " + n);
						};
					#endregion

					var result1 = f.GetProperty("result1");
					var result1_1 = result1["output"];
					var result2 = f.GetProperty("result2");

					f["prep0"] = delegate
					{
						Thread.Sleep(1200);
					};

					f["prep1"] = delegate
					{
						Console.WriteLine("prep1 begins");
						Thread.Sleep(1200);

						result1.Text = "prep1 result";
						result1_1.Text = "this is additional output";

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

					var WillFault = true;
					f["prep3"] = c =>
					{
						Console.WriteLine("prep3 at " + f.Stopwatch.ElapsedMilliseconds + "ms");

						Console.WriteLine(result1.Text);
						Console.WriteLine(result2.Text);

						c.Fault = WillFault;
						WillFault = false;
						Thread.Sleep(500);

					};

					uint Total = 0;
					foreach (var t in f.Tasks)
					{
						Total += t.Elapsed.ValueUInt32;
						Console.WriteLine("Statistics: " + t.TaskName + " completed in " + t.Elapsed.ValueUInt32 + "ms");
					}

					Console.WriteLine("Statistics: time elapsed at fault " + f.ElapsedAtFault);

					Total += f.ElapsedAtFault;

					Console.WriteLine("Statistics: Personalization completed in " + Total + "ms in " + f.Counter + " iterations");



					if (f.PersistanceRequired)
						Console.WriteLine("Tasks will be saved... " + f.Stopwatch.ElapsedMilliseconds + "ms");
					else
						Console.WriteLine("Tasks completed");
				}
			}
		}

	}


}
