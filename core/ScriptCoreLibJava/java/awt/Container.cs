using ScriptCoreLib;

namespace java.awt
{
    // http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Container.html#setLayout(java.awt.LayoutManager)

    [Script(IsNative = true)]
    public class Container : Component
    {
        /// <summary>
        /// Paints the container.
        /// </summary>
        public virtual void paint(Graphics g)
        {
        }

        public void setLayout(LayoutManager mgr)
        {

        }

        public void add(Component comp)
        {
        }

        public void remove(Component comp)
        {
        }

        /// <summary>
        /// Invalidates the container.
        /// </summary>
        public void invalidate()
        {
        }


        /// <summary>
        /// Validates this container and all of its subcomponents.
        /// </summary>
        public void validate()
        {
        }

    }
}
