using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.Shared.Archive
{
	[Script]
	[Obsolete("Please use ScriptCoreLib.Archive.ZIP assembly instead.")]
	public class ZIPFile
	{
		[Script]
		public class Entry
		{
			public string FileName
			{
				get
				{
					return Header.file_name;
				}
			}

			public MemoryStream Data
			{
				get
				{
					return Header.file_data;
				}
			}

			internal ZIPFileEntryHeader Header;
		}

		public readonly List<Entry> Items = new List<Entry>();

		public static implicit operator ZIPFile(BinaryReader s)
		{
			var n = new ZIPFile();

			#region read all files
			while (s.BaseStream.Position < s.BaseStream.Length)
			{
				ZIPFileEntryHeader h = s;

				if (h == null)
					break;

				n.Items.Add(new Entry { Header = h });
			}
			#endregion

			return n;
		}

	}
}
