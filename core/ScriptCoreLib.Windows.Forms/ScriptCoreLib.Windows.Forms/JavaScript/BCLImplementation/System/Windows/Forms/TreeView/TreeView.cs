using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Extensions;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    // http://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/TreeView.cs,babfd7e2e150fae9,references
    // X:\jsc.svn\core\ScriptCoreLibJava.Windows.Forms\ScriptCoreLibJava.Windows.Forms\BCLImplementation\System\Windows\Forms\TreeView.cs

    [Script(Implements = typeof(global::System.Windows.Forms.TreeView))]
    internal class __TreeView : __Control
    {
        // would we be able to render it into svg for vr?
        // could a NDK do a window and host this?


        // can we databind to the three.js visual tree for diagnostics?
        // https://zproxy.wordpress.com/2009/10/21/extending-scriptcorelib/#more-1445
        // 5 years later:D!
        // drag n drop may be important.
        // where is gx2?


        // X:\jsc.svn\examples\javascript\forms\Test\TestTreeView\TestTreeView\ApplicationControl.cs


        //public IHTMLDiv InternalElement = new IHTMLDiv();
        public IHTMLDiv InternalElement = typeof(__TreeView);

        #region SelectedNode
        public __TreeNode InternalSelectedNode;
        public TreeNode SelectedNode
        {
            get { return InternalSelectedNode; }
            set
            {
                // databinding selection?
                // would the udp sync show selection in vr?

                if (InternalSelectedNode != null)
                {
                    InternalSelectedNode.IsSelected = false;

                    new IStyle(InternalSelectedNode.InternalElementHeader)
                    {
                        backgroundColor = "",
                        color = ""
                    };
                }

                InternalSelectedNode = value;

                if (InternalSelectedNode != null)
                {
                    InternalSelectedNode.IsSelected = true;

                    new IStyle(InternalSelectedNode.InternalElementHeader)
                    {
                        backgroundColor = "blue",
                        color = "white"
                    };
                }

                if (AfterSelect != null)
                {
                    AfterSelect(this, new TreeViewEventArgs(this.InternalSelectedNode));


                }
            }
        }
        #endregion


        public __TreeView()
        {
            this.InternalElement.style.backgroundColor = "yellow";

            // for svg renderer we would want full view?
            this.InternalElement.style.overflow = IStyle.OverflowEnum.auto;


            // can it work for shadow elements too?



            // need it only once?
            // what triggers it?
            this.InternalAtAfterVisibleChanged +=
                delegate
                {
                    // Uncaught Error: fault at IHTMLStyle.StyleSheet
                    var style = new IHTMLStyle { }.AttachTo(InternalElement);

                    // dont want to see it in svg render
                    // yes this seems to do the trick.
                    style.style.display = IStyle.DisplayEnum.none;

                    var selector = "." + typeof(__TreeNode).Name + ">.Header:hover";

                    Console.WriteLine(new { selector });

                    // we need to wait until attached to document?
                    //var rule = style.StyleSheet.AddRule("." + typeof(__TreeNode).Name + ":hover");
                    new IStyle(style[selector])
                    {
                        textDecoration = "underline",
                        cursor = IStyle.CursorEnum.pointer,

                        backgroundColor = "blue",
                        color = "white"
                    };

                    // Content

                    // level1
                    //new IStyle(style["." + typeof(__TreeNode).Name + ">.Content"])
                    //{
                    //    textIndent = "2em"
                    //};

                    // level2
                    //new IStyle(style["." + typeof(__TreeNode).Name + " ." + typeof(__TreeNode).Name + ">.Content"])
                    //{
                    //    textIndent = "4em"
                    //};
                };

            this.Nodes = new __TreeNodeCollection { that__TreeView = this };


            //this.InternalElement[].click


            // will we get the same font in svg render?
            this.InternalSetDefaultFont();
        }


        public TreeNodeCollection Nodes { get; }

        // what do we have to do to implement it?
        // 01. clear media/capture for new screenshots
        // 02. lets make a yellow box. lets not use typeof(x) thistime as we expect to render it in svg via remoting?
        // 03. run test. is it yellow?
        // 04. would be nice if we had our background compler, out of band code patching ready huh:D
        // 05. yes the yellow box shows.
        // 06. lets add content in it?
        // 07. for hover effects we should use typeof(x):? should we convert win forms to 4.5.3 yet?
        // 08. add node click



        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return this.InternalElement;
            }
        }





        public void RaiseAfterExpand(TreeViewEventArgs args)
        {
            if (AfterExpand != null)
                AfterExpand(this, args);
        }

        public event TreeViewEventHandler AfterExpand;
        public event TreeViewEventHandler AfterSelect;

        public event TreeNodeMouseClickEventHandler NodeMouseClick;

        public void RaiseNodeMouseClick(TreeNodeMouseClickEventArgs args)
        {
            if (NodeMouseClick != null)
                NodeMouseClick(this, args);

        }

        public static implicit operator TreeView(__TreeView e)
        {
            return (TreeView)(object)e;
        }


        public ItemDragEventHandler InternalItemDrag;

        public event ItemDragEventHandler ItemDrag
        {
            // for dynamic code patching of a lve app
            // jsc should also be able to deduce how to undo an add?
            remove { }

            add
            {
                //Console.WriteLine("add ItemDrag");


                InternalItemDrag += value;

                // this would select a wrong element as image?
                // need to reactiveate ondrag for each row separatly instead?

                // for current and future elements +=

                //this.InternalElement[".x"].ondragstart +=

                this.InternalElement.ondragstart +=
                    e =>
                    {
                        //Console.WriteLine("ondragstart");

                        //var Buttons = e.GetMouseButton();

                        //Console.WriteLine("ondragstart " + new { Buttons });

                        //var a = e.GetMouseEventHandler(Buttons);
                        var a = new __ItemDragEventArgs(MouseButtons.Left, this.SelectedNode);

                        var MissingDoDragDrop = true;

                        InternalUIDoDragDrop =
                            (that, data, effects) =>
                            {
                                //Console.WriteLine("enter InternalDoDragDrop");


                                MissingDoDragDrop = false;

                                // X:\jsc.internal.svn\compiler\jsx.reflector\ReflectorWindow.AddNode.cs
                                // X:\jsc.svn\examples\javascript\DragIntoCRX\DragIntoCRX\Application.cs

                                // hope its a string?

                                // http://msdn.microsoft.com/en-us/library/ie/ms536744(v=vs.85).aspx
                                // https://msdn.microsoft.com/en-us/library/637ys738(v=vs.110).aspx
                                var dataTransfer = e.dataTransfer;

                                // InternalUIDoDragDrop {{ xDataObject = null }}
                                var xDataObject = data as __DataObject;

                                Console.WriteLine("InternalUIDoDragDrop " + new { xDataObject });

                                if (xDataObject != null)
                                {
                                    xDataObject.AtDataTransfer(dataTransfer);
                                }
                                else
                                {

                                    var text = "" + data;

                                    // SCRIPT65535: Unexpected call to method or property access. 
                                    //dataTransfer.setData("text/plain", text);
                                    dataTransfer.setData("Text", text);
                                }
                                // http://blogs.msdn.com/b/adamroot/archive/2008/02/19/shell-style-drag-and-drop-in-net-part-3.aspx
                                // http://help.dottoro.com/ljxfefku.php
                                // https://code.google.com/p/chromium/issues/detail?id=95631
                                //dataTransfer.addElement(
                                //    ((__TreeNode)this.SelectedNode).InternalElementHeader
                                //);


                                // Uncaught TypeError: Failed to execute 'setDragImage' on 'DataTransfer': setDragImage: Invalid first argument
                                //dataTransfer.setDragImage(null, 0, 0);
                                // c# where is the non null op?


                                //Uncaught TypeError: setDragImageFromElement: Invalid first argument 
                                //e.dataTransfer.setDragImage(null, 0, 0);

                            };

                        //Console.WriteLine("before raise ItemDrag " + new { InternalUIDoDragDrop });
                        value(this, a);
                        //Console.WriteLine("after raise ItemDrag " + new { MissingDoDragDrop });

                        InternalUIDoDragDrop = null;

                        // can we abort?
                        if (MissingDoDragDrop)
                        {
                            e.preventDefault();
                            e.stopPropagation();
                        }
                    };
            }

        }
    }
}
