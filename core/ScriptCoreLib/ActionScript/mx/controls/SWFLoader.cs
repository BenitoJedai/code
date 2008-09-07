using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.ActionScript.mx.core;

namespace ScriptCoreLib.ActionScript.mx.controls
{
	// http://docs.brajeshwar.com/as3/mx/controls/SWFLoader.html
    [Script(IsNative = true)]
	public class SWFLoader : UIComponent
    {
		#region Properties
		/// <summary>
		/// A flag that indictes whether content starts loading automatically or waits for a clal to the load() method.
		/// </summary>
		public bool autoLoad { get; set; }

		/// <summary>
		/// [read-only] The number of bytes of the SWF or image file already loaded.
		/// </summary>
		public double bytesLoaded { get; private set; }

		/// <summary>
		/// [read-only] The total size of the SWF or image file.
		/// </summary>
		public double bytesTotal { get; private set; }

		/// <summary>
		/// [read-only] This property contains the object that represents the content that was loaded in the SWFLoader control.
		/// </summary>
		public DisplayObject content { get; private set; }

		/// <summary>
		/// [read-only] Height of the scaled content loaded by the control, in pixels.
		/// </summary>
		public double contentHeight { get; private set; }

		/// <summary>
		/// [read-only] Width of the scaled content loaded by the control, in pixels.
		/// </summary>
		public double contentWidth { get; private set; }

		/// <summary>
		/// A LoaderContext object to use to control loading of the content.
		/// </summary>
		public LoaderContext loaderContext { get; set; }

		/// <summary>
		/// A flag that indicates whether to maintain the aspect ratio of the loaded content.
		/// </summary>
		public bool maintainAspectRatio { get; set; }

		/// <summary>
		/// [read-only] The percentage of the image or SWF file already loaded.
		/// </summary>
		public double percentLoaded { get; private set; }

		/// <summary>
		/// A flag that indicates whether to scale the content to fit the size of the control or resize the control to the content's size.
		/// </summary>
		public bool scaleContent { get; set; }

		/// <summary>
		/// A flag that indicates whether to show a busy cursor while the content loads.
		/// </summary>
		public bool showBusyCursor { get; set; }

		/// <summary>
		/// The URL, object, class or string name of a class to load as the content.
		/// </summary>
		public object source { get; set; }

		/// <summary>
		/// If true, the content is loaded into your security domain.
		/// </summary>
		public bool trustContent { get; set; }

		#endregion

    }
}
