using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebGLRah66Comanche.Library
{
    static class TreeViewExtensions
    {
        public static Task<TreeNode> AsyncAfterExpand(this TreeNode n)
        {
            var t = new TaskCompletionSource<TreeNode>();

            n.Collapse();

            n.TreeView.AfterExpand +=
                (sender, args) =>
                {
                    if (args.Node == n)
                    {
                        t.SetResult(n);
                        n = null;
                    }

                };

            return t.Task;
        }

        public static Task<TreeNode> AsyncAfterCollapse(this TreeNode n)
        {
            var t = new TaskCompletionSource<TreeNode>();

            n.Collapse();

            n.TreeView.AfterCollapse+=
                (sender, args) =>
                {
                    if (args.Node == n)
                    {
                        t.SetResult(n);
                        n = null;
                    }

                };

            return t.Task;
        }
    }
}
