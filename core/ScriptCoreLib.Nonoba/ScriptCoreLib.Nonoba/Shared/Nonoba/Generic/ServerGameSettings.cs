using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Nonoba.Generic
{
	[Script]
	public class ServerGameSettings
	{
		public Func<string, bool, bool> GetBoolean;
		public Func<string, int, int> GetInteger;
		public Func<string, string, string> GetOption;
		public Func<string, string, string> GetString;

		[Script]
		public class Defaults
		{
			public bool GetBoolean(string e, bool v)
			{
				return v;
			}

			public int GetInteger(string e, int v)
			{
				return v;
			}

			public string GetOption(string e, string v)
			{
				return v;
			}


			public string GetString(string e, string v)
			{
				return v;
			}

		}

		public ServerGameSettings()
		{
			var v = new Defaults();

			this.GetBoolean = v.GetBoolean;
			this.GetInteger = v.GetInteger;
			this.GetOption = v.GetOption;
			this.GetString = v.GetString;
		}
	}
}
