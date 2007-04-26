using ScriptCoreLib;

namespace java.awt
{
    [Script(IsNative = true)]
    public class Component
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


    }
}
