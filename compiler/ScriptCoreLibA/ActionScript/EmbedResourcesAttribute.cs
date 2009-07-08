using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
	/// <summary>
	/// This attribute shall be attached to the resource collector type, which
	/// must have a static Default member which must have a Class this[string e] setter
	/// The resources will be registered with the collector within
	/// a ScriptEntryPoint static constructor.
	/// 
	/// By supporting this attribute you can embed resources like web/assets/AssemblyName/asset.png
	/// </summary>
	[global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public class EmbedResourcesAttribute : Attribute
	{

	}
}
