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




		public delegate Property SubPropertyConstructor(string name);
		public delegate void PropertyAction(Property p);
		public delegate bool PropertyFilterFunc(Property p);

		public class Property
		{

			public Property this[string name]
			{
				get
				{
					return this.InternalHandlers.GetSubProperty(name);
				}
			}


			public class Handlers
			{
				public SubPropertyConstructor GetSubProperty;

				public StringAction set_TextHandler;
				public StringFunc get_TextHandler;

				public ByteArrayAction set_BytesHandler;
				public ByteArrayFunc get_BytesHandler;
			}

			readonly Handlers InternalHandlers;

			public readonly string Name;
			public readonly string Category;

			public Property(Handlers h, string Name, string Category)
			{
				this.Name = Name;
				this.Category = Category;
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

		}

		public Property GetProperty(string name)
		{
			return GetProperty(name, "Properties");
		}

		public Property GetProperty(string name, string category)
		{

			string TextExtension = ".txt";
			string BinaryExtensions = ".bin";

			if (Path.HasExtension(name))
			{
				TextExtension = "";
				BinaryExtensions = "";
			}

			var TextHandler = category.CombinePath(name + TextExtension);
			var BytesHandler = category.CombinePath(name + BinaryExtensions);

			var h = new Property.Handlers
			{
				get_TextHandler = () => this.Content[TextHandler].Text,
				set_TextHandler = value => this.Content[TextHandler].Text = value,

				get_BytesHandler = () => this.Content[BytesHandler].Bytes,
				set_BytesHandler = value => this.Content[BytesHandler].Bytes = value,

				GetSubProperty =
					n => GetProperty(n, Path.Combine(category, name))

			};

			return new Property(h, name, category);
		}
	}

}
