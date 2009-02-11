using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Lambda
{
	[Script]
	public class PackedInt32
	{
		public readonly int BitsPerElement;

		public readonly uint[] Elements;

		public PackedInt32(int BitsPerElement)
		{
			this.BitsPerElement = BitsPerElement;
			this.Elements = new uint[Convert.ToInt32(Math.Floor(32.0 / BitsPerElement))];
			
			// actionscript does not init the array to zero?
			this.Value = 0;
		}

		public void Pop()
		{
			this.Value = this.Value >> this.BitsPerElement;
		}
		
		public uint Value
		{
			set
			{
				var mask = (1 << this.BitsPerElement) - 1;

				for (int i = 0; i < Elements.Length; i++)
				{
					this.Elements[i] = (uint)(value & mask);

					value = value >> this.BitsPerElement;
				}
			}
			get
			{
				var mask = (1 << this.BitsPerElement) - 1;
				uint value = 0;

				for (int i = Elements.Length - 1; i >= 0; i--)
				{
					value = value << this.BitsPerElement;

					value += (uint)(this.Elements[i] & mask);
				}

				return value;
			}
		}
	}
}
