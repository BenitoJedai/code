using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Text
{
	[Script(Implements = typeof(global::System.Text.StringBuilder))]
	internal class __StringBuilder
	{
		public __StringBuilder()
		{

		}

		string _Value = "";

		public __StringBuilder Append(bool e)
		{
			_Value += e;

			return this;
		}

		public __StringBuilder Append(double e)
		{
			_Value += e;

			return this;
		}

		public __StringBuilder Append(uint e)
		{
			_Value += e;

			return this;
		}

		public __StringBuilder Append(int e)
		{
			_Value += e;

			return this;
		}

		public __StringBuilder Append(char e)
		{
			_Value += Native.API.chr(e);

			return this;
		}

		public __StringBuilder Append(string e)
		{
			_Value += e;

			return this;
		}

		public __StringBuilder Append(object value)
		{
			if (ScriptCoreLib.PHP.Runtime.Expando.Of(value).IsNumber)
			{
				_Value += (int)value;

				return this;
			}

			if (value != null)
			{
				// php strings do not have the __ToString member
				// fixme: should use the is string operator instead


				if (ScriptCoreLib.PHP.Runtime.Expando.Of(value).IsString)
				{
					_Value += (string)value;
				}
				else
				{
					_Value += value.ToString();
				}
			}

			return this;
		}

		public __StringBuilder AppendLine()
		{
			return Append(Environment.NewLine);
		}

		public __StringBuilder AppendLine(string value)
		{
			return Append(value).AppendLine();
		}


		public override string ToString()
		{
			return _Value;
		}
	}
}
