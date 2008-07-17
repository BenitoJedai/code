using ScriptCoreLib;
using java.awt.image;

namespace java.awt
{
    // http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Component.html
    [Script(IsNative = true)]
    public class Component : ImageObserver
    {
        public bool Enabled
        {
            [Script(ExternalTarget = "is*")]
            get { return default(bool); }
            [Script(ExternalTarget = "set*")]
            set { }
        }

        //public event Action<@event.KeyListener> KeyListener
        //{
        //    add {

        //    }
        //    remove {

        //    }
        //}

        public void addKeyListener(@event.KeyListener e)
        {

        }

        public void removeKeyListener(@event.KeyListener e)
        {
        }


        public Image createImage(int width, int height)
        {
            return default(Image);
        }

        public void requestFocus()
        {

        }

        public Graphics getGraphics()
        {
            return default(Graphics);

        }

        public Dimension getSize()
        {
            return default(Dimension);
        }

        public void setName(string e)
        {

        }

        public void setBounds(int x, int y, int width, int height)
        {

        }

        public Rectangle getBounds()
        {
            return default(Rectangle);
        }

        public int getWidth()
        {
            return default(int);
        }

        public int getHeight()
        {
            return default(int);
        }


        public void setLocation(int x, int y)
        {
        }

        public void setSize(int width, int height)
        {

        }


        #region Color Foreground
        public Color getForeground()
        {
            return default(Color);
        }

        public void setForeground(Color c)
        {

        }
        #endregion


        public Color getBackground()
        {
            return default(Color);
        }

        public void setBackground(Color c)
        {

        }


        #region Font Font
        public Font getFont()
        {
            return default(Font);
        }

        public void setFont(Font c)
        {

        }
        #endregion

        /// <summary>
        /// Repaints this component.
        /// </summary>
        public void repaint()
        {
        }

        public virtual bool keyDown(Event evt, int key)
        {
            return default(bool);
        }

        public virtual bool keyUp(Event evt, int key)
        {
            return default(bool);

        }

    }
}
