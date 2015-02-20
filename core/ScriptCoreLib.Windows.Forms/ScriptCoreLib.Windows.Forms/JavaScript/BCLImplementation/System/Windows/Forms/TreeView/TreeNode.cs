using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{

    [Script(Implements = typeof(global::System.Windows.Forms.TreeNode))]
    internal unsafe class __TreeNode
    {
        // X:\jsc.svn\examples\javascript\canvas\FormsSVGTreeView\FormsSVGTreeView\Application.cs
        // X:\jsc.svn\examples\javascript\forms\Test\TestTreeView\TestTreeView\ApplicationControl.cs

        public IHTMLDiv InternalElement = typeof(__TreeNode);
        //public IHTMLDiv InternalElementContent = new IHTMLDiv().AttachTo(InternalElement);
        public IHTMLDiv InternalElementHeader = new IHTMLDiv { className = "Header" };
        public IHTMLSpan InternalElementHeaderText = new IHTMLSpan { };
        public IHTMLDiv InternalElementContent = new IHTMLDiv { className = "Content" };


        // wont work for shadow elements?
        //static IStyle hover = new IStyle(Native.css[typeof(__TreeNode)].hover)
        //{
        //    border = "1px solid rgba(0,0,255, 0.5)",
        //    backgroundColor = "rgba(0,0,255, 0.1)"
        //};

        public string Name { get; set; }
        public string Text { get { return this.InternalElementHeaderText.innerText; } set { this.InternalElementHeaderText.innerText = value; } }


        public int Level { get; set; }

        #region IsExpanded
        public bool IsExpanded { get; set; }

        public void Toggle()
        {
            if (IsExpanded)
                Collapse();
            else
                Expand();
        }

        public void Collapse()
        {
            IsExpanded = false;
            // will it render on svg?
            this.InternalElementContent.Hide();
        }

        public void Expand()
        {
            IsExpanded = true;

            this.InternalElementContent.Show();

            ((__TreeView)this.TreeView).RaiseAfterExpand(
                new TreeViewEventArgs(this)
            );
        }
        #endregion



        // what about drag n drop?

        public bool IsSelected { get; set; }

        public __TreeNode(string text) : this(text, null) { }
        public __TreeNode(string text, TreeNode[] children)
        {
            // svg renderer doesnt know div is a block?
            this.InternalElement.style.display = IStyle.DisplayEnum.block;
            this.InternalElementHeader.style.display = IStyle.DisplayEnum.block;


            this.Text = text;

            //new IHTMLSpan { ref this.Text }.AttachTo(this.IntenalElement);
            this.InternalElementHeader.AttachTo(this.InternalElement);


            // this wont render in svg!
            //new IHTMLInput { type = Shared.HTMLInputTypeEnum.checkbox }.AttachTo(this.InternalElementHeader);


            // explorer hides em without mouse hover
            //var ToggleButton = new IHTMLSpan { "[-]" }.AttachTo(this.InternalElementHeader);

            //ToggleButton.style.marginLeft = "-1em";

            // https://developer.mozilla.org/en-US/docs/Web/CSS/text-overflow
            this.InternalElementHeaderText.style.textOverflow = "ellipsis";
            this.InternalElementHeaderText.style.whiteSpace = IStyle.WhiteSpaceEnum.nowrap;
            this.InternalElementHeaderText.style.overflow = IStyle.OverflowEnum.hidden;


            this.InternalElementHeaderText.AttachTo(this.InternalElementHeader);

            // either add onclick to all nodes or listen on control level?
            this.InternalElementHeader.onclick +=
                e =>
                {
                    //if (e.MouseButton == IEvent.MouseButtonEnum.Middle)
                    //{
                    //    e.stopPropagation();
                    //    e.preventDefault();
                    //    this.Toggle();
                    //    return;
                    //}

                    this.TreeView.SelectedNode = this;

                    ((__TreeView)this.TreeView).RaiseNodeMouseClick(
                        new TreeNodeMouseClickEventArgs(
                            this, MouseButtons.Left, 0, 0, 0
                        )
                    );

                    e.stopPropagation();
                    e.preventDefault();
                    return;
                };

            this.InternalElementHeader.ondblclick +=
                e =>
                {
                    e.stopPropagation();
                    e.preventDefault();
                    this.Toggle();
                    return;
                };


            this.InternalElementContent.AttachTo(this.InternalElement);

            //InternalLevelChanged();


            //Error CS0206  A property or indexer may not be passed as an out or ref parameter ScriptCoreLib.Windows.Forms TreeNode.cs 31
            //link(ref this.Text);
            //link(&this.Text);

            // how is this compiling?
            this.Nodes = new __TreeNodeCollection { that__TreeNode = this };


            if (children != null)
                this.Nodes.AddRange(children);

            IsExpanded = true;
        }

        public void InternalLevelChanged()
        {
            this.Level = 0;
            var p = this.Parent;
            while (p != null)
            {
                this.Level++;
                p = p.Parent;
            }

            this.InternalElementHeader.title = new { Level }.ToString();


            // depends how deep we are?
            // wont render?
            //this.InternalElementHeaderText.style.textIndent = (this.Level * 2) + "em";
            //this.InternalElementHeaderText.style.paddingLeft = (this.Level * 2) + "em";
            this.InternalElementHeaderText.style.marginLeft = ((this.Level + 1) * 2) + "em";

            foreach (var item in ((__TreeNodeCollection)this.Nodes).InternalList)
            {
                item.InternalLevelChanged();
            }
        }

        public TreeNodeCollection Nodes { get; }



        public static implicit operator TreeNode(__TreeNode e)
        {
            return (TreeNode)(object)e;
        }


        public static implicit operator __TreeNode(TreeNode e)
        {
            return (__TreeNode)(object)e;
        }

        //static void link(ref string e)
        //{
        //}

        //static void link(string* e)
        //{
        //    //Error CS0208  Cannot take the address of, get the size of, or declare a pointer to a managed type('string')  ScriptCoreLib.Windows.Forms TreeNode.cs 64

        //}

        public TreeNode Parent
        {
            get; set;
        }

        public __TreeView that__TreeView;

        public TreeView TreeView
        {
            get
            {
                if (Parent != null)
                    return Parent.TreeView;

                return that__TreeView;
            }
        }



        public object Tag { get; set; }
    }
}
