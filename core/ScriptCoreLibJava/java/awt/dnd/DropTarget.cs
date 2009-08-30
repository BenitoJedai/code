// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.datatransfer;
using java.awt.dnd;

namespace java.awt.dnd
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/dnd/DropTarget.html
	[Script(IsNative = true)]
	public class DropTarget
	{
		[Script(IsNative = true)]
		public class DropTargetAutoScroller
		{
		}

		/// <summary>
		/// Creates a <code>DropTarget</code>.
		/// </summary>
		public DropTarget()
		{
		}

		/// <summary>
		/// Creates a <code>DropTarget</code> given the <code>Component</code>
		/// to associate itself with, and the <code>DropTargetListener</code>
		/// to handle event processing.
		/// </summary>
		public DropTarget(Component @c, DropTargetListener @dtl)
		{
		}

		/// <summary>
		/// Creates a <code>DropTarget</code> given the <code>Component</code>
		/// to associate itself with, an <code>int</code> representing
		/// the default acceptable action(s) to support, and a
		/// <code>DropTargetListener</code> to handle event processing.
		/// </summary>
		public DropTarget(Component @c, int @ops, DropTargetListener @dtl)
		{
		}

		/// <summary>
		/// Creates a <code>DropTarget</code> given the <code>Component</code>
		/// to associate itself with, an <code>int</code> representing
		/// the default acceptable action(s)
		/// to support, a <code>DropTargetListener</code>
		/// to handle event processing, and a <code>boolean</code> indicating
		/// if the <code>DropTarget</code> is currently accepting drops.
		/// </summary>
		public DropTarget(Component @c, int @ops, DropTargetListener @dtl, bool @act)
		{
		}

		/// <summary>
		/// Creates a new DropTarget given the <code>Component</code>
		/// to associate itself with, an <code>int</code> representing
		/// the default acceptable action(s) to
		/// support, a <code>DropTargetListener</code>
		/// to handle event processing, a <code>boolean</code> indicating
		/// if the <code>DropTarget</code> is currently accepting drops, and
		/// a <code>FlavorMap</code> to use (or null for the default <CODE>FlavorMap</CODE>).
		/// </summary>
		public DropTarget(Component @c, int @ops, DropTargetListener @dtl, bool @act, FlavorMap @fm)
		{
		}

		/// <summary>
		/// Adds a new <code>DropTargetListener</code> (UNICAST SOURCE).
		/// </summary>
		public void addDropTargetListener(DropTargetListener @dtl)
		{
		}

		/// <summary>
		/// Notify the DropTarget that it has been associated with a Component
		/// 
		/// 
		/// This method is usually called from java.awt.Component.addNotify() of
		/// the Component associated with this DropTarget to notify the DropTarget
		/// that a ComponentPeer has been associated with that Component.
		/// </summary>
		//public void addNotify(java.awt.peer.ComponentPeer @peer)
		//{
		//}

		/// <summary>
		/// clear autoscrolling
		/// </summary>
		protected void clearAutoscroll()
		{
		}

		/// <summary>
		/// create an embedded autoscroller
		/// </summary>
		public DropTarget.DropTargetAutoScroller createDropTargetAutoScroller(Component @c, Point @p)
		{
			return default(DropTarget.DropTargetAutoScroller);
		}

		/// <summary>
		/// Creates the DropTargetContext associated with this DropTarget.
		/// </summary>
		public DropTargetContext createDropTargetContext()
		{
			return default(DropTargetContext);
		}

		/// <summary>
		/// The <code>DropTarget</code> intercepts
		/// dragEnter() notifications before the
		/// registered <code>DropTargetListener</code> gets them.
		/// </summary>
		public void dragEnter(DropTargetDragEvent @dtde)
		{
		}

		/// <summary>
		/// The <code>DropTarget</code> intercepts
		/// dragExit() notifications before the
		/// registered <code>DropTargetListener</code> gets them.
		/// </summary>
		public void dragExit(DropTargetEvent @dte)
		{
		}

		/// <summary>
		/// The <code>DropTarget</code>
		/// intercepts dragOver() notifications before the
		/// registered <code>DropTargetListener</code> gets them.
		/// </summary>
		public void dragOver(DropTargetDragEvent @dtde)
		{
		}

		/// <summary>
		/// The <code>DropTarget</code> intercepts drop() notifications before the
		/// registered <code>DropTargetListener</code> gets them.
		/// </summary>
		public void drop(DropTargetDropEvent @dtde)
		{
		}

		/// <summary>
		/// The <code>DropTarget</code> intercepts
		/// dropActionChanged() notifications before the
		/// registered <code>DropTargetListener</code> gets them.
		/// </summary>
		public void dropActionChanged(DropTargetDragEvent @dtde)
		{
		}

		/// <summary>
		/// Gets the <code>Component</code> associated
		/// with this <code>DropTarget</code>.
		/// </summary>
		public Component getComponent()
		{
			return default(Component);
		}

		/// <summary>
		/// Gets an <code>int</code> representing the
		/// current action(s) supported by this <code>DropTarget</code>.
		/// </summary>
		public int getDefaultActions()
		{
			return default(int);
		}

		/// <summary>
		/// Gets the <code>DropTargetContext</code> associated
		/// with this <code>DropTarget</code>.
		/// </summary>
		public DropTargetContext getDropTargetContext()
		{
			return default(DropTargetContext);
		}

		/// <summary>
		/// Gets the <code>FlavorMap</code>
		/// associated with this <code>DropTarget</code>.
		/// </summary>
		public FlavorMap getFlavorMap()
		{
			return default(FlavorMap);
		}

		/// <summary>
		/// initialize autoscrolling
		/// </summary>
		protected void initializeAutoscrolling(Point @p)
		{
		}

		/// <summary>
		/// Reports whether or not
		/// this <code>DropTarget</code>
		/// is currently active (ready to accept drops).
		/// </summary>
		public bool isActive()
		{
			return default(bool);
		}

		/// <summary>
		/// Removes the current <code>DropTargetListener</code> (UNICAST SOURCE).
		/// </summary>
		public void removeDropTargetListener(DropTargetListener @dtl)
		{
		}

		/// <summary>
		/// Notify the DropTarget that it has been disassociated from a Component
		/// 
		/// 
		/// This method is usually called from java.awt.Component.removeNotify() of
		/// the Component associated with this DropTarget to notify the DropTarget
		/// that a ComponentPeer has been disassociated with that Component.
		/// </summary>
		//public void removeNotify(java.awt.peer.ComponentPeer @peer)
		//{
		//}

		/// <summary>
		/// Sets the DropTarget active if <code>true</code>,
		/// inactive if <code>false</code>.
		/// </summary>
		public void setActive(bool @isActive)
		{
		}

		/// <summary>
		/// Note: this interface is required to permit the safe association
		/// of a DropTarget with a Component in one of two ways, either:
		/// <code> component.setDropTarget(droptarget); </code>
		/// or <code> droptarget.setComponent(component); </code>
		/// </summary>
		public void setComponent(Component @c)
		{
		}

		/// <summary>
		/// Sets the default acceptable actions for this <code>DropTarget</code>
		/// </summary>
		public void setDefaultActions(int @ops)
		{
		}

		/// <summary>
		/// Sets the <code>FlavorMap</code> associated
		/// with this <code>DropTarget</code>.
		/// </summary>
		public void setFlavorMap(FlavorMap @fm)
		{
		}

		/// <summary>
		/// update autoscrolling with current cursor locn
		/// </summary>
		protected void updateAutoscroll(Point @dragCursorLocn)
		{
		}

	}
}

