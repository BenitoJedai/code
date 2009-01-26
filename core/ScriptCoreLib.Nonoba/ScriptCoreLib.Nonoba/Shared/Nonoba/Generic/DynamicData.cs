using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Nonoba.Generic
{
	[Script]
	public partial class DynamicData
	{
		public DynamicData Parent;

		public Action<string, string> SetData;
		public Func<string, string, string> GetData;

		public int Index = -1;
		public string Key = "";
		public string DefaultValue = "";

		public string GetCombinedKey()
		{
			if (Index >= 0)
			{
				if (Parent == null)
					return "_" + Index;


				return Parent.GetCombinedKey() + "_" + Index;
			}

			if (Parent == null)
				return Key;

			var p = Parent.GetCombinedKey();

			if (string.IsNullOrEmpty(p))
				return Key;

			return Parent.GetCombinedKey() + "_" + Key;
		}

		public string Value
		{
			get
			{
				if (this.GetData == null)
					return this.DefaultValue;

				return this.GetData(this.GetCombinedKey(), this.DefaultValue);
			}
			set
			{
				if (this.SetData == null)
					return;

				this.SetData(this.GetCombinedKey(), value);
			}
		}

		public int ValueInt32
		{
			get
			{
				var value = 0;

				try
				{
					value = int.Parse(this.Value);
				}
				catch
				{

				}

				return value;
			}
			set
			{
				this.Value = "" + value;
			}
		}

		public DynamicData this[int index]
		{
			get
			{
				return new DynamicData
				{
					DefaultValue = DefaultValue,
					GetData = GetData,
					SetData = SetData,
					Index = index,
					Parent = this
				};
			}
		}



		public DynamicData this[string index]
		{
			get
			{
				return new DynamicData
				{
					DefaultValue = DefaultValue,
					GetData = GetData,
					SetData = SetData,
					Key = index,
					Parent = this
				};
			}
			set
			{
				this[index].Value = value.Value;
			}
		}

		public static implicit operator DynamicData(string e)
		{
			return new DynamicData { DefaultValue = e };
		}
	}
}
