using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLibJava.BCLImplementation.System.ComponentModel;
using ScriptCoreLib;
using System.Windows.Forms;
using System.Collections;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	partial class __Control
	{

		[Script(Implements = typeof(global::System.Windows.Forms.Control.ControlCollection))]
		internal class __ControlCollection : Layout.__ArrangedElementCollection
		{
			readonly Control Owner;
			readonly ArrayList Items = new ArrayList();

			public __ControlCollection(Control owner)
			{
				this.Owner = owner;
			}

			public void Remove(Control e)
			{
				
			}

			public void Add(Control e)
			{
				__Control _Owner = Owner;
				__Control _e = e;

				var __Owner = _Owner.InternalGetElement();
				var __e = _e.InternalGetElement();

				this.Items.Add(e);

				var __Owner_JFrame = __Owner as javax.swing.JFrame;
				if (__Owner_JFrame != null)
				{
					__Owner_JFrame.getContentPane().add(__e);
					return;
				}

				var __Owner_JDialog = __Owner as javax.swing.JDialog;
				if (__Owner_JDialog != null)
				{
					__Owner_JDialog.getContentPane().add(__e);
					return;
				}


				throw new NotSupportedException();
			}

			public override IEnumerator GetEnumerator()
			{
				throw new NotImplementedException();
			}

			public override int Count
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			public virtual Control this[int index]
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			public virtual void SetChildIndex(Control child, int newIndex)
			{

			}
		}
	}
}
