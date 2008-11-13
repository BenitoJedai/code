using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flex/3/langref/flash/display/Stage.html#propertySummary
    [Script(IsNative = true)]
    public class Stage : DisplayObjectContainer
    {
        #region Properties
        /// <summary>
        /// A value from the StageAlign class that specifies the alignment of the stage in Flash Player or the browser.
        /// </summary>
        public string align { get; set; }

        /// <summary>
        /// [write-only]
        /// </summary>
        public bool cacheAsBitmap { get; set; }

        /// <summary>
        /// A value from the StageDisplayState class that specifies which display state to use.
        /// </summary>
        public string displayState { get; set; }

        /// <summary>
        /// The interactive object with keyboard focus; or null if focus is not set or if the focused object belongs to a security sandbox to which the calling object does not have access.
        /// </summary>
        public InteractiveObject focus { get; set; }

        /// <summary>
        /// Gets and sets the frame rate of the stage.
        /// </summary>
        public double frameRate { get; set; }

        /// <summary>
        /// [read-only] Returns the height of the monitor that will be used when going to full screen size, if that state is entered immediately.
        /// </summary>
        public uint fullScreenHeight { get; private set; }

        /// <summary>
        /// Sets Flash Player to scale a specific region of the stage to full-screen mode.
        /// </summary>
        public Rectangle fullScreenSourceRect { get; set; }

        /// <summary>
        /// [read-only] Returns the width of the monitor that will be used when going to full screen size, if that state is entered immediately.
        /// </summary>
        public uint fullScreenWidth { get; private set; }

        /// <summary>
        /// Indicates the height of the display object, in pixels.
        /// </summary>
        public double height { get; set; }

        /// <summary>
        /// Determines whether or not the children of the object are mouse enabled.
        /// </summary>
        public bool mouseChildren { get; set; }


        /// <summary>
        /// [read-only] Returns the number of children of this object.
        /// </summary>
        public int numChildren { get; private set; }

        /// <summary>
        /// A value from the StageQuality class that specifies which rendering quality is used.
        /// </summary>
        public string quality { get; set; }

        /// <summary>
        /// A value from the StageScaleMode class that specifies which scale mode to use.
        /// </summary>
        public string scaleMode { get; set; }

        /// <summary>
        /// Specifies whether to show or hide the default items in the Flash Player context menu.
        /// </summary>
        public bool showDefaultContextMenu { get; set; }

        /// <summary>
        /// Specifies whether or not objects display a glowing border when they have focus.
        /// </summary>
        public bool stageFocusRect { get; set; }

        /// <summary>
        /// The current height, in pixels, of the Stage.
        /// </summary>
        public int stageHeight { get; set; }

        /// <summary>
        /// Specifies the current width, in pixels, of the Stage.
        /// </summary>
        public int stageWidth { get; set; }

        /// <summary>
        /// Determines whether the children of the object are tab enabled.
        /// </summary>
        public bool tabChildren { get; set; }

        /// <summary>
        /// [write-only]
        /// </summary>
        public bool tabEnabled { get; set; }

        /// <summary>
        /// [read-only] Returns a TextSnapshot object for this DisplayObjectContainer instance.
        /// </summary>
        public TextSnapshot textSnapshot { get; private set; }

        /// <summary>
        /// Indicates the width of the display object, in pixels.
        /// </summary>
        public double width { get; set; }

        #endregion

        #region Events
        /// <summary>
        /// Dispatched when the Stage object enters, or leaves, full-screen mode.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<FullScreenEvent> fullScreen;

        /// <summary>
        /// Dispatched by the Stage object when the mouse pointer moves out of the stage area.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> mouseLeave;

        /// <summary>
        /// Dispatched when the scaleMode property of the Stage object is set to StageScaleMode.NO_SCALE and the SWF file is resized.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> resize;

        #endregion


		#region Methods
	

		/// <summary>
		/// Calling the invalidate() method signals Flash Player to alert display objects on the next opportunity it has to render the display list (for example, when the playhead advances to a new frame).
		/// </summary>
		public void invalidate()
		{
		}

		/// <summary>
		/// Determines whether the Stage.focus property returns null for security reasons.
		/// </summary>
		public bool isFocusInaccessible()
		{
			return default(bool);
		}

		

	

		#endregion

	


    }
}
