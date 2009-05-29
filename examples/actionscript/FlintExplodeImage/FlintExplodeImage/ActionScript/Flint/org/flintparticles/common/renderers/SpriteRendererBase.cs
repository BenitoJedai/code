using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.emitters;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.renderers
{
	[Script(IsNative = true)]
	public class SpriteRendererBase : Sprite, Renderer
	{
		#region Properties
		/// <summary>
		/// [read-only]
		/// </summary>
		public Emitter[] emitters { get; private set; }

		#endregion

		#region Methods
		/// <summary>
		/// Adds the emitter to the renderer.
		/// </summary>
		public void addEmitter(Emitter emitter)
		{
		}

		/// <summary>
		/// Removes the emitter from the renderer.
		/// </summary>
		public void removeEmitter(Emitter emitter)
		{
		}

		#endregion

		#region Constructors
		/// <summary>
		/// The constructor creates a RendererBase class.
		/// </summary>
		public SpriteRendererBase()
		{
		}

		#endregion

	}
}
