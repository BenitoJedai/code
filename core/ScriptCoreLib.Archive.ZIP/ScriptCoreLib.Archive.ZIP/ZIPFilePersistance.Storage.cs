using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.IO;

namespace ScriptCoreLib.Archive.ZIP
{
	partial class ZIPFilePersistance
	{




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




	}

}
