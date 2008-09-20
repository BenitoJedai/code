using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Nonoba.Generic
{
	[Script]
	public class ServerGameSettings
	{
		public Func<string, bool> GetBoolean;
		public Func<string, int> GetInteger;
		public Func<string, string> GetOption;
		public Func<string, string> GetString;
	}
}
