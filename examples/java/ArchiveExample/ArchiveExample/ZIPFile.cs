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
	public partial class ZIPFile : IEnumerable
	{
		[Script]
		public class Entry
		{
			string InternalFileName;
			public string FileName
			{
				set
				{
					InternalFileName = value;
					Header = null;
				}
				get
				{
					if (Header == null)
						return InternalFileName;

					return Header.file_name;
				}
			}

			MemoryStream InternalData;
			public MemoryStream Data
			{
				set
				{
					InternalData = value;
					Header = null;
				}

				get
				{
					if (Header == null)
						return InternalData;
					
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


		public void Add(string FileName, string text)
		{
			this.Add(FileName, Encoding.ASCII.GetBytes(text));
		}

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return this.Entries.GetEnumerator();
		}

		#endregion
	}

}
