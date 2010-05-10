using System;
using System.Xml.Linq;
namespace ScriptCoreLib.ActionScript.Components
{
	public interface ISaveAction
	{
		void Clear();
		void Add(string name, string data);
		void Add(string name, XElement data);
		string FileName { get; set; }
	}

	public interface ISaveActionWhenReady
	{
		void WhenReady(Action<ISaveAction> y);
	}
}
