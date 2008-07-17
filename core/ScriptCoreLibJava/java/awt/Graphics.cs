using ScriptCoreLib;
using java.awt.image;

namespace java.awt
{
    // http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Graphics.html
    [Script(IsNative = true)]
    public class Graphics
    {
        /// <summary>
        /// Draws as much of the specified image as is currently available.
        /// </summary>
        public bool drawImage(Image img, int x, int y, ImageObserver observer)
        {
            return default(bool);
        }
          

        public void drawString(string str, int x, int y)
        {

        }

        public void fillRect(int x, int y, int width, int height)
        {

        }

        public void drawRect(int x, int y, int width, int height)
        {

        }

        public void setColor(Color c)
        {

        }

        public  void drawLine(int x1,
                              int y1,
                              int x2,
                              int y2)
        {

        }
    }
}
