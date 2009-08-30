// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/GraphicsConfigTemplate.html
	[Script(IsNative = true)]
	public abstract class GraphicsConfigTemplate
	{
		/// <summary>
		/// This class is an abstract class so only subclasses can be
		/// instantiated.
		/// </summary>
		public GraphicsConfigTemplate()
		{
		}

		/// <summary>
		/// Returns the "best" configuration possible that passes the
		/// criteria defined in the <code>GraphicsConfigTemplate</code>.
		/// </summary>
		public GraphicsConfiguration getBestConfiguration(GraphicsConfiguration[] @gc)
		{
			return default(GraphicsConfiguration);
		}

		/// <summary>
		/// Returns a <code>boolean</code> indicating whether or
		/// not the specified <code>GraphicsConfiguration</code> can be
		/// used to create a drawing surface that supports the indicated
		/// features.
		/// </summary>
		abstract public bool isGraphicsConfigSupported(GraphicsConfiguration @gc);

	}
}

