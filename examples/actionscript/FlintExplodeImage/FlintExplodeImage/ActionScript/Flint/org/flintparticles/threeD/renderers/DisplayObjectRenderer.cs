using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.renderers;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.threeD.renderers
{
	[Script(IsNative = true)]
	public class DisplayObjectRenderer : SpriteRendererBase
	{
		#region Properties
		/// <summary>
		/// The camera controls the view for the renderer
		/// </summary>
		public Camera camera { get; set; }

		/// <summary>
		/// Indicates whether the particles should be sorted in distance order for display.
		/// </summary>
		public bool zSort { get; set; }

		#endregion

		#region Methods
		#endregion

		#region Constructors
		/// <summary>
		/// The constructor creates a DisplayObject3DRenderer.
		/// </summary>
		public DisplayObjectRenderer(bool zSort)
		{
		}

		/// <summary>
		/// The constructor creates a DisplayObject3DRenderer.
		/// </summary>
		public DisplayObjectRenderer()
		{
		}

		#endregion

	}
}
