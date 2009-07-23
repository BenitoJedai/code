using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;
using System.Collections;

namespace ScriptCoreLib.Archive.ZIP
{
	/// <summary>
	/// ZIP file provieds the most basic functionality to compose a non compressed zip file.
	/// </summary>
	public partial class ZIPFile : IEnumerable
	{
		// http://stackoverflow.com/questions/856013/mime-type-for-zip-file-in-google-chrome

		public const string ContentType = "application/zip";

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

			public string Text
			{
				set
				{
					this.Data = new MemoryStream(Encoding.ASCII.GetBytes(value));
				}
				get
				{
					return Encoding.ASCII.GetString(Data.ToArray());
				}
			}

			public byte[] Bytes
			{
				get
				{
					return this.Data.ToArray();
				}
				set
				{
					this.Data = new MemoryStream(value);
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

		public static implicit operator ZIPFile(Stream s)
		{
			return new BinaryReader(s);
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

		public bool Contains(string FileName)
		{
			var r = false;

			foreach (var k in this.Entries)
			{
				if (k.FileName == FileName)
				{
					r = true;
					break;
				}
			}

			return r;
		}

		/// <summary>
		/// Returns current or creates new file entry
		/// </summary>
		/// <param name="FileName"></param>
		/// <returns></returns>
		public Entry this[string FileName]
		{
			get
			{
				var r = default(Entry);

				foreach (var k in this.Entries)
				{
					if (k.FileName == FileName)
					{
						r = k;
						break;
					}
				}

				if (r == null)
				{
					r = new Entry { FileName = FileName };
					this.Add(r);
				}

				return r;
			}
		}

	}

}
