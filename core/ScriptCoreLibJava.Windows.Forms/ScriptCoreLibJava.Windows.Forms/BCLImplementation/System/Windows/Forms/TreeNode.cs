using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.Runtime.Serialization;
using System.Windows.Forms;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.TreeNode))]
	internal class __TreeNode : __MarshalByRefObject, __ICloneable, __ISerializable
	{
		public __TreeNode(string text)
		{
		}

		public __TreeNode(string text, TreeNode[] children)
		{
		}

		public string Name { get; set; }
		public string Text { get; set; }
	}
}
