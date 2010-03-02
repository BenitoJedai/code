using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.IO;

namespace ScriptCoreLib.Archive.ZIP
{
	/// <summary>
	/// This type will provide abstraction to measure the time taken by single tasks, present the opportunity to decide to
	/// suspend currenct execution of tasks just to be resumed later
	/// </summary>
	public partial class ZIPFilePersistance : IDisposable
	{

		public delegate void StringTimeSpanAction(string e, TimeSpan elapsed);
		public delegate void StringAction(string e);


		public delegate string StringFunc();


		public delegate void ByteArrayAction(byte[] e);


		public delegate byte[] ByteArrayFunc();


		public delegate bool BooleanFunc();



		public int Timeout;

		public readonly Stopwatch Stopwatch = new Stopwatch();
		public readonly ZIPFile Content = new ZIPFile();

		public ZIPFilePersistance()
		{
			const string InternalProperties = null;

			this.InternalData = this.GetProperty("Data", InternalProperties);
			this.InternalDescription = this.GetProperty("Description", InternalProperties);
			this.InternalIdentity = this.GetProperty("Identity", InternalProperties);
			this.InternalCounter = this.GetProperty("Counter", InternalProperties);
			this.InternalElapsedAtFault = this.GetProperty("ElapsedAtFault", InternalProperties);

			this.ElapsedAtFault = 0;

			Stopwatch.Start();
		}

		public event StringAction TaskSkipped;
		public event StringAction TaskEnqeued;
		public event StringAction TaskStarted;
		public event StringAction TaskFault;
		public event StringTimeSpanAction TaskCompleted;

		public int CountTaskEnqeued;
		readonly ArrayList InternalTasks = new ArrayList();


		public class TaskInfo
		{
			public string TaskName;
			public Property Elapsed;
		}

		public TaskInfo[] Tasks
		{
			get
			{
				return (TaskInfo[])InternalTasks.ToArray(typeof(TaskInfo));
			}
		}

		public TaskControlAction this[Property Task, string SubTask, PropertyFilterFunc Filter]
		{
			set
			{
				this[Task.Name, Path.Combine(SubTask, "Filter")] =
					c =>
					{
						if (Filter(Task))
							return;

						c.Fault = true;
					};

				this[Task.Name, SubTask] = value;
			}
		}

		public TaskControlAction this[string TaskName, string SubTask]
		{
			set
			{
				this[Path.Combine(TaskName, SubTask)] = value;
			}
		}


		public class TaskControl
		{
			public string Name;

			/// <summary>
			/// When set to true this task is to be retried. The time elapsed will be added to the 
			/// generic ElapsedAtFault time counter.
			/// </summary>
			public bool Fault;
		}

		public delegate void TaskControlAction(TaskControl c);

		public TaskControlAction this[string TaskName]
		{
			set
			{
				var Category = "Tasks/" + TaskName;

				var TaskHandler = Category + "/";

				if (this.Content.Contains(TaskHandler))
				{
					AddTaskInfo(TaskName, Category, new TaskInfo());

					if (this.TaskSkipped != null)
						this.TaskSkipped(TaskName);

					return;
				}

				while (InternalInvokeTask(TaskName, value, Category, TaskHandler));

			}
		}

		private bool InternalInvokeTask(string TaskName, TaskControlAction value, string Category, string TaskHandler)
		{

			if (!this.PersistanceDisabled)
				if (this.Stopwatch.ElapsedMilliseconds > Timeout)
				{
					CountTaskEnqeued++;

					if (this.TaskEnqeued != null)
						this.TaskEnqeued(TaskName);

					return false;
				}



			if (this.TaskStarted != null)
				this.TaskStarted(TaskName);

			var c = new TaskControl();

			var s = new Stopwatch();
			s.Start();
			try
			{
				value(c);
			}
			catch
			{
				// in case of errors we will mark this workflow not to be persisted anymore...
				PersistanceDisabled = true;
				// we might want to store the current exception...
				throw new InvalidOperationException();
			}

			s.Stop();

			if (c.Fault)
			{
				if (this.TaskFault != null)
					this.TaskFault(TaskName);

				this.ElapsedAtFault += (uint)s.ElapsedMilliseconds;

				return true;
			}

			this.Content.Add(TaskHandler);
			var i = new TaskInfo();
			AddTaskInfo(TaskName, Category, i);
			i.Elapsed.ValueUInt32 = (uint)s.ElapsedMilliseconds;

			if (this.TaskCompleted != null)
				this.TaskCompleted(TaskName, s.Elapsed);

			return false;
		}

		private void AddTaskInfo(string TaskName, string Category, TaskInfo i)
		{
			i.TaskName = TaskName;
			i.Elapsed = GetProperty("Elapsed", Category);

			InternalTasks.Add(i);
		}

		readonly Property InternalIdentity;
		public string Identity
		{
			get
			{
				return this.InternalIdentity.Text;
			}
			set
			{
				this.InternalIdentity.Text = value;
			}
		}

		readonly Property InternalDescription;
		public string Description
		{
			get
			{
				return this.InternalDescription.Text;
			}
			set
			{
				this.InternalDescription.Text = value;
			}
		}

		readonly Property InternalCounter;
		public uint Counter
		{
			get
			{
				return this.InternalCounter.ValueUInt32;
			}
			set
			{
				this.InternalCounter.ValueUInt32 = value;
			}
		}

		readonly Property InternalElapsedAtFault;
		public uint ElapsedAtFault
		{
			get
			{
				return this.InternalElapsedAtFault.ValueUInt32;
			}
			set
			{
				this.InternalElapsedAtFault.ValueUInt32 = value;
			}
		}


		readonly Property InternalData;
		public byte[] Data
		{
			get
			{
				return this.InternalData.Bytes;
			}
			set
			{
				this.InternalData.Bytes = value;
			}
		}

		protected void WriteToStorage()
		{
			var s = new MemoryStream();
			Content.WriteTo(new BinaryWriter(s));
			this.Archive.Writer(s.ToArray());
		}

		public bool PersistanceDisabled;

		public bool PersistanceRequested;

		public bool PersistanceRequired
		{
			get
			{
				if (PersistanceDisabled)
					return false;

				if (PersistanceRequested)
					return true;

				if (this.Stopwatch.ElapsedMilliseconds > Timeout)
					if (CountTaskEnqeued > 0)
						return true;

				return false;
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			Stopwatch.Stop();

			if (PersistanceRequired)
				this.WriteToStorage();
		}

		#endregion

		Storage InternalArchive;
		public Storage Archive
		{
			get
			{
				return InternalArchive;
			}
			set
			{
				InternalArchive = value;

				if (InternalArchive.Exists())
				{
					foreach (ZIPFile.Entry k in (ZIPFile)new MemoryStream(InternalArchive.Reader()))
					{
						if (this.Content.Contains(k.FileName))
						{
							var c = this.Content[k.FileName];

							if (c.Data.Length == 0)
								c.Data = k.Data;
						}
						else
						{
							this.Content[k.FileName].Data = k.Data;
						}
					}
					Counter++;
					InternalArchive.Delete();
				}
				else
				{
					Counter = 0;
				}
			}
		}


		
	}

}
