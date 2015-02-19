using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{

    [Script(Implements = typeof(global::System.Windows.Forms.TreeNode))]
    internal class __TreeNode
    {
        // X:\jsc.svn\examples\javascript\forms\Test\TestTreeView\TestTreeView\ApplicationControl.cs


        public string Name { get; set; }
        public string Text { get; set; }

        public __TreeNode(string text)
        {

        }

        public __TreeNode(string text, TreeNode[] children)
        { }
    }
}
