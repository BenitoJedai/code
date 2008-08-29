using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.mx.preloaders
{
	// http://livedocs.adobe.com/flex/3/langref/mx/preloaders/IPreloaderDisplay.html
	[Script(IsNative = true)]
	public interface IPreloaderDisplay
	{
		#region Properties
		/// <summary>
		/// Alpha level of the SWF file or image defined by the backgroundImage property, or the color defined by the backgroundColor property.
		/// </summary>
		double backgroundAlpha { get; set; }

		/// <summary>
		/// Background color of a download progress bar.
		/// </summary>
		uint backgroundColor { get; set; }

		/// <summary>
		/// The background image of the application, which is passed in by the preloader.
		/// </summary>
		object backgroundImage { get; set; }

		/// <summary>
		/// Scales the image specified by backgroundImage to different percentage sizes.
		/// </summary>
		string backgroundSize { get; set; }

		/// <summary>
		/// [write-only] The Preloader class passes in a reference to itself to the display class so that it can listen for events from the preloader.
		/// </summary>
		Sprite preloader { get; set; }

		/// <summary>
		/// The height of the stage, which is passed in by the Preloader class.
		/// </summary>
		double stageHeight { get; set; }

		/// <summary>
		/// The width of the stage, which is passed in by the Preloader class.
		/// </summary>
		double stageWidth { get; set; }

		#endregion

		#region Methods
		/// <summary>
		/// Called by the Preloader after the download progress bar has been added as a child of the Preloader.
		/// </summary>
		void initialize();

		#endregion

		#region Constructors
		#endregion


	}
}
