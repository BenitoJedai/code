using System;
using System.Xml.Linq;
namespace ScriptCoreLib.ActionScript.Components
{
	public interface ISaveAction
	{
		void Add(string name, string data);
		void Add(string name, XElement data);
		string FileName { get; set; }
	}
}
