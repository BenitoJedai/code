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
	
	public class ZIPFilePersistance : IDisposable
	{
		
		public delegate void StringAction(string e);

		
		public delegate string StringFunc();

		
		public delegate void ByteArrayAction(byte[] e);

		
		public delegate byte[] ByteArrayFunc();

		
		public delegate bool BooleanFunc();

		
		public delegate bool StorageExists();

		
		public delegate byte[] StorageReader();

		
		public delegate void StorageWriter(byte[] bytes);

		
		public delegate bool StorageTargetExists(string Target);

		
		public delegate byte[] StorageTargetReader(string Target);

		
		public delegate void StorageTargetWriter(string Target, byte[] bytes);

		
		public sealed class Storage
		{
			public Action Delete;
			public StorageExists Exists;
			public StorageReader Reader;
			public StorageWriter Writer;

			public static Storage Of(string Target, StorageTargetExists Exists, StringAction Delete, StorageTargetReader Reader, StorageTargetWriter Writer)
			{
				return new Storage
				{
					Delete = () => Delete(Target),
					Exists = () => Exists(Target),
					Reader = () => Reader(Target),
					Writer = bytes => Writer(Target, bytes)
				};
			}
		}

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
			
			this.Counter = 0;

			Stopwatch.Start();
		}

		public event StringAction TaskSkipped;
		public event StringAction TaskEnqeued;
		public event StringAction TaskCompleted;

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

		public Action this[string TaskName]
		{
			set
			{
				var Category = "Tasks/" + TaskName;

				var n = Category + "/";
				var i = new TaskInfo();

				if (this.Content.Contains(n))
				{
					AddTaskInfo(TaskName, Category, i);

					if (this.TaskSkipped != null)
						this.TaskSkipped(TaskName);

					return;
				}

				if (!this.PersistanceDisabled)
					if (this.Stopwatch.ElapsedMilliseconds > Timeout)
					{
						CountTaskEnqeued++;

						if (this.TaskEnqeued != null)
							this.TaskEnqeued(TaskName);

						return;
					}

				this.Content.Add(n);

				AddTaskInfo(TaskName, Category, i);


				var s = new Stopwatch();
				s.Start();
				value();
				s.Stop();

				i.Elapsed.ValueUInt32 = (uint)s.ElapsedMilliseconds;

				if (this.TaskCompleted != null)
					this.TaskCompleted(TaskName);
			}
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

		public bool PersistanceRequired
		{
			get
			{
				if (PersistanceDisabled)
					return false;

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
			}
		}

		
		public class Property
		{
			
			public class Handlers
			{
				public StringAction set_TextHandler;
				public StringFunc get_TextHandler;

				public ByteArrayAction set_BytesHandler;
				public ByteArrayFunc get_BytesHandler;
			}

			readonly Handlers InternalHandlers;

			public Property(Handlers h)
			{
				this.InternalHandlers = h;
			}

			public string Text
			{
				get
				{
					return this.InternalHandlers.get_TextHandler();
				}
				set
				{
					this.InternalHandlers.set_TextHandler(value);
				}
			}

			public byte[] Bytes
			{
				get
				{
					return this.InternalHandlers.get_BytesHandler();
				}
				set
				{
					this.InternalHandlers.set_BytesHandler(value);
				}
			}

			public uint ValueUInt32
			{
				get
				{
					var r = new BinaryReader(new MemoryStream(Bytes));

					return r.ReadUInt32();
				}
				set
				{
					var m = new MemoryStream();
					var w = new BinaryWriter(m);
					w.Write((int)value);

					this.Bytes = m.ToArray();
				}
			}

			public override string ToString()
			{
				return this.Text;
			}
		}

		public Property GetProperty(string name)
		{
			return GetProperty(name, "Properties");
		}

		public Property GetProperty(string name, string category)
		{
			var TextHandler = category.CombinePath(name + ".txt");
			var BytesHandler = category.CombinePath(name + ".bin");

			var h = new Property.Handlers
			{
				get_TextHandler = () => this.Content[TextHandler].Text,
				set_TextHandler = value => this.Content[TextHandler].Text = value,

				get_BytesHandler = () => this.Content[BytesHandler].Bytes,
				set_BytesHandler = value => this.Content[BytesHandler].Bytes = value


			};

			return new Property(h);

		}
	}

}
