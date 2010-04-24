using System;
namespace ScriptCoreLib.ActionScript.Components
{
	public interface ISaveAction
	{
		void Add(string name, string data);
		string FileName { get; set; }
	}
}
