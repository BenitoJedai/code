using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{

    [Script(Implements = typeof(global::System.Windows.Forms.TreeNodeCollection))]
    internal class __TreeNodeCollection
    {
        // X:\jsc.svn\examples\javascript\forms\Test\TestTreeView\TestTreeView\ApplicationControl.cs

        public __TreeView that__TreeView;
        public __TreeNode that__TreeNode;

        public List<__TreeNode> InternalList = new List<__TreeNode>();


        public int Count { get { return this.InternalList.Count; } }

        public virtual TreeNode Add(string text)
        {
            var n = new TreeNode(text);

            AddRange(new[] { n });


            return n;
        }

        public virtual void AddRange(TreeNode[] nodes)
        {
            // are we on the root?
            // we are adding the roots!

            foreach (__TreeNode item in nodes)
            {
                InternalList.Add(item);

                //new IHTMLDiv { item.Text }.AttachTo(that);

                if (that__TreeView != null)
                {
                    item.that__TreeView = that__TreeView;
                    item.InternalElement.AttachTo(that__TreeView.AsNode());
                }

                if (that__TreeNode != null)
                {
                    item.Parent = that__TreeNode;
                    item.InternalElement.AttachTo(that__TreeNode.InternalElementContent);
                }

                item.InternalLevelChanged();
            }
        }

        public virtual void Clear()
        {
            // X:\jsc.svn\examples\javascript\WebGL\WebGLDashedLines\WebGLDashedLines\Application.cs

            if (that__TreeView != null)
            {
                that__TreeView.AsNode().Clear();
            }

            if (that__TreeNode != null)
            {
                that__TreeNode.InternalElementContent.Clear();
            }

            this.InternalList.Clear();

        }

        public static implicit operator TreeNodeCollection(__TreeNodeCollection e)
        {
            return (TreeNodeCollection)(object)e;
        }
        public static implicit operator __TreeNodeCollection(TreeNodeCollection e)
        {
            return (__TreeNodeCollection)(object)e;
        }
    }
}
