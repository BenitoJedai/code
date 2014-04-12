using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Nonoba.Generic
{
	partial class DynamicData
	{
		[Script]
		public class Composed
		{
			public DynamicData Reference;

			public static implicit operator DynamicData.Composed(DynamicData Reference)
			{
				return new Composed { Reference = Reference };
			}

			/// <summary>
			/// The actual number of keys to store the data will be calculated based on the allowed chunk size
			/// </summary>
			public int ChunkSize;

			public Composed()
			{
				this.ChunkSize = 8 * 31;
			}

			public int Length
			{
				get
				{
					return Reference["Length"].ValueInt32;
				}
				set
				{
					Reference["Length"].ValueInt32 = value;
				}
			}

			public string Value
			{
				get
				{
					var Length = this.Length;

					var value = new StringBuilder();

					var c = 0;
					for (int i = 0; i < Length; i += ChunkSize)
					{
						value.Append(Reference[c].Value);
						c++;
					}

					return value.ToString();
				}
				set
				{
					this.Length = value.Length;

					var c = 0;
					for (int i = 0; i < Length; i += ChunkSize)
					{
						var x = ChunkSize;

						if (i + ChunkSize > value.Length)
							x = value.Length - i;

						Reference[c].Value = value.Substring(i, x);
						c++;
					}
				}
			}
		}
	}
}
