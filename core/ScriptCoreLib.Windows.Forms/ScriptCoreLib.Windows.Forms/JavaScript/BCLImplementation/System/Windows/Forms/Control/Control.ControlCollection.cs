using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Drawing;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using ScriptCoreLib.JavaScript.Drawing;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{

    using DOMHandler = global::System.Action<DOM.IEvent>;
    using ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms;
    using ScriptCoreLib.JavaScript.DOM;

    partial class __Control
    {
        [Script(Implements = typeof(global::System.Windows.Forms.Control.ControlCollection))]
        internal class __ControlCollection : Layout.__ArrangedElementCollection
        {
            readonly Control Owner;

            public __ControlCollection(Control owner)
            {
                this.Owner = owner;
            }

            readonly List<Control> Items = new List<Control>();

            public virtual void Remove(Control e)
            {
                throw new global::System.Exception("Not implemented");
            }

            public bool InternalAddToTop = true;

            public virtual void Add(Control e)
            {
                var ee = (__Control)e;
                //var ChildElement = e.GetHTMLTarget();
                var ChildElement = ee.HTMLTargetRef;
                if (ChildElement == null)
                {
                    Console.WriteLine("missing HTMLTargetRef for " + new { e });
                    return;
                }



                Items.Add(e);

                var bg = this.Owner.GetHTMLTargetContainer();

                // why would we want to do the reverse here?
                //if (bg.firstChild == null)



                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140124
                // let the DOM know about our pseudo shadow DOM
                // we do expect the reflection to give us the .net name
                // of the control here.
                //var ContextTypeName = this.Owner.GetType().Name;
                var ChildElementTypeName = e.GetType().Name;

                // do we want to help css selectors to know
                // who is the parent? 
                //bg.className += " ContextOf" + ContextTypeName;

                // what about being added multiple times?


                // does it work for other browsers?
                // see also: 
                // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\DataGridView\DataGridView..ctor.cs
                if (!ChildElement.classList.Contains(ChildElementTypeName))
                    ChildElement.className += " " + ChildElementTypeName;

                // X:\jsc.svn\examples\javascript\forms\FormsNIC\FormsNIC\ApplicationControl.cs
                // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\FlowLayoutPanel.cs

                if (InternalAddToTop)
                {
                    // play well with forms designer.

                    if (bg.childNodes.Length == 0)
                        bg.appendChild(ChildElement);
                    else
                        bg.insertBefore(ChildElement, bg.childNodes[0]);
                }
                else
                {
                    bg.appendChild(ChildElement);
                }

                //else
                //bg.insertBefore(e.GetHTMLTarget(), bg.firstChild);

                var c = (__Control)e;

                c.InternalAssignParent(this.Owner);

                ((__Control)this.Owner).OnControlAdded(new ControlEventArgs(e));

            }

            public override IEnumerator GetEnumerator()
            {
                return Items.GetEnumerator();
            }

            public override int Count
            {
                get
                {
                    return Items.Count;
                }
            }

            public virtual Control this[int index]
            {
                get
                {
                    if (index < 0)
                        throw new Exception("IndexOutOfRange");

                    if (index >= this.Count)
                        throw new Exception("IndexOutOfRange");

                    return (Control)Items[index];
                }
            }

            public virtual void SetChildIndex(Control child, int newIndex)
            {
                // we should apply the index
            }


            public static implicit operator __ControlCollection(Control.ControlCollection c)
            {
                return (__ControlCollection)(object)c;
            }
        }


    }
}
