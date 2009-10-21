using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.Runtime.Serialization;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.TreeNode))]
	internal class __TreeNode : __MarshalByRefObject, __ICloneable, __ISerializable
	{
		public javax.swing.tree.DefaultMutableTreeNode InternalElement = new javax.swing.tree.DefaultMutableTreeNode();

		public __TreeNode(string text)
		{
			// http://forums.sun.com/thread.jspa?threadID=153780

			this.Text = text;
		}


		public __TreeNode(string text, global::System.Windows.Forms.TreeNode[] children)
		{
			this.Text = text;

			foreach (var c in children)
			{
				var cc = (__TreeNode)(object)c;

				this.InternalElement.add(cc.InternalElement);
			}
		}

		public string Name { get; set; }

		string InternalText;
		public string Text
		{
			get
			{
				return InternalText;
			}
			set
			{
				InternalText = value;
				this.InternalElement.setUserObject(value);
			}
		}
	}
}
