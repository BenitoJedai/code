using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;
using System.Collections;

namespace ArchiveExample
{
	[Script]
	public partial class ZIPFile
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

			public ZIPFileEntryHeader Header;
		}

		readonly ArrayList Items = new ArrayList();

		public Entry[] Entries
		{
			get
			{
				return (Entry[])Items.ToArray(typeof(Entry));
			}
		}

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
