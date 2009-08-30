// This source code was generated for ScriptCoreLib
using ScriptCoreLib;

namespace java.awt.geom
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/geom/PathIterator.html
	[Script(IsNative = true)]
	public interface PathIterator
	{
		/// <summary>
		/// Returns the coordinates and type of the current path segment in
		/// the iteration.
		/// </summary>
		int currentSegment(double[] @coords);

		/// <summary>
		/// Returns the coordinates and type of the current path segment in
		/// the iteration.
		/// </summary>
		int currentSegment(float[] @coords);

		/// <summary>
		/// Returns the winding rule for determining the interior of the
		/// path.
		/// </summary>
		int getWindingRule();

		/// <summary>
		/// Tests if the iteration is complete.
		/// </summary>
		bool isDone();

		/// <summary>
		/// Moves the iterator to the next segment of the path forwards
		/// along the primary direction of traversal as long as there are
		/// more points in that direction.
		/// </summary>
		void next();

	}
}

