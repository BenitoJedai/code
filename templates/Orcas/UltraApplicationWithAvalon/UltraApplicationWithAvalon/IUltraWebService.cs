using System;
namespace UltraApplicationWithAvalon
{
	public interface IUltraWebService
	{
		void GetTime(string x, ScriptCoreLib.Ultra.Library.Delegates.StringAction result);
	}
}
