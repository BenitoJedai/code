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

		public global::System.Windows.Forms.TreeNodeCollection Nodes { get; set; }

		public __TreeNodeCollection InternalNodes;

		public __TreeNode(string text)
		{
			// http://forums.sun.com/thread.jspa?threadID=153780

			this.Text = text;

			this.InternalNodes = new __TreeNodeCollection
			{
				//InternalTreeView = this,
				InternalRoot = this.InternalElement
			};

			this.Nodes = (global::System.Windows.Forms.TreeNodeCollection)(object)this.InternalNodes;
		}


		public __TreeNode(string text, global::System.Windows.Forms.TreeNode[] children)
			: this(text)
		{
			this.Nodes.AddRange(children);
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
				this.InternalElement.setUserObject(new TreeNodeUserObject { Context = this });
			}
		}

		public object Tag { get; set; }

		[Script]
		public sealed class TreeNodeUserObject
		{
			public __TreeNode Context;

			public override string ToString()
			{
				return this.Context.Text;
			}
		}

		public void Remove()
		{
			var p = this.InternalElement.getParent();

			this.InternalElement.removeFromParent();

			if (p != null)
				if (this.InternalNodes.InternalTreeView != null)
					this.InternalNodes.InternalTreeView.InternalModel.reload(p);
		}
	}
}
