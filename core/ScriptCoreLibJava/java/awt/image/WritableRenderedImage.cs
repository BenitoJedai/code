// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.image;

namespace java.awt.image
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/image/WritableRenderedImage.html
	[Script(IsNative = true)]
	public interface WritableRenderedImage : RenderedImage
	{
		/// <summary>
		/// Adds an observer.
		/// </summary>
		void addTileObserver(TileObserver @to);

		/// <summary>
		/// Checks out a tile for writing.
		/// </summary>
		WritableRaster getWritableTile(int @tileX, int @tileY);

		/// <summary>
		/// Returns an array of Point objects indicating which tiles
		/// are checked out for writing.
		/// </summary>
		Point[] getWritableTileIndices();

		/// <summary>
		/// Returns whether any tile is checked out for writing.
		/// </summary>
		bool hasTileWriters();

		/// <summary>
		/// Returns whether a tile is currently checked out for writing.
		/// </summary>
		bool isTileWritable(int @tileX, int @tileY);

		/// <summary>
		/// Relinquishes the right to write to a tile.
		/// </summary>
		void releaseWritableTile(int @tileX, int @tileY);

		/// <summary>
		/// Removes an observer.
		/// </summary>
		void removeTileObserver(TileObserver @to);

		/// <summary>
		/// Sets a rect of the image to the contents of the Raster r, which is
		/// assumed to be in the same coordinate space as the WritableRenderedImage.
		/// </summary>
		void setData(Raster @r);

	}
}

