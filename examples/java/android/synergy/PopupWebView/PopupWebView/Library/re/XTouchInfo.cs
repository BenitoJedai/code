using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopupWebView.Library
{
    public class XTouchInfo
    {
        /**
	 * The state of the window.
	 */
        public int firstX, firstY, lastX, lastY;
        public double dist, scale, firstWidth, firstHeight;
        public float ratio;

        /**
         * Whether we're past the move threshold already.
         */
        public bool moving;
    }
}
