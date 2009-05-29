using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.emitters;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.common.renderers
{
	[Script(IsNative = true)]
	public interface Renderer
	{
		#region Methods
		/// <summary>
		/// Add an emitter to this renderer.
		/// </summary>
		void addEmitter(Emitter emitter);

		/// <summary>
		/// Stop rendering particles that are managed by this emitter.
		/// </summary>
		void removeEmitter(Emitter emitter);

		#endregion

	}
}
