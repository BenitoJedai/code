using ScriptCoreLib;

namespace java.nio
{
    [Script(IsNative=true)]
    public abstract class Buffer
    {
        #region methods
        /// <summary>
        /// Returns this buffer's capacity.
        /// </summary>
        public int capacity()
        {
            return default(int);
        }

        /// <summary>
        /// Clears this buffer.
        /// </summary>
        public Buffer clear()
        {
            return default(Buffer);
        }

        /// <summary>
        /// Flips this buffer.
        /// </summary>
        public Buffer flip()
        {
            return default(Buffer);
        }

        /// <summary>
        /// Tells whether there are any elements between the current position and the limit.
        /// </summary>
        public bool hasRemaining()
        {
            return default(bool);
        }

        /// <summary>
        /// Tells whether or not this buffer is read-only.
        /// </summary>
        public abstract bool isReadOnly();

        /// <summary>
        /// Returns this buffer's limit.
        /// </summary>
        public int limit()
        {
            return default(int);
        }

        /// <summary>
        /// Sets this buffer's limit.
        /// </summary>
        public Buffer limit(int newLimit)
        {
            return default(Buffer);
        }

        /// <summary>
        /// Sets this buffer's mark at its position.
        /// </summary>
        public Buffer mark()
        {
            return default(Buffer);
        }

        /// <summary>
        /// Returns this buffer's position.
        /// </summary>
        public int position()
        {
            return default(int);
        }

        /// <summary>
        /// Sets this buffer's position.
        /// </summary>
        public Buffer position(int newPosition)
        {
            return default(Buffer);
        }

        /// <summary>
        /// Returns the number of elements between the current position and the limit.
        /// </summary>
        public int remaining()
        {
            return default(int);
        }

        /// <summary>
        /// Resets this buffer's position to the previously-marked position.
        /// </summary>
        public Buffer reset()
        {
            return default(Buffer);
        }

        /// <summary>
        /// Rewinds this buffer.
        /// </summary>
        public Buffer rewind()
        {
            return default(Buffer);
        }

        #endregion

    }
}
